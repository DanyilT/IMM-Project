using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private string way = "back";

    // Update is called once per frame
    void Update()
    {
        // Move the object
        switch (way)
        {
            case "forward":
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                break;
            case "back":
                transform.Translate(Vector3.back * speed * Time.deltaTime);
                break;
            case "left":
                transform.Translate(Vector3.left * speed * Time.deltaTime);
                break;
            case "right":
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                break;
            case "up":
                transform.Translate(Vector3.up * speed * Time.deltaTime);
                break;
            case "down":
                transform.Translate(Vector3.down * speed * Time.deltaTime);
                break;
        }
    }

    // TODO: If out of seen, destroy the object
}
