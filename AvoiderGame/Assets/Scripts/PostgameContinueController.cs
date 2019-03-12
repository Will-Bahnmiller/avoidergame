using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostgameContinueController : MonoBehaviour
{
    public KeyCode playerOneSelectKey;
    public KeyCode playerTwoSelectKey;

    public GameObject continueText;
    public string mainMenuSceneName = "title";

    private bool isPlayerOneAlive = false;
    private bool isPlayerTwoAlive = false;

    private bool hasPlayerOneConfirmed = false;
    private bool hasPlayerTwoConfirmed = false;

    private bool showContinue = false;


    void Start()
    {
        continueText.SetActive(false);

        isPlayerOneAlive = (GlobalVariableManager.GetIntegerVariable(INTEGERVAR.is_player_one_alive) == 1);
        isPlayerTwoAlive = (GlobalVariableManager.GetIntegerVariable(INTEGERVAR.is_player_two_alive) == 1);
    }
    
    void Update()
    {
        if (showContinue)
        {
            if (Input.GetKeyDown(playerOneSelectKey) && isPlayerOneAlive)
            {
                ReturnToMainMenu();
            }
            if (Input.GetKeyDown(playerTwoSelectKey) && isPlayerTwoAlive)
            {
                ReturnToMainMenu();
            }
        }
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void PlayerConfirmed(int playerNumber)
    {
        if (playerNumber == 1 && isPlayerOneAlive)
        {
            hasPlayerOneConfirmed = true;
        }
        if (playerNumber == 2 && isPlayerTwoAlive)
        {
            hasPlayerTwoConfirmed = true;
        }

        if ((!isPlayerOneAlive || hasPlayerOneConfirmed) && (!isPlayerTwoAlive || hasPlayerTwoConfirmed))
        {
            showContinue = true;
            continueText.SetActive(true);
        }
    }

}
