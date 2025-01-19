using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class WifiChecking : MonoBehaviour
{
    public GameObject internetPanel;
    public int timeForWait;
    public static WifiChecking instance;
    public Slider loadingSlider;
    private void Awake(){

         timeForWait = Random.Range(2,3);

         if(instance == null){

            instance=this;
            
         }
    }
    public void Update()
    {
        StartCoroutine(ConnectionChecking());
        LoadingSlider();
    }

    IEnumerator ConnectionChecking(){

        yield return new WaitForSeconds(timeForWait);

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            internetPanel.SetActive(true);
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                internetPanel.SetActive(false);
                StartCoroutine(LoadLevel());
            }
        }

    
    }
    IEnumerator LoadLevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        yield return new WaitForSeconds(timeForWait * Time.deltaTime);

    }

    void LoadingSlider(){

        loadingSlider.value += 0.3f * Time.deltaTime;
    }
}
