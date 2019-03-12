using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextController : MonoBehaviour
{
    public int playerNumber = 1;

    public Text scoreText;
    public GameObject titleText;

    public bool displayName = false;

    private INTEGERVAR playerVariable = INTEGERVAR.player_one_score;
    private bool isPlayerAlive = false;


    void Awake()
    {
        switch (playerNumber)
        {
            case 1:
                playerVariable = INTEGERVAR.player_one_score;
                break;
            case 2:
                playerVariable = INTEGERVAR.player_two_score;
                break;
        }
    }

    void Start()
    {
        // Turn off player's score if not playing
        if (playerNumber == 1)
        {
            isPlayerAlive = (GlobalVariableManager.GetIntegerVariable(INTEGERVAR.is_player_one_alive) == 1);
        }
        else if (playerNumber == 2)
        {
            isPlayerAlive = (GlobalVariableManager.GetIntegerVariable(INTEGERVAR.is_player_two_alive) == 1);
        }
        if (!isPlayerAlive)
        {
            titleText.SetActive(false);
            gameObject.SetActive(false);
        }

        SetScoreText(0);
    }

    void Update()
    {
        if (isPlayerAlive)
        {
            SetScoreText(GlobalVariableManager.GetIntegerVariable(playerVariable));
        }
    }

    private void SetScoreText(int score)
    {
        string scoreStr = score.ToString("00000");
        scoreText.text = (displayName) ? "Player " + playerNumber + ": " + scoreStr : scoreStr;
    }
}
