using System;
using TMPro;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private Material negativeObstacleCountMaterial;
    [SerializeField] private Material positiveObstacleCountMaterial;
    private Material zeroObstacleCountMaterial;
    [SerializeField] private int obstacleHitCount = 0;
    private TextMeshProUGUI value;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the Text on the Obstacle
        value = GetComponentInChildren<Canvas>().GetComponentInChildren<TextMeshProUGUI>();

        // Take Default (Zero Count) Material
        zeroObstacleCountMaterial = GetComponent<MeshRenderer>().material;

        // Set the Text and Material based on the Obstacle Count
        ObstacleHitCountWatcher();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the Text on the Obstacle
        ObstacleHitCountWatcher();
    }

    // Update the Text and Material based on the Obstacle Count
    private void ObstacleHitCountWatcher()
    {
        // Update the Text on the Obstacle
        value.text = obstacleHitCount.ToString();

        // Set the Material based on the Obstacle Count
        switch (obstacleHitCount)
        {
            case < 0:
                GetComponent<MeshRenderer>().material = negativeObstacleCountMaterial;
                break;
            case > 0:
                GetComponent<MeshRenderer>().material = positiveObstacleCountMaterial;
                break;
            default:
                GetComponent<MeshRenderer>().material = zeroObstacleCountMaterial;
                break;
        }
    }

    private void HandlePlayerCollision(Collider other)
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.playerCount += obstacleHitCount;
        if (gameManager.playerCount < 0) Debug.Log("Game Over"); // Implement real game over logic later
        //gameManager.CreatePlayerCopies(obstacleHitCount);
        Destroy(gameObject);
    }

    private void HandleProjectileCollision(Collider other)
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        obstacleHitCount += gameManager.playerCount * gameManager.playerBonus;
        //obstacleHitCount++;
        Destroy(other.gameObject);
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
