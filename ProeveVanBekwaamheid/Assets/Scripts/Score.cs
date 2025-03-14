﻿//Tim Gijzen //Dexter Changed variables values so that the game time equals 5 minutes
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    //Set UI text in here 
    public Text scoreDisplay;

    //The score
    public float currentScore;

    //Get-set from LevelCountdown Script
    public float remTime;

    //Get-set from EnemyAI Script
    public int enemyDead = 0;

    //Set LevelCountdown and EnemyAI on this gameobject to let this script work
    public GameObject scriptManagerCountdown;



    void Update()
    {
        //Get values from other scripts script = <>
        remTime = scriptManagerCountdown.GetComponent<LevelCountdown>().remainingtime;

        //RemTime for every second left +1 score
        //EnemyDead for every Enemy dead +35 score
        currentScore = enemyDead * 10;



        //set score to : Score: "CurrentScore"
        scoreDisplay.text = ("Score: ") + currentScore.ToString();
    }
}