using UnityEngine;

public class MeteorMover : MonoBehaviour
{
    public float speed = 2f;

    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Destroy if out of range to prevent buildup
        if (transform.position.y < -50) // Adjust as needed
        {
            Destroy(gameObject);
        }
    }
}
