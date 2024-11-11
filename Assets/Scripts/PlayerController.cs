using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Camera mainCamera;
    [SerializeField] private string groundTag = "Ground";
    private Vector3 cameraOffset;
    private Vector3 touchStartPos;
    private Vector3 playerStartPos;
    private bool isDragging = false;
    private float boundaryLeft;
    private float boundaryRight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        cameraOffset = mainCamera.transform.position - transform.position;
        CalculateBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
        HandleKeyboardInput();
        HandleMouseOrTouchInput();
        ClampPosition();
    }

    // Move the player forward
    void MoveForward()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Vector3 cameraPosition = mainCamera.transform.position;
        cameraPosition.z = transform.position.z + cameraOffset.z;
        mainCamera.transform.position = cameraPosition;
    }

    // Handle Horizontal keyboard input (A, D / Left, Right)
    void HandleKeyboardInput()
    {
        float move = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(move, 0, 0);
    }

    // Handle mouse or touch input
    void HandleMouseOrTouchInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider == GetComponent<Collider>())
            {
                isDragging = true;
                touchStartPos = mousePos;
                playerStartPos = transform.position;
            }
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector3 mousePos = Input.mousePosition;
            float deltaX = mousePos.x - touchStartPos.x;
            transform.position = new Vector3(playerStartPos.x + deltaX * 0.01f, transform.position.y, transform.position.z);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    // Calculate the boundaries based on the ground plane
    void CalculateBoundaries()
    {
        GameObject ground = GameObject.FindGameObjectWithTag(groundTag);

        if (ground != null)
        {
            Collider groundCollider = ground.GetComponent<Collider>();
            if (groundCollider != null)
            {
                boundaryLeft = ground.transform.position.x - groundCollider.bounds.size.x / 2;
                boundaryRight = ground.transform.position.x + groundCollider.bounds.size.x / 2;
            }
        }
    }

    // Clamp the player's position within the boundaries
    void ClampPosition()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, boundaryLeft, boundaryRight);
        transform.position = clampedPosition;
    }
}
