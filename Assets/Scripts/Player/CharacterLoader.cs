using UnityEngine;

public class CharacterLoader : MonoBehaviour
{
    public GameObject playerContainer; // Reference to the GameObject that contains all character prefabs

    private void Start()
    {
        // Get the index of the equipped character from PlayerPrefs
        int equippedIndex = PlayerPrefs.GetInt("EquippedCharacter", 0);

        // Disable all child GameObjects in the container
        for (int i = 0; i < playerContainer.transform.childCount; i++)
        {
            playerContainer.transform.GetChild(i).gameObject.SetActive(false);
        }

        // Activate the selected character
        if (equippedIndex >= 0 && equippedIndex < playerContainer.transform.childCount)
        {
            playerContainer.transform.GetChild(equippedIndex).gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Invalid equipped character index!");
        }
    }
}
