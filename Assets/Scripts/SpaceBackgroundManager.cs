using UnityEngine;

public class SpaceBackgroundManager : MonoBehaviour
{
    [Header("Star Settings")]
    public GameObject[] starPrefabs; // Array of star prefabs
    public int starCount = 100; // Number of stars to spawn
    public Vector2 starSpawnRangeX = new Vector2(-50, 50);
    public Vector2 starSpawnRangeY = new Vector2(-30, 30);
    public float minSize, maxSize;

    [Header("Meteor Settings")]
    public GameObject[] meteorPrefabs; // Array of meteor prefabs
    public int meteorCount = 10; // Number of meteors to spawn
    public Vector2 meteorSpawnRangeX = new Vector2(-50, 50);
    public Vector2 meteorSpawnRangeY = new Vector2(-30, 30);

    [Header("Effects")]
    public bool allowMeteorMovement = true;
    public float meteorSpeed = 2f;

    private Transform container; // Container for spawned objects

    void Start()
    {
        container = new GameObject("BackgroundElements").transform; // Create a container
        SpawnStars();

        if (meteorPrefabs != null)
        {
            SpawnMeteors();
        }
    }

    void SpawnStars()
    {
        for (int i = 0; i < starCount; i++)
        {
            // Random position for the star
            Vector3 starPosition = new Vector3(
                Random.Range(starSpawnRangeX.x, starSpawnRangeX.y),
                Random.Range(starSpawnRangeY.x, starSpawnRangeY.y),
                10); // Z-offset to keep it behind gameplay

            // Randomly pick a star prefab and instantiate
            GameObject star = Instantiate(
                starPrefabs[Random.Range(0, starPrefabs.Length)],
                starPosition,
                Quaternion.identity,
                container);

            // Optional: Randomize scale and opacity
            float randomScale = Random.Range(minSize, maxSize);
            star.transform.localScale = Vector3.one * randomScale;

            SpriteRenderer starRenderer = star.GetComponent<SpriteRenderer>();
            if (starRenderer != null)
            {
                starRenderer.color = new Color(1f, 1f, 1f, Random.Range(0.5f, 1f)); // Adjust opacity
            }
        }
    }

    void SpawnMeteors()
    {
        for (int i = 0; i < meteorCount; i++)
        {
            // Random position for the meteor
            Vector3 meteorPosition = new Vector3(
                Random.Range(meteorSpawnRangeX.x, meteorSpawnRangeX.y),
                Random.Range(meteorSpawnRangeY.x, meteorSpawnRangeY.y),
                9); // Z-offset closer than stars but behind gameplay

            // Randomly pick a meteor prefab and instantiate
            GameObject meteor = Instantiate(
                meteorPrefabs[Random.Range(0, meteorPrefabs.Length)],
                meteorPosition,
                Quaternion.identity,
                container);

            // Add movement if enabled
            if (allowMeteorMovement)
            {
                meteor.AddComponent<MeteorMover>().speed = meteorSpeed;
            }
        }
    }
}
