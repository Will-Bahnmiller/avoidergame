using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerTextController : MonoBehaviour
{
    public Text timerText;

    void Update()
    {
        float gameTimeSeconds = Mathf.Ceil(GameTimeManager.GetInstance().GetCurrentGameTime());

        int secondsLeft = (int)(gameTimeSeconds) % 60;
        int minutesLeft = (int)(gameTimeSeconds) / 60;

        if (GameTimeManager.GetInstance().GetGameState() == GAMESTATE.postgame)
        {
            timerText.text = "00:00";
        }
        else
        {
            timerText.text = minutesLeft.ToString("00") + ":" + secondsLeft.ToString("00");
        }
    }
}
