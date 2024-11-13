using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    public int playerCount = 1;

    // Update is called once per frame
    void Update()
    {

    }

    // Create copies of the player at the nearest available space on the ground
    public void CreatePlayerCopies(int numberOfCopies)
    {
        playerCount += numberOfCopies;

        // If the number of copies is positive: create the copies
        if (numberOfCopies >= 1)
        {
            for (int i = 0; i < numberOfCopies; i++)
            {
                Vector3 spawnPosition = FindNearestAvailableSpace(); // Have to implement this function

                if (spawnPosition != Vector3.positiveInfinity)
                {
                    Debug.Log("Spawn position: " + spawnPosition);
                    Instantiate(playerPrefab, spawnPosition, playerPrefab.transform.rotation);
                    Debug.Log("Player copy created");
                }
                else
                {
                    Debug.LogError("Failed to find a valid spawn position");
                }
            }
        }

        // If the number of copies is negative: destroy the copies
        else if (numberOfCopies < 0)
        {
            GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("Player");

            if (playerCount > 0)
            {
                for (int i = 0; i < Mathf.Abs(numberOfCopies); i++)
                {
                    Destroy(playerUnits[playerUnits.Length - 1 - i]);
                    Debug.Log("Player copy destroyed");
                }
            }
            else
            {
                // TODO: Implement the real game over logic later
                Debug.Log("Game Over");
            }
        }
    }

    // Find the nearest available space on the ground
    private Vector3 FindNearestAvailableSpace()
    {
        // TODO: Have To Implement This Function
        // Find the nearest available space to all player's units on the ground (use Tag: Ground) and return the position (Vector3)
        // Note: Y position should be 1 (it is will spawn on the ground)

        return Vector3.positiveInfinity;
    }
}
