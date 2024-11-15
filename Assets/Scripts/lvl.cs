using UnityEngine;

public class lvl : MonoBehaviour
{
    public GameObject levelUpPanel;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
             // Display the "Level Up" message
            levelUpPanel.SetActive(true);

            // Stop the game by setting time scale to 0
            Time.timeScale = 0;

            // Optional: Log message for debugging
            Debug.Log("Level Up!");
        }
    }
}
