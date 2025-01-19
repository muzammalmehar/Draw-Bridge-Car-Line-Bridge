using UnityEngine;
using UnityEngine.UI;

public class CoinsManager : MonoBehaviour
{
    public static CoinsManager Instance { get; private set; }

    public Text coinText; // Reference to the UI Text element for displaying coins
    public int coins;
    private const int defaultCoins = 1000;
    private const int coinsPerLevel = 100;

    private void Awake()
    {
        // Implement singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }

    }

    private void Start()
    {
        LoadCoins();
        UpdateCoinDisplay();
    }

    // Call this method to add coins when a level is completed
    public void AddCoinsForLevelCompletion()
    {
        coins += coinsPerLevel;
        SaveCoins();
        UpdateCoinDisplay();
    }

    // Call this method to deduct coins if needed
    public void DeductCoins(int amount)
    {
        coins -= amount;
        SaveCoins();
        UpdateCoinDisplay();
    }

    // Save coins to PlayerPrefs
    private void SaveCoins()
    {
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.Save();
    }

    // Load coins from PlayerPrefs
    private void LoadCoins()
    {
        if (PlayerPrefs.HasKey("Coins"))
        {
            coins = PlayerPrefs.GetInt("Coins");
        }
        else
        {
            coins = defaultCoins;
            SaveCoins();
        }
    }

    // Update the coin count display
    private void UpdateCoinDisplay()
    {
        coinText.text = coins.ToString();
    }

    // Get the current coin count
    public int GetCoins()
    {
        return coins;
    }
}
