using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameSelectorController : MonoBehaviour
{
    public int playerNumber = 1;

    public GameObject nameObject;
    public GameObject scoreObject;
    public List<Text> letters;
    public Transform done;
    public Transform selector;
    public GameObject selectorArrows;
    public HighscoresManager highscores;
    public HighscoresTextController highscoresText;
    public PostgameContinueController continueController;

    public KeyCode moveKeyUp;
    public KeyCode moveKeyDown;
    public KeyCode moveKeyLeft;
    public KeyCode moveKeyRight;
    public KeyCode selectKey;

    public string alphabet;
    private char[] alphabetArray;
    
    private int letterIndex = 0;
    private List<int> inputNameIndexes = new List<int>();

    private bool isPlayerAlive = false;
    private int playerScore = -1;
    private Vector3 playerColorVec = new Vector3();
    private Color playerColor;

    private bool hasConfirmed = false;


    void Start()
    {
        inputNameIndexes.Add(0);
        inputNameIndexes.Add(0);
        inputNameIndexes.Add(0);

        alphabetArray = alphabet.ToCharArray();

        if (playerNumber == 1)
        {
            isPlayerAlive = (GlobalVariableManager.GetIntegerVariable(INTEGERVAR.is_player_one_alive) == 1);
            playerScore = GlobalVariableManager.GetIntegerVariable(INTEGERVAR.player_one_score);
            playerColorVec = GlobalVariableManager.GetVectorVariable(VECTORVAR.player_one_color);
        }
        else if (playerNumber == 2)
        {
            isPlayerAlive = (GlobalVariableManager.GetIntegerVariable(INTEGERVAR.is_player_two_alive) == 1);
            playerScore = GlobalVariableManager.GetIntegerVariable(INTEGERVAR.player_two_score);
            playerColorVec = GlobalVariableManager.GetVectorVariable(VECTORVAR.player_two_color);
        }
        playerColor = new Color(playerColorVec.x, playerColorVec.y, playerColorVec.z);
    }

    void Update()
    {
        if (isPlayerAlive && !hasConfirmed)
        {
            // Selector is on a letter
            if (letterIndex < letters.Count)
            {
                if (Input.GetKeyDown(moveKeyUp))
                {
                    // Change current letter down the alphabet
                    int inputNameLetter = inputNameIndexes[letterIndex];
                    --inputNameLetter;
                    if (inputNameLetter < 0)
                    {
                        inputNameLetter = alphabet.Length - 1;
                    }
                    inputNameIndexes[letterIndex] = inputNameLetter;

                    UpdateCurrentLetter();
                }
                if (Input.GetKeyDown(moveKeyDown))
                {
                    // Change current letter up the alphabet
                    int inputNameLetter = inputNameIndexes[letterIndex];
                    ++inputNameLetter;
                    if (inputNameLetter >= alphabet.Length)
                    {
                        inputNameLetter = 0;
                    }
                    inputNameIndexes[letterIndex] = inputNameLetter;

                    UpdateCurrentLetter();
                }

                if (Input.GetKeyDown(moveKeyLeft))
                {
                    --letterIndex;
                    MoveSelector();
                }

                if (Input.GetKeyDown(moveKeyRight))
                {
                    ++letterIndex;
                    MoveSelector();
                }
            }

            // Selector is on done
            else
            {
                if (Input.GetKeyDown(selectKey))
                {
                    // Save name and score
                    string confirmedName = "";
                    for (int i = 0; i < letters.Count; ++i)
                    {
                        confirmedName += alphabet[inputNameIndexes[i]];
                    }
                    highscores.ChangeScoreName(playerScore, "???", confirmedName);
                    highscoresText.UpdateScoreName(confirmedName, playerScore, playerColor);

                    nameObject.SetActive(false);
                    scoreObject.SetActive(false);

                    hasConfirmed = true;
                    continueController.PlayerConfirmed(playerNumber);
                }

                if (Input.GetKeyDown(moveKeyLeft))
                {
                    --letterIndex;
                    MoveSelector();
                }
                if (Input.GetKeyDown(moveKeyRight))
                {
                    letterIndex = 0;
                    MoveSelector();
                }
            }
        }
    }

    private void UpdateCurrentLetter()
    {
        int letter = inputNameIndexes[letterIndex];
        letters[letterIndex].text = "" + alphabetArray[letter];
    }

    private void MoveSelector()
    {
        if (letterIndex < letters.Count)
        {
            selector.transform.localPosition = letters[letterIndex].transform.localPosition;
            selectorArrows.SetActive(true);
        }
        else
        {
            selector.transform.localPosition = done.localPosition;
            selectorArrows.SetActive(false);
        }
    }

}
