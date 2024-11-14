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

        // Destroy the Obstacle if the Obstacle behind the Player
        // this logic is not necessary it doesnt not affect gameplay 
       // DestroyIfBehindThePlayer();
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

    // Destroy the Obstacle if the Obstacle behind the Player
    private void DestroyIfBehindThePlayer()
    {
        if (transform.position.z < GameObject.Find("Player").GetComponent<Transform>().position.z)
        {
            Destroy(gameObject);
        }
    }

    private void HandlePlayerCollision(Collision collision)
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.CreatePlayerCopies(obstacleHitCount);
        Destroy(gameObject);
    }

    private void HandleProjectileCollision(Collision collision)
    {
        obstacleHitCount++;
        Destroy(collision.gameObject);
    }

    // Handle collision
    private void OnCollisionEnter(Collision collision)
    {
        // Check if this gameObject collides with another GameObject
        if (collision.gameObject != null)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                HandlePlayerCollision(collision);
            }
            else if (collision.gameObject.CompareTag("Projectile"))
            {
                HandleProjectileCollision(collision);
            }
        }
    }
}
