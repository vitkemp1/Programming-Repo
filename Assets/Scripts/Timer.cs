using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private PlayerController playerControllerScript;
    private float timeRemaining;
    private float timeOfGame = 180f;
    private IEnumerator coroutine;
    public Text timeText;
    private void Start()
    {        
        playerControllerScript = GameObject.Find("PlayerController").GetComponent<PlayerController>();
    }
    void Update()
    {
        if (playerControllerScript.isGameStarted)
        {
            DisplayTime();
            if (timeRemaining <= 0)
            {
                stopTimer();
            }
        }
       
    }
    public void startTimer()
    {
        timeRemaining = timeOfGame;
        coroutine = CountTime();
        StartCoroutine(coroutine);
    }
    public void stopTimer()
    {
        StopCoroutine(coroutine);
        playerControllerScript.stopGame();
    }
   
    void DisplayTime()
    {
        float timeToDisplay = timeRemaining;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = "Time : "+ minutes+":"+ seconds;        
    }
    IEnumerator CountTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeRemaining--;
        }
    }
}
