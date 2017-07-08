using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    public golfball BallClass;
    public MenuManager menuManager;
    private golfball currentBall;
    
    private int currentScore = 0;
    private Dictionary<int, int> scoreBoard = new Dictionary<int, int>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public Course[] Courses;
    public int CurrentLevelIndex { get; private set; }

    public Course getCurrentLevel()
    {
        return Courses[CurrentLevelIndex];
    }

    public void loadLevel(int index)
    {
        if (index >= Courses.Length)
        {
            unloadLevel();
            return;
        }
        CurrentLevelIndex = index;

        for(int i = 0; i < Courses.Length; i++)
        {
            if (i == index)
            {
                Courses[i].gameObject.SetActive(true);
            }
            else
            {
                Courses[i].gameObject.SetActive(false);
            }
        }
        RespawnBall();
    }

    public void unloadLevel()
    {
        foreach (Course gc in Courses)
        {
            gc.gameObject.SetActive(false);
        }

        UnpauseGame();
        currentBall.Kill();  
        currentBall = null;
    }

    // Use this for initialization
    void Start ()
    {
        CurrentLevelIndex = 0;
        Load();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void levelEnded()
    {
        //int index = (CurrentLevelIndex + 1) < Courses.Length ? (CurrentLevelIndex + 1) : 0;

        if (currentScore < getScoreForLevel(CurrentLevelIndex) || getScoreForLevel(CurrentLevelIndex) == -1)
        {
            setScoreForLevel(CurrentLevelIndex,currentScore);
        }

        

        unloadLevel();
        menuManager.loadMenu();

        Save();
    }

    private void setScoreForLevel(int level, int score)
    {
        scoreBoard.Add(level, score);
    }

    public int getScoreForLevel(int level)
    {
        int score = -1;
        if (scoreBoard.TryGetValue(level, out score)) return score;
        return -1;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
    }

    public void RespawnBall ()
    {
        currentScore = 0;
        if(currentBall)
            currentBall.Kill();
        currentBall = Instantiate<golfball>(BallClass, getCurrentLevel().start.transform.position, Quaternion.identity);
    }

    public golfball getCurrentBall()
    {
        return currentBall;
    }

    public void addHit()
    {
        currentScore++;
    }

    public int getCurrentScore()
    {
        return currentScore;
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/arDATA.dat");

        PlayerData data = new PlayerData();
        data.scoreBoard = scoreBoard;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/arDATA.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/arDATA.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            scoreBoard = data.scoreBoard;
        }
    }

    [Serializable]
    class PlayerData
    {
        public Dictionary<int, int> scoreBoard = new Dictionary<int, int>();
    }
}
