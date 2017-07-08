using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public GameObject InGameUI;
    public GameObject MenuUI;
    public GameObject PauseUI;

    public Text[] scoresUI;
    public Text currentScore;
    public Text currentLevel;

    // Use this for initialization
    void Start ()
	{
	    GameManager.Instance.menuManager = this;

	    loadMenu();
	}
	
	// Update is called once per frame
	void Update () {
	    if (InGameUI.activeInHierarchy)
	    {
	        currentScore.text = "Hit: " + GameManager.Instance.getCurrentScore();
	    }
        else if (MenuUI.activeInHierarchy)
	    {
            for (int i = 0; i < scoresUI.Length; i++)
            {
                int score = GameManager.Instance.getScoreForLevel(i);
                scoresUI[i].text = "Score: " + (score == -1 ? "-" : score + "");
            }
        }
	}

    public void onLevelButtonClick(int level)
    {
        GameManager.Instance.loadLevel(level);
        currentLevel.text = "Course: " + (level+1);
        unloadMenu();
    }

    public void onPauseButtonClick()
    {
        PauseUI.SetActive(true);
        GameManager.Instance.PauseGame();
    }

    public void onContinueButtonClick()
    {
        PauseUI.SetActive(false);
        GameManager.Instance.UnpauseGame();
    }

    public void OnMenuButtonClick()
    {
        GameManager.Instance.unloadLevel();
        loadMenu();
    }

    public void loadMenu()
    {
        InGameUI.SetActive(false);
        PauseUI.SetActive(false);
        MenuUI.SetActive(true);

        for (int i = 0; i < scoresUI.Length; i++)
        {
            int score = GameManager.Instance.getScoreForLevel(i);
            scoresUI[i].text = "Score: " + (score == -1 ? "-" : score + "");
        }
    }

    public void unloadMenu()
    {
        InGameUI.SetActive(true);
        MenuUI.SetActive(false);
        PauseUI.SetActive(false);
    }

}
