using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoresTextController : MonoBehaviour
{
    public HighscoresManager highscores;
    public List<Text> scoreTexts;


    void Start()
    {
        bool isPlayerOneAlive = (GlobalVariableManager.GetIntegerVariable(INTEGERVAR.is_player_one_alive) == 1);
        bool isPlayerTwoAlive = (GlobalVariableManager.GetIntegerVariable(INTEGERVAR.is_player_two_alive) == 1);

        int playerOneTopScore = -1;
        int playerTwoTopScore = -1;

        if (isPlayerOneAlive)
        {
            playerOneTopScore = highscores.SubmitNewScore(GlobalVariableManager.GetIntegerVariable(INTEGERVAR.player_one_score), "???");
        }
        if (isPlayerTwoAlive)
        {
            playerTwoTopScore = highscores.SubmitNewScore(GlobalVariableManager.GetIntegerVariable(INTEGERVAR.player_two_score), "???");
        }

        for (int i = 0; i < highscores.topScoresLimit; ++i)
        {
            string scoreStr = highscores.GetScore(i).ToString("00000");
            string scoreNameStr = highscores.GetScoreName(i);
            if (scoreNameStr.Length == 0)
            {
                scoreNameStr = "AAA";
            }
            char[] scoreName = scoreNameStr.ToCharArray();

            scoreTexts[i].text = (i + 1) + ".  " + scoreName[0] + " " + scoreName[1] + " " + scoreName[2] + " - " + scoreStr;
        }

        if (playerOneTopScore >= 0)
        {
            Vector3 colorVec = GlobalVariableManager.GetVectorVariable(VECTORVAR.player_one_color);
            Color color = new Color(colorVec.x, colorVec.y, colorVec.z);
            UpdateScore(playerOneTopScore, GlobalVariableManager.GetIntegerVariable(INTEGERVAR.player_one_score), "???", color);
        }
        if (playerTwoTopScore >= 0)
        {
            Vector3 colorVec = GlobalVariableManager.GetVectorVariable(VECTORVAR.player_two_color);
            Color color = new Color(colorVec.x, colorVec.y, colorVec.z);
            UpdateScore(playerTwoTopScore, GlobalVariableManager.GetIntegerVariable(INTEGERVAR.player_two_score), "???", color);
        }
    }

    public void UpdateScore(int index, int score, string name, Color color)
    {
        scoreTexts[index].text = (index + 1) + ".  " + name[0] + " " + name[1] + " " + name[2] + " - " + score.ToString("00000");
        scoreTexts[index].color = color;
    }

    public void UpdateScoreName(string newName, int score, Color color)
    {
        for (int i = 0; i < scoreTexts.Count; ++i)
        {
            if (scoreTexts[i].text.Contains("? ? ?") && scoreTexts[i].color == color)
            {
                scoreTexts[i].text = (i+1) + ".  " + newName[0] + " " + newName[1] + " " + newName[2] + " - " + score.ToString("00000");
                break;
            }
        }
    }

}
