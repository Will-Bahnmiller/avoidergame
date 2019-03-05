using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum INTEGERVAR
{
    player_count,
    is_player_one_alive,
    is_player_two_alive,
    player_one_score,
    player_two_score,
    MAX
}

public enum VECTORVAR
{
    player_one_color,
    player_two_color,
    MAX
}

public class GlobalVariableManager : MonoBehaviour
{
    void Awake()
    {
        integerVariables.Clear();
        vectorVariables.Clear();

        for (int i = 0; i < (int)(INTEGERVAR.MAX); ++i)
        {
            integerVariables.Add( PlayerPrefs.GetInt(IntegerVarToString((INTEGERVAR)(i))) );
        }
        
        for (int i = 0; i < (int)(VECTORVAR.MAX); ++i)
        {
            float x = PlayerPrefs.GetFloat(VectorVarToString((VECTORVAR)(i), 1));
            float y = PlayerPrefs.GetFloat(VectorVarToString((VECTORVAR)(i), 2));
            float z = PlayerPrefs.GetFloat(VectorVarToString((VECTORVAR)(i), 3));

            vectorVariables.Add(new Vector3(x, y, z));
        }
    }

    void OnDestroy()
    {
        for (int i = 0; i < (int)(INTEGERVAR.MAX); ++i)
        {
            PlayerPrefs.SetInt(IntegerVarToString((INTEGERVAR)(i)), integerVariables[i]);
        }
        for (int i = 0; i < (int)(VECTORVAR.MAX); ++i)
        {
            PlayerPrefs.SetFloat(VectorVarToString((VECTORVAR)(i), 1), vectorVariables[i].x);
            PlayerPrefs.SetFloat(VectorVarToString((VECTORVAR)(i), 2), vectorVariables[i].y);
            PlayerPrefs.SetFloat(VectorVarToString((VECTORVAR)(i), 3), vectorVariables[i].z);
        }

        PlayerPrefs.Save();
    }

    public static string IntegerVarToString(INTEGERVAR variable)
    {
        return ("INT" + ((int)(variable)).ToString());
    }

    public static string VectorVarToString(VECTORVAR variable, int element)
    {
        return ("VEC" + ((int)(variable)).ToString() + element.ToString());
    }

    private static List<int> integerVariables = new List<int>();
    public static int GetIntegerVariable(INTEGERVAR variable)
    {
        if (variable < INTEGERVAR.MAX)
        {
            return integerVariables[(int)(variable)];
        }
        else
        {
            Debug.LogError("GetIntegerVariable: Trying to get MAX index!");
            return 0;
        }
    }

    public static void SetIntegerVariable(INTEGERVAR variable, int value)
    {
        if (variable < INTEGERVAR.MAX)
        {
            integerVariables[(int)variable] = value;
        }
        else
        {
            Debug.LogError("SetIntegerVariable: Trying to get MAX index!");
        }
    }

    private static List<Vector3> vectorVariables = new List<Vector3>();
    public static Vector3 GetVectorVariable(VECTORVAR variable)
    {
        if (variable < VECTORVAR.MAX)
        {
            return vectorVariables[(int)variable];
        }
        else
        {
            Debug.LogError("GetVectorVariable: Trying to get MAX index!");
            return new Vector3();
        }
    }

    public static void SetVectorVariable(VECTORVAR variable, Vector3 value)
    {
        if (variable < VECTORVAR.MAX)
        {
            vectorVariables[(int)variable] = value;
        }
        else
        {
            Debug.LogError("SetVectorVariable: Trying to get MAX index!");
        }
    }

}
