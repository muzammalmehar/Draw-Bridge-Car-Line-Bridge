using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
   
   public static GameManager instance;
   public bool isGameStarted = false;
   public AudioSource[] soundEffect;
   public GameObject[] UIPanels;
   public int currentScene;
   public GameObject settingPanel;
   public GameObject lineDrawer;
   public GameObject settingText;
   void Awake(){

    if(instance == null){

        instance = this;
    }
   }

   private void Start(){

    currentScene = SceneManager.GetActiveScene().buildIndex;
    AdsManager.instance.LoadAd();
    soundEffect[0].Play();
    UIPanels[0].SetActive(false);
    UIPanels[1].SetActive(false);
    settingPanel.SetActive(false);
    settingText.SetActive(false);

    
    int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
    int previousLevelIndex = currentLevelIndex - 1;

    // Save the current and previous level indices
    LevelManager.SaveLevels(currentLevelIndex, previousLevelIndex);

   } 
   public void StarGame(){

    isGameStarted = true;
    Timer.instance.StartTimer();

   }

   public void LevelCompleted(){

    Timer.instance.StopTimer();
    soundEffect[1].Play();
    UIPanels[0].SetActive(true);
    CoinsManager.Instance.AddCoinsForLevelCompletion();
    AdsManager.instance.ShowInterstitialAd();
    AdsManager.instance.DestroyAd();    


   }

   public void LevelFailed(){

    Timer.instance.StopTimer();
    soundEffect[2].Play();
    UIPanels[1].SetActive(true);
    carController.instance.StopCarMovement();
    AdsManager.instance.ShowInterstitialAd();
    AdsManager.instance.DestroyAd();    
   }

    public void Restart(){

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Home(){

        SceneManager.LoadScene(sceneName: "MainMenu");
    }

    public void LoadNextLevel(){

        SceneManager.LoadScene(sceneName: "LoadingLevel");
    }

    public void PauseGame(){
    
        if(isGameStarted == true){
            Debug.Log("Setting panel can't open during play");
            settingText.SetActive(true);

        } else if (isGameStarted == false){
            settingPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ResumeGame(){

        Time.timeScale = 1;
        settingPanel.SetActive(false);
    }


}
