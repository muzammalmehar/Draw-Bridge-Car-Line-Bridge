using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingGame : MonoBehaviour
{
    public Slider slider;

    public float loadingTime = 3f;
    private float currentTime = 0f;

    void Start()
    {
        Load();
    }

    void Load()
    {

        slider.value = 0f;

        StartCoroutine(SimulateLoading());
    }

    IEnumerator SimulateLoading()
    {
        while (currentTime < loadingTime)
        {
            slider.value = currentTime / loadingTime;

            currentTime += Time.deltaTime;

            yield return null;
        }

        // Load the last saved level
        int lastLevelIndex = LevelManager.LoadLastLevel();
        if (lastLevelIndex != 0)
        {
            // Load the last saved level if available
            LevelManager.LoadLevel(lastLevelIndex);
        }
        else if(lastLevelIndex == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
