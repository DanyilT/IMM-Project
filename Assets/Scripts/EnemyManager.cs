using TMPro;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private int enemiesCount = 0;
    private int bonusValue = 0;
    private TextMeshProUGUI value;
    private TextMeshProUGUI bonus;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the Text on the Obstacle
        value = GetComponentInChildren<Canvas>().GetComponentInChildren<TextMeshProUGUI>();

        // Get the Text on the Bonus
        bonus = transform.Find("Bonus/Canvas/Value").GetComponent<TextMeshProUGUI>();

        // Generate a random bonus value
        GenerateRandomBonus();

        // Set the Text and Material based on the Obstacle Count
        ObstacleHitCountWatcher();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the Text on the Obstacle
        ObstacleHitCountWatcher();
    }

    // Generate a random bonus value that is a multiple of 1, 2, 3, 4, or 5
    private void GenerateRandomBonus()
    {
        int[] multipliers = { 1, 2, 3, 4, 5 };
        int randomIndex = Random.Range(0, multipliers.Length);
        bonusValue = multipliers[randomIndex];
        bonus.text = "×" + bonusValue;
    }

    // Update the Text and Material based on the Obstacle Count
    private void ObstacleHitCountWatcher()
    {
        // Update the Text on the Obstacle
        value.text = enemiesCount.ToString();
    }

    private void HandlePlayerCollision(Collider other)
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.playerCount -= enemiesCount;
        if (gameManager.playerCount < 0) Debug.Log("Game Over"); // Implement real game over logic later
        //gameManager.CreatePlayerCopies(obstacleHitCount);
        Destroy(gameObject);
    }

    private void HandleProjectileCollision(Collider other)
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemiesCount -= gameManager.playerCount * gameManager.playerBonus;
        //enemiesCount--;
        Destroy(other.gameObject);
        if (enemiesCount <= 0)
        {
            gameManager.playerBonus = bonusValue;
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
