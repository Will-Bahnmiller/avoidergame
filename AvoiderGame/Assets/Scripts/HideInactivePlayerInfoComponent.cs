using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInactivePlayerInfoComponent : MonoBehaviour
{
    public int playerNumber = 1;

    private bool isPlayerAlive = false;


    void Start()
    {
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
            gameObject.SetActive(false);
        }
    }
}
