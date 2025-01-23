using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    public float rotationSpeed = 100f; // Rotation speed in degrees per second

    void Update()
    {
        // Rotate the sprite around its Z-axis
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
