﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoSingleton<GameController>
{
    public enum EndGameCondition
    {
        Win,
        Lose
    }

    public GameObject Player;
    public GameObject _activeLevel;
    public int _activeLevelNo;
    public int LevelLimit;
    public bool isGameStarted = false;
    

    // Use this for initialization
    void Start ()
	{
        Time.timeScale = 0.75f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UnLoadActiveLevel()
    {
        if (_activeLevel)
        {
            _activeLevel.SetActive(false);
            Destroy(_activeLevel);
            _activeLevel = null;
        }
        Player.SetActive(false);
    }

    public void LoadLevel(int levelNo)
    {
        UnLoadActiveLevel();

        _activeLevelNo = levelNo;
        _activeLevel = Instantiate(Resources.Load("Prefabs/Levels/Level" + levelNo) as GameObject);

        UIController.Instance.SwitchToGameMenu();
        
        StartLevel();
    }

    private void StartLevel()
    {
        UIController.Instance.ShowTapToStartDialogue();
        Player.SetActive(true);
        PlayerController.Instance.Init();
        isGameStarted = false;
    }

    public void NextLevel()
    {
        if (_activeLevelNo + 1 <= LevelLimit)
        {
            LoadLevel(_activeLevelNo + 1);
        }
    }

    public void Retry()
    {
        UnLoadActiveLevel();
        LoadLevel(_activeLevelNo);
    }

    public void EndGame()
    {
        UnLoadActiveLevel();
    }
}
