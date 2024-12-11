using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float spawnInterval = 1.0f;
    [SerializeField] private AudioClip projectileSound;
    [SerializeField] private ParticleSystem collisionParticles;

    private float nextSpawnTime;

    void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            SpawnProjectile();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y + 1.7f, transform.position.z), Quaternion.identity);

        // Attach the collision handler
        ProjectileCollisionHandler collisionHandler = projectile.AddComponent<ProjectileCollisionHandler>();
        collisionHandler.Setup(collisionParticles);

        // Play the projectile sound
        if (MusicManager.instance != null)
        {
            AudioSource audioSource = projectile.AddComponent<AudioSource>();
            audioSource.clip = projectileSound;
            MusicManager.instance.RegisterGameEffectAudioSource(audioSource);
            audioSource.Play();
        }
    }

    // Nested class to handle projectile collisions
    private class ProjectileCollisionHandler : MonoBehaviour
    {
        private ParticleSystem collisionParticles;

        public void Setup(ParticleSystem particles)
        {
            collisionParticles = particles;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
            {
                if (collisionParticles != null)
                {
                    ParticleSystem particles = Instantiate(collisionParticles, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.identity);
                    Destroy(particles.gameObject, 0.5f);

                }
            }
        }
    }
}
