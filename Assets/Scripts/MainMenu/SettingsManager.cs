using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;
    public Slider bloomSlider;
    public AudioMixer audioMixer;
    public Slider volumeSlider;

    private Bloom bloomEffect;

    private void OnEnable()
    {
        ValidatePostProcessVolume(); // Ensure postProcessVolume is valid
    }

    private void Start()
    {
        ValidatePostProcessVolume();

        if (postProcessVolume != null && postProcessVolume.profile.TryGetSettings(out bloomEffect))
        {
            // Load saved Bloom intensity or use default
            float savedBloom = PlayerPrefs.GetFloat("BloomIntensity", 5f); // Default value is 5
            bloomEffect.intensity.value = savedBloom;
            bloomSlider.value = savedBloom;

            // Initialize Bloom slider interactability
            bloomSlider.interactable = postProcessVolume.enabled;
        }

        // Set default values if you want
        string graphicsSetting = PlayerPrefs.GetString("GraphicsSetting", "Performance");
        UpdateGraphics(graphicsSetting);
    }

    private void ValidatePostProcessVolume()
    {
        if (postProcessVolume == null)
        {
            postProcessVolume = FindObjectOfType<PostProcessVolume>();

            if (postProcessVolume == null)
            {
                Debug.LogWarning("PostProcessVolume not found. Please ensure it exists in the scene or is marked as DontDestroyOnLoad.");
                return;
            }
        }
    }

    public void OnGraphicsSettingChanged(string setting)
    {
        ValidatePostProcessVolume();
        if (setting == "Performance")
        {
            // Disable post-processing effects
            postProcessVolume.enabled = false;
            // Disable the Bloom slider
            bloomSlider.interactable = false;
        }
        else if (setting == "Quality")
        {
            // Enable post-processing effects
            postProcessVolume.enabled = true;
            // Enable the Bloom slider
            bloomSlider.interactable = true;
        }
        PlayerPrefs.SetString("GraphicsSetting", setting);
        PlayerPrefs.Save();
    }

    public void OnBloomSliderChanged()
    {
        if (bloomEffect != null)
        {
            bloomEffect.intensity.value = bloomSlider.value;
            PlayerPrefs.SetFloat("BloomIntensity", bloomSlider.value); // Save Bloom intensity
            PlayerPrefs.Save();
        }
    }

    public void OnVolumeSliderChanged()
    {
        // Set the volume level using an AudioMixer
        audioMixer.SetFloat("Volume", volumeSlider.value); // Converts linear volume to decibels
        PlayerPrefs.SetFloat("Volume", volumeSlider.value); // Save the volume setting
    }

    public void UpdateGraphics(string graphicsSetting)
    {
        OnGraphicsSettingChanged(graphicsSetting); // Apply the graphic setting
    }
}
