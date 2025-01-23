using System.Collections;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public int damage = 10; // Damage to player on collision
    public float ballSpawnInterval = 5f; // Time interval between ball spawns
    public GameObject ballPrefab; // Prefab for the balls
    public Transform[] bulletSpawnPoints; // Array of bullet spawn points
    public float ballSpeed = 5f; // Speed of the balls
    public Transform playerTransform; // Player transform for targeting
    public int speed = 5;


    private void Start()
    {
        // Find the player in the scene and get its Transform
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found in the scene! Make sure the player has the 'Player' tag.");
        }
        StartCoroutine(SpawnBallsRoutine());
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            // Move towards the player
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private IEnumerator SpawnBallsRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(ballSpawnInterval);
            SpawnBalls();
        }
    }

    private void SpawnBalls()
    {
        foreach (Transform spawnPoint in bulletSpawnPoints)
        {
            // Spawn a bullet at each spawn point
            GameObject ball = Instantiate(ballPrefab, spawnPoint.position, spawnPoint.rotation);

            // Apply velocity to the bullet
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = spawnPoint.position - transform.position;
                direction.Normalize();
                rb.velocity = direction * ballSpeed;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
