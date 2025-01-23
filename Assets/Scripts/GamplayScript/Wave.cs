using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public GameObject[] enemies; // Array of enemy prefabs
    public GameObject bossEnemy; // Boss enemy prefab
    public Transform[] spawnPoints; // Spawn points for the enemies
    public Transform[] bossSpawnPoints; // Spawn points for the boss enemy
    public Text waveText; // UI Text element to display current wave number
    public Text countdownText; // UI Text element to display countdown
    public Text remainingEnemyText;
    public int initialEnemyCount = 5; // Number of enemies to spawn at the beginning of each wave
    public int enemiesPerWaveIncrease = 2; // Number of additional enemies for each wave
    public float countdownTime = 10f; // Countdown time before each wave starts after all enemies are defeated

    private int currentWave = 1; // Current wave number
    private int totalEnemiesToKill = 0; // Total enemies to kill for the current wave
    public int enemiesKilled = 0; // Enemies killed so far
    public int enemiesRemaining;
    private bool isCountingDown = false; // To prevent multiple countdowns from starting

    public CameraShake cameraShake;

    private void Start()
    {
        UpdateWaveUI();
        StartWave();
    }

    private void UpdateWaveUI()
    {
        waveText.text = "Wave: " + currentWave;
    }

    private void StartWave()
    {
        isCountingDown = false; // Reset countdown flag
        enemiesKilled = 0; // Reset enemies killed for the new wave
        totalEnemiesToKill = initialEnemyCount + (currentWave - 1) * enemiesPerWaveIncrease; // Calculate total enemies for the wave
        enemiesRemaining = totalEnemiesToKill;
        remainingEnemyText.text = "Enemies : " + enemiesRemaining;
        UpdateWaveUI();

        StartCoroutine(SpawnEnemiesForWave());

        // Check if it's a boss wave (every 5 waves)
        if (currentWave % 5 == 0)
        {
            SpawnBoss();
        }
    }

    private IEnumerator SpawnEnemiesForWave()
    {
        for (int i = 0; i < totalEnemiesToKill; i++)
        {
            // Choose a random spawn point and enemy type
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject enemyPrefab = enemies[Random.Range(0, enemies.Length)];

            // Spawn the enemy
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

            // Subscribe to the enemy's OnDeath event
            EnemyHealth enemyScript = enemy.GetComponent<EnemyHealth>();
            if (enemyScript != null)
            {
                enemyScript.OnDeath += OnEnemyDefeated;
            }

            // Wait for a short delay before spawning the next enemy
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void SpawnBoss()
    {
        if (bossEnemy == null || bossSpawnPoints.Length == 0) return;

        // Choose a random boss spawn point
        Transform spawnPoint = bossSpawnPoints[Random.Range(0, bossSpawnPoints.Length)];

        // Spawn the boss
        GameObject boss = Instantiate(bossEnemy, spawnPoint.position, spawnPoint.rotation);

        // Subscribe to the boss's OnDeath event
        EnemyHealth bossScript = boss.GetComponent<EnemyHealth>();
        if (bossScript != null)
        {
            bossScript.OnDeath += OnBossDefeated;
        }

        Debug.Log("Boss spawned in wave " + currentWave);
    }

    private void OnEnemyDefeated()
    {
        enemiesKilled++;
        enemiesRemaining--;

        remainingEnemyText.text = "Enemies : " + enemiesRemaining;

        if (cameraShake != null)
        {
            StartCoroutine(cameraShake.Shake());
        }

        // Check if all enemies for this wave have been killed
        if (enemiesKilled >= totalEnemiesToKill && !isCountingDown)
        {
            isCountingDown = true;
            StartCoroutine(CountdownToNextWave());
        }
    }

    private void OnBossDefeated()
    {
        if (cameraShake != null)
        {
            StartCoroutine(cameraShake.Shake());
        }

        Debug.Log("Boss defeated in wave " + currentWave);
    }

    private IEnumerator CountdownToNextWave()
    {
        countdownText.gameObject.SetActive(true);

        float remainingTime = countdownTime;
        while (remainingTime > 0)
        {
            countdownText.text = Mathf.Ceil(remainingTime) + "s";
            remainingTime -= Time.deltaTime;
            yield return null;
        }

        countdownText.gameObject.SetActive(false);
        currentWave++; // Increase wave number
        StartWave(); // Start the next wave
    }

    public int GetCurrentWave()
    {
        return currentWave;
    }
}
