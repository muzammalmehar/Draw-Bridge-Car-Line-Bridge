using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadingLevel : MonoBehaviour
{
    public int previousLevel;
    private int loadingTime;

    public Slider loadingSlider;
    private void Awake(){

         previousLevel = GameManager.instance.currentScene;
 
         loadingTime = Random.Range(3, 5);
        
    }

    private void Start(){

        Invoke(nameof(LoadNextLevel), loadingTime);

    }

    void Update(){
        LoadingBar();
    }
    public void LoadNextLevel(){

        SceneManager.LoadScene(previousLevel + 1);
    }

    private void LoadingBar(){

        loadingSlider.value += 0.3f * Time.deltaTime;


    }
}
