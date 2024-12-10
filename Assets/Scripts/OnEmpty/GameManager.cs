using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private float rowSpacing = 2f; // Space between rows
    [SerializeField] private float columnSpacing = 2f; // Space between players in the same row

    public int playerCount = 1; // Starting with 1 for the main player
    public int playerBonus = 1;
    public bool isWin = false;
    public bool isGameOver = false;

    private Transform mainPlayer;
    private List<Vector3> spawnPositions = new List<Vector3>();
    private int currentSpawnIndex = 0;

    void Start()
    {
        mainPlayer = GameObject.FindWithTag("Player").transform;
        // Calculate initial positions starting from row 2 (since row 1 has main player)
        CalculateSpawnPositions(20);
    }

    void Update()
    {
        if (playerCount <= 0 && !isGameOver)
        {
            isGameOver = true;
            Debug.Log("Game Over");
        }
    }

    public void CreatePlayerCopy()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("Player prefab is not assigned!");
            return;
        }

        // If we're running out of pre-calculated positions, calculate more
        if (currentSpawnIndex >= spawnPositions.Count - 5)
        {
            CalculateSpawnPositions(20);
        }

        // Get the next spawn position
        Vector3 relativePosition = spawnPositions[currentSpawnIndex];
        Vector3 worldPosition = mainPlayer.position + relativePosition;
        worldPosition.y = 1f; // Set consistent Y position

        // Instantiate the player copy
        GameObject playerClone = Instantiate(playerPrefab, worldPosition, mainPlayer.rotation);

        if (playerClone != null)
        {
            Debug.Log($"Player copy {playerCount + 1} created at position: " + worldPosition);
            currentSpawnIndex++;
            playerCount++;
        }
        else
        {
            Debug.LogError("Failed to create player copy.");
        }
    }

    private void CalculateSpawnPositions(int count)
    {
        // Clear the list only if we're starting fresh
        if (currentSpawnIndex == 0)
        {
            spawnPositions.Clear();
        }

        // Start from row 2 since row 1 has the main player
        int currentRow = 2;
        int totalCalculated = spawnPositions.Count;
        int remainingCopies = count;

        while (remainingCopies > 0)
        {
            // Each row has currentRow number of players
            int playersInThisRow = currentRow;

            // Calculate row position (start from second row)
            float rowZOffset = (currentRow - 1) * rowSpacing;
            float startX = -(playersInThisRow - 1) * columnSpacing * 0.5f;

            // Add positions for this row
            for (int i = 0; i < playersInThisRow; i++)
            {
                Vector3 position = new Vector3(
                    startX + (i * columnSpacing),
                    0,
                    -rowZOffset // Negative to spawn behind the main player
                );
                spawnPositions.Add(position);
            }

            remainingCopies -= playersInThisRow;
            currentRow++;
        }
    }
}
