using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioClip hoverSound;   // Sound to play on hover
    public AudioClip clickSound; // Sound to play on click
    private AudioSource audioSource;

    private void Start()
    {
        // Get or find an AudioSource in the scene
        audioSource = FindObjectOfType<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (audioSource != null && hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound); // Play the hover sound
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound); // Play click sound
        }
    }
}
