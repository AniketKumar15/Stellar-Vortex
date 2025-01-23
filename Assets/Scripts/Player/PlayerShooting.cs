using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Bullet prefab
    public GameObject missilePrefab;
    public Transform[] shootPoints; // Array of shoot points (two points)
    public float fireRate = 0.5f; // Time between shots

    public float bulletSpeed = 10f; // Speed of bullets
    public float bulletLifetime = 2f; // Lifetime of bullets

    public PlayerMovement playerMovement; // Reference to PlayerMovement script

    //PowerUp
    private float nextFireTime = 0f;
    private float currentFireRate;
    public GameObject currentProjectile;

    private void Start()
    {
        currentFireRate = fireRate; // Initialize the fire rate
        currentProjectile = bulletPrefab; // Start with normal bullets
    }

    void Update()
    {
        // Check fire conditions based on control type
        if (Time.time >= nextFireTime)
        {
            if (!playerMovement.isMobileControl && Input.GetMouseButton(0)) // PC shooting
            {
                nextFireTime = Time.time + currentFireRate;
                AudioManager.instance.Play("fire");
                ShootFromAllPoints();
            }
            else if (playerMovement.isMobileControl && playerMovement.rightJoystick.GetInput().magnitude > 0.1f) // Mobile shooting
            {
                nextFireTime = Time.time + currentFireRate;
                AudioManager.instance.Play("fire");
                ShootFromAllPoints();
            }
        }
    }

    void ShootFromAllPoints()
    {
        foreach (Transform shootPoint in shootPoints)
        {
            // Instantiate the bullet at each shoot point
            GameObject bullet = Instantiate(currentProjectile, shootPoint.position, shootPoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = shootPoint.right * bulletSpeed;

            // Destroy the bullet after its lifetime
            Destroy(bullet, bulletLifetime);
        }
       
    }

    public float getCurrentFireRate()
    {
        return currentFireRate;
    }

    public GameObject getCurrentProjectile()
    {
        return currentProjectile;
    }

    public void IncreaseFireRate(float newFireRate, float duration)
    {
        StopAllCoroutines(); // Stop any existing fire rate coroutine
        StartCoroutine(TemporaryFireRateBoost(newFireRate, duration));
    }

    private IEnumerator TemporaryFireRateBoost(float newFireRate, float duration)
    {
        currentFireRate = newFireRate; // Apply the new fire rate
        yield return new WaitForSeconds(duration); // Wait for the effect duration
        currentFireRate = fireRate; // Revert to the normal fire rate
    }

    public void ChangeToMissile(float duration)
    {
        StopAllCoroutines(); // Stop any existing projectile change coroutine
        StartCoroutine(TemporaryProjectileChange(missilePrefab, duration));
    }

    private IEnumerator TemporaryProjectileChange(GameObject newProjectile, float duration)
    {
        currentProjectile = newProjectile; // Change to the new projectile (missile)
        yield return new WaitForSeconds(duration); // Wait for the duration
        currentProjectile = bulletPrefab; // Revert back to the normal bullet
    }
}
