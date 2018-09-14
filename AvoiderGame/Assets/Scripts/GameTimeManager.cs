using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GAMESTATE
{
    pregame,
    gameplay,
    postgame,
    MAX
}

public class GameTimeManager : MonoBehaviour
{
    public float readyTimeSeconds = 3;
    public float gameTimeSeconds = 60;
    public float finishTimeSeconds = 5;

    public static UnityAction OnGameStart;
    public static UnityAction OnGameEnd;

    private static GameTimeManager instance;

    private GAMESTATE gameState = GAMESTATE.pregame;
    private float gameTimer;
    private bool isTimerActive = true;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public static GameTimeManager GetInstance()
    {
        return instance;
    }

    void Start()
    {
        gameTimer = readyTimeSeconds;
    }

    public GAMESTATE GetGameState()
    {
        return gameState;
    }

    void Update()
    {
        if (isTimerActive)
        {
            if (gameTimer <= 0)
            {
                switch (gameState)
                {
                    case GAMESTATE.pregame:
                        gameTimer = gameTimeSeconds;
                        gameState = GAMESTATE.gameplay;
                        OnGameStart.Invoke();
                        break;
                    case GAMESTATE.gameplay:
                        gameTimer = finishTimeSeconds;
                        gameState = GAMESTATE.postgame;
                        OnGameEnd.Invoke();
                        break;
                    case GAMESTATE.postgame:
                        gameTimer = 0;
                        isTimerActive = false;
                        ///TODO: transition to high scores screen
                        break;
                }
            }
            else
            {
                gameTimer -= Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameTimer = 0;
            }
        }
    }

    public float GetCurrentGameTime()
    {
        switch (gameState)
        {
            case GAMESTATE.gameplay:
            case GAMESTATE.postgame:
                return gameTimer;
            default:
                return gameTimeSeconds;
        }
    }
}
