using TMPro;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField] private int bossHitCount = 0;
    private TextMeshProUGUI value;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the Text on the Obstacle
        value = GetComponentInChildren<Canvas>().GetComponentInChildren<TextMeshProUGUI>();

        // Set the Text based on the Obstacle Count
        BossHitCountWatcher();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the Text on Boss
        BossHitCountWatcher();
    }

    // Update the Text and Material based on the Obstacle Count
    private void BossHitCountWatcher()
    {
        // Update the Text on the Obstacle
        value.text = bossHitCount.ToString();
    }

    private void HandlePlayerCollision(Collider other)
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.playerCount -= bossHitCount;
        //gameManager.CreatePlayerCopies(obstacleHitCount);
        Destroy(gameObject);
    }

    private void HandleProjectileCollision(Collider other)
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        bossHitCount += gameManager.playerCount;
        //obstacleHitCount++;
        Destroy(other.gameObject);
        if (bossHitCount >= 0)
        {
            Destroy(gameObject);
        }
    }

    // Handle the Collision Collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if this gameObject collides with another GameObject
        if (other.gameObject != null)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                HandlePlayerCollision(other);
            }
            else if (other.gameObject.CompareTag("Projectile"))
            {
                HandleProjectileCollision(other);
            }
        }
    }
}
