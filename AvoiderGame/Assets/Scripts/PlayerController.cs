using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int playerNumber = 1;

    public KeyCode moveKeyUp;
    public KeyCode moveKeyDown;
    public KeyCode moveKeyLeft;
    public KeyCode moveKeyRight;

    private GridManager gridManager = null;

    private bool canMove = false;
    private bool isValidPlayer = true;

    private UnityAction gameStartAction;
    private UnityAction gameEndAction;

    private int score = 0;
    private INTEGERVAR playerScoreVariable = INTEGERVAR.player_one_score;


    void Awake()
    {
        switch (playerNumber)
        {
            case 1:
                playerScoreVariable = INTEGERVAR.player_one_score;
                break;
            case 2:
                playerScoreVariable = INTEGERVAR.player_two_score;
                break;
        }
    }

    void Start()
    {
        // Remove second player if not in use
        if (playerNumber == 2 && GlobalVariableManager.GetIntegerVariable(INTEGERVAR.player_count) == 1)
        {
            transform.position = new Vector3(10000, 10000, 10000);
            isValidPlayer = false;
        }
        else
        {
            gridManager = GridManager.GetInstance();
            gridManager.RegisterPlayer(this);

            gameStartAction = new UnityAction(OnGameStart);
            gameEndAction = new UnityAction(OnGameEnd);

            GameTimeManager.OnGameStart += gameStartAction;
            GameTimeManager.OnGameEnd += gameEndAction;
        }
    }

    private void OnGameStart()
    {
        canMove = true;
    }

    private void OnGameEnd()
    {
        canMove = false;
    }

    void Update()
    {
        // Handle movement
        if (canMove && isValidPlayer)
        {
            if (Input.GetKeyDown(moveKeyUp))
            {
                gridManager.MovePlayer(DIRECTION.UP, this);
            }
            if (Input.GetKeyDown(moveKeyDown))
            {
                gridManager.MovePlayer(DIRECTION.DOWN, this);
            }
            if (Input.GetKeyDown(moveKeyLeft))
            {
                gridManager.MovePlayer(DIRECTION.LEFT, this);
            }
            if (Input.GetKeyDown(moveKeyRight))
            {
                gridManager.MovePlayer(DIRECTION.RIGHT, this);
            }
        }
    }

    public void ObtainPoints(int pointsAmount)
    {
        score += pointsAmount;
        GlobalVariableManager.SetIntegerVariable(playerScoreVariable, score);
    }

} // end of class PlayerController.cpp
