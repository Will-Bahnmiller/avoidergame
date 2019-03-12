using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerScores : MonoBehaviour
{
    void Start()
    {
        GlobalVariableManager.SetIntegerVariable(INTEGERVAR.player_one_score, 0);
        GlobalVariableManager.SetIntegerVariable(INTEGERVAR.player_two_score, 0);
    }
}
