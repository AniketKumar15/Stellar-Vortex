using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CharacterStats
{
    public Sprite characterSprite;
    public float health;
    public float power;
    public float speed;
    public int price;
    public bool isUnlocked = false; // Tracks if the character is unlocked
}

public class MainMenu : MonoBehaviour
{
    public Text coinText; // UI text to display coins
    public Image characterDisplay;
    private int currentIndex = 0;

    public Slider healthSlider;
    public Slider powerSlider;
    public Slider speedSlider;
    public Text priceText;
    public Button buyButton; // Button for buying or equipping characters
    public CharacterStats[] characters;

    private void Start()
    {
        LoadCharacterData();
        UpdateCoinDisplay();
        UpdateCharacterDisplay();
    }

    private void UpdateCoinDisplay()
    {
        if (MoneyManager.Instance != null)
        {
            coinText.text = "Coins: " + MoneyManager.Instance.GetTotalCoins();
        }
    }

    public void NextCharacter()
    {
        currentIndex++;
        if (currentIndex >= characters.Length)
        {
            currentIndex = 0;
        }
        UpdateCharacterDisplay();
    }

    public void PreviousCharacter()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = characters.Length - 1;
        }
        UpdateCharacterDisplay();
    }

    private void UpdateCharacterDisplay()
    {
        CharacterStats currentCharacter = characters[currentIndex];

        characterDisplay.sprite = currentCharacter.characterSprite;
        healthSlider.value = currentCharacter.health;
        powerSlider.value = currentCharacter.power;
        speedSlider.value = currentCharacter.speed;
        priceText.text = currentCharacter.isUnlocked ? "Unlocked" : $"Price - {currentCharacter.price}";

        // Update the buy/equip button
        if (currentCharacter.isUnlocked)
        {
            buyButton.GetComponentInChildren<Text>().text = "Equip";
            buyButton.interactable = true; // Equip button should always be interactable
        }
        else
        {
            buyButton.GetComponentInChildren<Text>().text = "Buy";
            buyButton.interactable = MoneyManager.Instance.GetTotalCoins() >= currentCharacter.price;
        }
    }

    public void BuyOrEquipCharacter()
    {
        CharacterStats currentCharacter = characters[currentIndex];

        if (currentCharacter.isUnlocked)
        {
            EquipCharacter();
        }
        else
        {
            BuyCharacter();
        }
    }

    private void BuyCharacter()
    {
        CharacterStats currentCharacter = characters[currentIndex];

        if (MoneyManager.Instance.GetTotalCoins() >= currentCharacter.price)
        {
            MoneyManager.Instance.SpendCoins(currentCharacter.price);
            currentCharacter.isUnlocked = true;
            SaveCharacterData();
            UpdateCoinDisplay();
            UpdateCharacterDisplay();
        }
        else
        {
            Debug.LogWarning("Not enough coins to buy this character!");
        }
    }

    private void EquipCharacter()
    {
        PlayerPrefs.SetInt("EquippedCharacter", currentIndex);
        Debug.Log($"Equipped character index: {currentIndex}");
        buyButton.GetComponentInChildren<Text>().text = "Equiped";
    }

    private void SaveCharacterData()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            PlayerPrefs.SetInt($"Character_{i}_Unlocked", characters[i].isUnlocked ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    private void LoadCharacterData()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].isUnlocked = PlayerPrefs.GetInt($"Character_{i}_Unlocked", i == 0 ? 1 : 0) == 1;
        }
    }
}
