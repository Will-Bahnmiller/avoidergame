using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoresManager : MonoBehaviour
{
    public int topScoresLimit = 10;

    private List<int> scores = new List<int>();
    private List<string> names = new List<string>();


    void Awake()
    {
        for (int i = 0; i < topScoresLimit; ++i)
        {
            scores.Add(PlayerPrefs.GetInt("HIGHSCOREVAL" + i));
            names.Add(PlayerPrefs.GetString("HIGHSCORENAME" + i));
        }
    }

    void OnDestroy()
    {
        UpdateScores();
    }

    private void UpdateScores()
    {
        for (int i = 0; i < topScoresLimit; ++i)
        {
            PlayerPrefs.SetInt("HIGHSCOREVAL" + i, scores[i]);
            PlayerPrefs.SetString("HIGHSCORENAME" + i, names[i]);
        }
    }

    public int GetScore(int index)
    {
        return scores[index];
    }

    public string GetScoreName(int index)
    {
        return names[index];
    }

    public int SubmitNewScore(int score, string name)
    {
        int scoreNum = -1;

        for (int i = 0; i < topScoresLimit; ++i)
        {
            if (score >= scores[i])
            {
                scoreNum = i;

                scores.Insert(i, score);
                names.Insert(i, name);

                scores.RemoveAt(scores.Count - 1);
                names.RemoveAt(names.Count - 1);

                UpdateScores();

                break;
            }
        }

        return scoreNum;
    }

    public bool ChangeScoreName(int score, string nameBefore, string newName)
    {
        bool nameWasChanged = false;

        for (int i = 0; i < topScoresLimit; ++i)
        {
            if (scores[i] == score && names[i] == nameBefore)
            {
                nameWasChanged = true;
                names[i] = newName;

                UpdateScores();

                break;
            }
        }

        return nameWasChanged;
    }

}
