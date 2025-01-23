using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies; // Array of enemy prefabs
    public Transform[] spawnPoints; // Array of spawn points
    public Transform player; // Reference to the player's Transform
    public Camera mainCamera; // Reference to the main camera

    public float spawnInterval = 2f; // Time interval between spawns

    private void Start()
    {
        // Start spawning enemies at regular intervals
        InvokeRepeating(nameof(SpawnEnemy), spawnInterval, spawnInterval);
    }

    private void SpawnEnemy()
    {
        // Get a list of valid spawn points (outside the camera view)
        List<Transform> validSpawnPoints = GetValidSpawnPoints();

        // If no valid spawn points, use all spawn points
        if (validSpawnPoints.Count == 0)
        {
            validSpawnPoints.AddRange(spawnPoints);
        }

        // Select a random spawn point from the valid ones
        Transform selectedSpawnPoint = validSpawnPoints[Random.Range(0, validSpawnPoints.Count)];

        // Select a random enemy type
        GameObject enemyPrefab = enemies[Random.Range(0, enemies.Length)];

        // Spawn the enemy at the selected spawn point
        Instantiate(enemyPrefab, selectedSpawnPoint.position, selectedSpawnPoint.rotation);
    }

    private List<Transform> GetValidSpawnPoints()
    {
        List<Transform> validPoints = new List<Transform>();

        // Get the camera's bounds
        Vector3 camBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 camTopRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        foreach (Transform spawnPoint in spawnPoints)
        {
            // Check if the spawn point is outside the camera's view
            if (spawnPoint.position.x < camBottomLeft.x || spawnPoint.position.x > camTopRight.x ||
                spawnPoint.position.y < camBottomLeft.y || spawnPoint.position.y > camTopRight.y)
            {
                validPoints.Add(spawnPoint);
            }
        }

        return validPoints;
    }
}
