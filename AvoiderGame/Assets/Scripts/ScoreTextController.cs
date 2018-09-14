using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextController : MonoBehaviour
{
    public int playerNumber = 1;

    public Text scoreText;
    public GameObject titleText;

    private INTEGERVAR playerVariable = INTEGERVAR.player_one_score;


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
        // Turn off second player score if only one player is playing
        if (playerNumber == 2 && GlobalVariableManager.GetIntegerVariable(INTEGERVAR.player_count) == 1)
        {
            titleText.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        int currentScore = GlobalVariableManager.GetIntegerVariable(playerVariable);

        scoreText.text = currentScore.ToString("00000");
    }
}
