using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene
{
    Start,
    Game
}
public class GameManager : Singleton<GameManager>
{

    public Action onGameOverEvent;
    public Scene currentScene;
    public int currentLevel;
    private GameManager() { }

    public int currentBallCount;
    private int deletBallCount;

    public int CurrentBallCount
    {
        get => currentBallCount;
        set
        {
            currentBallCount = value;
            if (currentBallCount <= 0)
            {
                Debug.Log("게임오버");
                onGameOverEvent?.Invoke();
            }
        }
    }

    public int DeletBallCount
    {
        get => deletBallCount;
        set => deletBallCount = value;
    }

    private void Start()
    {
        currentScene = Scene.Start;
        CurrentBallCount = 5;
    }

    public void GameOver()
    {
        onGameOverEvent?.Invoke();
    }

    public void StageLoad(int level)
    {
        currentLevel = level;
        currentScene = Scene.Game;
        SceneManager.LoadScene((int)Scene.Game);
        currentBallCount = 5;
        deletBallCount = 0;
    }

    private void OnApplicationQuit()
    {
        Time.timeScale = 0;
    }
}