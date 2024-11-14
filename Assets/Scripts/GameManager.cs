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
        playerCount = 1;

        if (playerPrefab == null)
        {
            Debug.LogError("Player prefab is not assigned!");
            return;
        }
  // Find the main player's transform to determine where to spawn copies
    Transform mainPlayerTransform = GameObject.FindWithTag("Player").transform;
    
    // Starting offset distance for clones
    float offsetDistance = 2f;
    
    for (int i = 0; i < playerCount; i++)
    {
        // Alternate between left and right side
        Vector3 offset;
        if (i % 2 == 0) // Place on the right side
        {
            offset = new Vector3(offsetDistance * ((i / 2) + 1), 0, 0);
        }
        else // Place on the left side
        {
            offset = new Vector3(-offsetDistance * ((i / 2) + 1), 0, 0);
        }

        // Set spawn position by adding offset to main player's position
        Vector3 spawnPosition = mainPlayerTransform.position + offset;
        spawnPosition.y = 1; // Set Y position to ensure clones spawn on the ground

        // Instantiate the player copy
        GameObject playerClone = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);

        if (playerClone != null)
        {
            Debug.Log("Player copy created at position: " + spawnPosition);
        }
        else
        {
            Debug.LogError("Failed to create player copy.");
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
