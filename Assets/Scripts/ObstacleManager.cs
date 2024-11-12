using System;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private int obstacleHitCount = 0;

    // Handle collision
    void OnCollisionEnter(Collision collision)
    {
        // TODO: Idk why it is not working

        // Check if this gameObject collides with another GameObject
        if (collision.gameObject != null)
        {
            Debug.Log($"Collision detected with: {collision.gameObject.name}");

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

    void HandlePlayerCollision(Collision collision)
    {
        // TODO: Hope this works
        PlayerManager playerManager = collision.gameObject.GetComponent<PlayerManager>();
        playerManager.playerCount += obstacleHitCount;
        playerManager.CreatePlayerCopies(playerManager.playerCount);
        Destroy(gameObject);
    }

    void HandleProjectileCollision(Collision collision)
    {
        obstacleHitCount++;
    }
}
