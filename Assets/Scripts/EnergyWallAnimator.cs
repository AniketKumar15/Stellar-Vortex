using UnityEngine;

public class EnergyWallAnimator : MonoBehaviour
{
    public Material wallMaterial;
    public float minEmission = 1f; // Minimum emission intensity
    public float maxEmission = 5f; // Maximum emission intensity
    public float pulseSpeed = 2f;  // Speed of pulsing

    void Update()
    {
        if (wallMaterial != null)
        {
            // Calculate emission intensity using a sine wave
            float intensity = Mathf.Lerp(minEmission, maxEmission, Mathf.Sin(Time.time * pulseSpeed) * 0.5f + 0.5f);
            wallMaterial.SetColor("_EmissionColor", Color.green * intensity);
        }
    }
}
