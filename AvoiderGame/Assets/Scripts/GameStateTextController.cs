using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateTextController : MonoBehaviour
{
    public Text readyText;
    public Text finishText;

    
    void Start()
    {
        GameTimeManager.OnGameStart += OnGameStart;
        GameTimeManager.OnGameEnd += OnGameEnd;
    }

    private void OnGameStart()
    {
        readyText.gameObject.SetActive(false);
    }

    private void OnGameEnd()
    {
        finishText.gameObject.SetActive(true);
    }
}
