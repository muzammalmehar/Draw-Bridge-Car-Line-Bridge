using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    private const string CURRENT_LEVEL_KEY = "currentLevel";
    private const string PREVIOUS_LEVEL_KEY = "previousLevel";

    public static LevelManager instance;

    void Start(){

        if(instance == null){

            instance = this;

        }
        LoadLastLevel();
    }
    // Function to save the current and previous level
    public static void SaveLevels(int currentLevelIndex, int previousLevelIndex)
    {
        PlayerPrefs.SetInt(CURRENT_LEVEL_KEY, currentLevelIndex);
        PlayerPrefs.SetInt(PREVIOUS_LEVEL_KEY, previousLevelIndex);
        PlayerPrefs.Save();
    }

    // Function to load the last saved level
    public static int LoadLastLevel()
    {
        if (PlayerPrefs.HasKey(CURRENT_LEVEL_KEY))
        {
            int levelIndex = PlayerPrefs.GetInt(CURRENT_LEVEL_KEY);
            return levelIndex;
        }
        else
        {
            // If no level is saved, return 0
            return 0;
        }
    }

    // Function to load the previous level
    public static int LoadPreviousLevel()
    {
        if (PlayerPrefs.HasKey(PREVIOUS_LEVEL_KEY))
        {
            int levelIndex = PlayerPrefs.GetInt(PREVIOUS_LEVEL_KEY);
            return levelIndex;
        }
        else
        {
            // If no previous level is saved, return 0
            return 0;
        }
    }

    // Function to reset the saved levels
    public static void ResetSavedLevels()
    {
        PlayerPrefs.DeleteKey(CURRENT_LEVEL_KEY);
        PlayerPrefs.DeleteKey(PREVIOUS_LEVEL_KEY);
        PlayerPrefs.Save();
    }

    // Function to load a specific level
    public static void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
