using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameTextController : MonoBehaviour
{
    public Text readyText;
    public Text countdownText;

    public int countdownDuration = 3;
    public string gameSceneName = "main";

    public TitlePlayerController playerOne;
    public TitlePlayerController playerTwo;

    private bool countdown = false;
    private float countdownTimer = -1;
    private int numPlayersAlive = 0;
    private int numPlayersReady = 0;


    void Start()
    {
        readyText.enabled = false;
        countdownText.enabled = false;
    }

    void Update()
    {
        if (countdown)
        {
            countdownTimer -= Time.deltaTime;

            if (countdownTimer <= 0)
            {
                countdownTimer = 0;
                PlayTheGame();
            }
            else
            {
                UpdateCountdownText();
            }
        }
    }

    private void PlayTheGame()
    {
        GlobalVariableManager.SetIntegerVariable(INTEGERVAR.player_count, numPlayersAlive);
        GlobalVariableManager.SetIntegerVariable(INTEGERVAR.is_player_one_alive, playerOne.IsPlayerAlive() ? 1 : 0);
        GlobalVariableManager.SetIntegerVariable(INTEGERVAR.is_player_two_alive, playerTwo.IsPlayerAlive() ? 1 : 0);

        SceneManager.LoadScene(gameSceneName);
    }

    public void PlayerActive()
    {
        numPlayersAlive = Mathf.Min(2, numPlayersAlive+1);

        UpdateReadyText();
        CancelCountdown();
    }

    public void PlayerUnactive()
    {
        numPlayersAlive = Mathf.Max(0, numPlayersAlive-1);

        UpdateReadyText();
        CancelCountdown();
    }

    public void PlayerReady()
    {
        numPlayersReady = Mathf.Min(2, numPlayersReady+1);
        UpdateReadyText();

        if (numPlayersReady == numPlayersAlive)
        {
            BeginCountdown();
        }
    }

    public void PlayerUnready()
    {
        numPlayersReady = Mathf.Max(0, numPlayersReady-1);
        UpdateReadyText();

        CancelCountdown();
    }

    private void BeginCountdown()
    {
        countdown = true;
        countdownTimer = countdownDuration;
        countdownText.enabled = true;
    }

    private void CancelCountdown()
    {
        countdown = false;
        countdownTimer = countdownDuration;
        countdownText.enabled = false;

        playerOne.Unready();
        playerTwo.Unready();
    }

    private void UpdateReadyText()
    {
        readyText.enabled = (numPlayersAlive > 0);
        readyText.text = "Ready: " + numPlayersReady + "/" + numPlayersAlive;
    }

    private void UpdateCountdownText()
    {
        countdownText.enabled = (numPlayersAlive == numPlayersReady && numPlayersAlive > 0);
        countdownText.text = Mathf.CeilToInt(countdownTimer).ToString();
    }
}
