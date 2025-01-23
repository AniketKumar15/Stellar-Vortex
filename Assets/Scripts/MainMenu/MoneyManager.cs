using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    public int totalCoins = 0; // Total coins collected

    private void Awake()
    {
        // Ensure there's only one MoneyManager instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
            LoadCoins();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void AddCoins(int amount)
    {
        totalCoins += amount;
        SaveCoins();
    }

    public int GetTotalCoins()
    {
        return totalCoins;
    }

    private void SaveCoins()
    {
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        PlayerPrefs.Save(); // Ensure data is written immediately
    }

    private void LoadCoins()
    {
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0); // Load saved coins, default to 0 if not found
    }

    public void SpendCoins(int amount)
    {
        if (totalCoins >= amount)
        {
            totalCoins -= amount;
            SaveCoins();
        }
        else
        {
            Debug.LogWarning("Not enough coins to spend!");
        }
    }
}
