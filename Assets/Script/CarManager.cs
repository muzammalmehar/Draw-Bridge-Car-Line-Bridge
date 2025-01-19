using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            GameManager.instance.LevelCompleted();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision){

        
        if(collision.gameObject.tag == "Over"){
            GameManager.instance.LevelFailed();
        }
    }
}
