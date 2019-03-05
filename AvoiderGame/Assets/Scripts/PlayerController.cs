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

    public SpriteRenderer playerSprite;

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
        // Remove player if not in use
        isValidPlayer = false;
        if (playerNumber == 1)
        {
            isValidPlayer = (GlobalVariableManager.GetIntegerVariable(INTEGERVAR.is_player_one_alive) == 1);
        }
        else if (playerNumber == 2)
        {
            isValidPlayer = (GlobalVariableManager.GetIntegerVariable(INTEGERVAR.is_player_two_alive) == 1);
        }
        if (!isValidPlayer)
        {
            transform.position = new Vector3(10000, 10000, 10000);
        }
        else
        {
            gridManager = GridManager.GetInstance();
            gridManager.RegisterPlayer(this);

            gameStartAction = new UnityAction(OnGameStart);
            gameEndAction = new UnityAction(OnGameEnd);

            GameTimeManager.OnGameStart += gameStartAction;
            GameTimeManager.OnGameEnd += gameEndAction;

            Vector3 playerOneColor = GlobalVariableManager.GetVectorVariable(VECTORVAR.player_one_color);
            Vector3 playerTwoColor = GlobalVariableManager.GetVectorVariable(VECTORVAR.player_two_color);

            if (playerNumber == 1)
            {
                playerSprite.color = new Color(playerOneColor.x, playerOneColor.y, playerOneColor.z);
            }
            else if (playerNumber == 2)
            {
                playerSprite.color = new Color(playerTwoColor.x, playerTwoColor.y, playerTwoColor.z);
            }
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
