using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public int maxTime = 10;
    public bool isTimerRunning;
    public int remainingTime;
    public Text completedInText;
    public static Timer instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        isTimerRunning = false;
        remainingTime = maxTime;
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        timerText.text = remainingTime.ToString();

        int completedIn = maxTime - remainingTime;
        completedInText.text = completedIn.ToString();
        
    }

    public void StartTimer()
    {
        if (!isTimerRunning)
        {
            isTimerRunning = true;
            StartCoroutine(Countdown());
        }
    }

    public void StopTimer()
    {
        isTimerRunning = false;
        StopCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        while (isTimerRunning && remainingTime > 0)
        {
            yield return new WaitForSeconds(1f);
            remainingTime--;
            UpdateTimerText();
        }

        if (remainingTime <= 0)
        {
            isTimerRunning = false;
            remainingTime = 0;
            UpdateTimerText();
            GameManager.instance.LevelFailed();
        }
    }
}
