using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float lifetime = 1f; // Time before the explosion is destroyed

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
