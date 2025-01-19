using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
public class MenuManager : MonoBehaviour
{

    public GameObject infoPanel;
    public GameObject privacyPanel;

    void Start(){
                privacyPanel.SetActive(true);

    }

    public void Play(){

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }


    public void ClosePrivacyPanel(){

        privacyPanel.SetActive(false);
    }

    public void OpenInfo(){

        infoPanel.SetActive(true);
    }

    public void CloseInfo(){

        infoPanel.SetActive(false);
    }

    public void LoadSavedLevel(){
        
        int lastLevelIndex = LevelManager.LoadLastLevel();
        if (lastLevelIndex != 0)
        {
            // Load the last saved level if available
            LevelManager.LoadLevel(lastLevelIndex);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void OpenPrivacyPolicy(){

        Application.OpenURL("https://brixxgamestudio.com/carlinebridge-privacy-policy.php");
        
    }
}
