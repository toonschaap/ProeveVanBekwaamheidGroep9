﻿//Suryani

using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeadboardManager : MonoBehaviour
{
    public Button playAgain, send, clear;
    public Text leaderboardNames, leaderboardScores, userName;
    public int score = 599;

    // Start is called before the first frame update
    void Start()
    {
        // Bind Text Fields
        leaderboardNames = GameObject.Find("LeaderboardNames").GetComponent<Text>();
        leaderboardScores = GameObject.Find("LeaderboardScores").GetComponent<Text>();
        userName = GameObject.Find("UserName").GetComponent<Text>();
        GameObject.Find("Score").GetComponent<Text>().text = score.ToString();
        // Hook Button events
        send = GameObject.Find("Send").GetComponent<Button>();
        send.onClick.AddListener(saveScore);
        playAgain = GameObject.Find("PlayAgain").GetComponent<Button>();
        playAgain.onClick.AddListener(loadGame);
        clear = GameObject.Find("ClearLeaderboard").GetComponent<Button>();
        clear.onClick.AddListener(clearLeaderboard);
        // Ensure Leaderboard file exists
        string scoresPath = Application.persistentDataPath+"/ScoreList.txt";
        if (!System.IO.File.Exists(scoresPath))
        {
            StreamWriter writer = new StreamWriter(scoresPath, true);
            writer.Close();
        }
        // Initialize Leaderboard
        loadLeaderboard();
    }



    void loadGame()
    {
        // Call scene manager to play a new game
        SceneManager.LoadScene("MainMenu");
    }

    void saveScore()
    {
        string path = Application.persistentDataPath+"/ScoreList.txt";
        // Load Leaderboard
        List<KeyValuePair<string, int>> entries = new List<KeyValuePair<string, int>>();
        List<string> names = new List<string>();
        List<int> scores = new List<int>();

        StreamReader reader = new StreamReader(path);
        while (!reader.EndOfStream)
        {
            string[] line = reader.ReadLine().Split(',');
            entries.Add(new KeyValuePair<string, int>(line[0], int.Parse(line[1])));
        }
        reader.Close();
        // Add new score, sort the scores and save them
        entries.Add(new KeyValuePair<string, int>(userName.text, score));
        entries.Sort((x, y) => x.Value > y.Value ? -1 : 1);
        StreamWriter writer = new StreamWriter(path, false);
        foreach (var entry in entries)
        {
            writer.WriteLine(entry.Key + "," + entry.Value.ToString());
        }
        writer.Close();
        // Reload leaderboard
        loadLeaderboard();
    }

    void loadLeaderboard()
    {
        //string path = "ScoreList.txt";
        string path = Application.persistentDataPath + "/ScoreList.txt";
        // Clear Leaderboard
        leaderboardNames.text = "";
        leaderboardScores.text = "";
        // Load Leaderboard data
        StreamReader reader = new StreamReader(path);
        while (!reader.EndOfStream)
        {
            string[] txt = reader.ReadLine().Split(',');
            if (txt.Length >= 2)
            {
                leaderboardNames.text += txt[0] + "\n";
                leaderboardScores.text += txt[1] + "\n";
            }
        }
        reader.Close();
    }

    void clearLeaderboard()
    {
        // Delete everything in the scoreslist file
        string scoresPath = Application.persistentDataPath + "/ScoreList.txt";
        StreamWriter writer = new StreamWriter(scoresPath, false);
        writer.Close();
        // Reload leaderboard
        loadLeaderboard();
    }

}
