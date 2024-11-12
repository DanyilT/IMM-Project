using UnityEngine;
using UnityEngine.AI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float spawnInterval = 1.0f;
    private float nextSpawnTime;
    internal int playerCount = 1;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            SpawnProjectile();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    // Spawn a projectile
    void SpawnProjectile()
    {
        Instantiate(projectilePrefab, transform.position, transform.rotation);
    }

    // Create copies of the player at the nearest available space on the ground
    public void CreatePlayerCopies(int numberOfCopies)
    {
        for (int i = 0; i < numberOfCopies; i++)
        {
            Vector3 spawnPosition = FindNearestAvailableSpace();
            Instantiate(gameObject, spawnPosition, transform.rotation);
        }
    }

    // Find the nearest available space on the ground
    private Vector3 FindNearestAvailableSpace()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 5;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 5, 1);
        return hit.position;
    }
}
