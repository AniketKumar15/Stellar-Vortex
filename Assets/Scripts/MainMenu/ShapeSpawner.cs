using UnityEngine;
using System.Collections.Generic;

public class ShapeSpawner : MonoBehaviour
{
    public Camera mainCamera; // Assign your main camera
    public GameObject bg; // Assign your parent GameObject (e.g., 'bg')
    public GameObject[] shapes; // Array of shape prefabs
    public int shapeCount = 10; // Number of shapes to spawn
    public float shapePadding = 0.5f; // Minimum space between shapes

    private List<Vector2> placedPositions = new List<Vector2>();

    void Start()
    {
        SpawnShapes();
    }

    void SpawnShapes()
    {
        // Get the camera bounds
        Vector2 screenMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector2 screenMax = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        int attempts = 0;

        for (int i = 0; i < shapeCount; i++)
        {
            bool placed = false;
            while (!placed && attempts < 1000) // Prevent infinite loops
            {
                // Generate random position within camera bounds
                Vector2 randomPosition = new Vector2(
                    Random.Range(screenMin.x, screenMax.x),
                    Random.Range(screenMin.y, screenMax.y)
                );

                // Check for overlaps
                if (IsPositionValid(randomPosition))
                {
                    // Spawn a random shape
                    GameObject shape = Instantiate(
                        shapes[Random.Range(0, shapes.Length)],
                        randomPosition,
                        Quaternion.identity
                    );

                    // Set the spawned shape as a child of the 'bg' GameObject
                    shape.transform.SetParent(bg.transform);

                    placedPositions.Add(randomPosition);
                    placed = true;
                }

                attempts++;
            }

            if (attempts >= 1000)
            {
                Debug.LogWarning("Failed to place all shapes without overlaps.");
                break;
            }
        }
    }

    bool IsPositionValid(Vector2 position)
    {
        foreach (Vector2 placedPosition in placedPositions)
        {
            if (Vector2.Distance(placedPosition, position) < shapePadding)
            {
                return false;
            }
        }
        return true;
    }
}
