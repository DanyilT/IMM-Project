using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject); // Destroy the enemy
            Destroy(other.gameObject); // Destroy the projectile
        }
    }
}
