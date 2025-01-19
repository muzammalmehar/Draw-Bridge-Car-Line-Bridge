using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioSource bgMusicAudio;
    public AudioSource soundFxAudio;


    void Awake(){

        if(instance == null){

            instance = this;
        }

        DontDestroyOnLoad(this.gameObject); 
    }
    void Start(){

        bgMusicAudio.Play();
        soundFxAudio.Play();
    }
}
