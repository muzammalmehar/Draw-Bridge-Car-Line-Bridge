using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{

    public int levelDifference;
    public TextMeshProUGUI levelNoText;
    public Text levelBarText;
    public int levelNo;
    void Start(){

    levelNo = SceneManager.GetActiveScene().buildIndex - levelDifference;

    if(levelNo <= 9){
   
    levelNoText.text = levelNo.ToString();

    } else if(levelNo > 9){

    levelNoText.text = levelNo.ToString();
    
    }

    if(levelNo <= 9){
   
    levelBarText.text = "Level No 0" + levelNo.ToString();

    } else if(levelNo > 9){

    levelBarText.text = "Level No " + levelNo.ToString();
    
    }

    }
    
}
