using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float spawnInterval = 1.0f;
    private float nextSpawnTime;

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
        Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y + 1.7f, transform.position.z), Quaternion.identity);
    }
}
