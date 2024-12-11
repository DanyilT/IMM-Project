using UnityEngine;

public class NextLevelManager : MonoBehaviour
{
    private GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            gameManager.isWin = true;
        }
        else if (other.gameObject.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
        }
    }
}
