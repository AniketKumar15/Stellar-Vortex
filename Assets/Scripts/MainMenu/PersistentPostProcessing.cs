using UnityEngine;

public class PersistentPostProcessing : MonoBehaviour
{
    private void Awake()
    {
        // Ensure this PostProcessVolume is not duplicated
        var instances = FindObjectsOfType<PersistentPostProcessing>();
        if (instances.Length > 1)
        {
            Destroy(gameObject); // Destroy duplicates
        }
        else
        {
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
    }
}
