using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TITLEOPTION
{
    inactive,
    color,
    ready,
    backout
}

public class TitlePlayerController : MonoBehaviour
{
    public int playerNumber = 1;

    public KeyCode moveKeyUp;
    public KeyCode moveKeyDown;
    public KeyCode moveKeyLeft;
    public KeyCode moveKeyRight;
    public KeyCode selectKey;

    public GameObject inactiveTextHolder;
    public GameObject activeTextHolder;
    public RectTransform selector;
    public List<GameObject> colorImages;
    public Image currentColorImage;
    public GameObject readyImage;
    public Sprite readyCheckedImage;
    public Sprite readyUncheckedImage;
    public GameObject backoutImage;

    public StartGameTextController startGameText;

    private TITLEOPTION currentOption = TITLEOPTION.inactive;
    private VECTORVAR colorOption = VECTORVAR.player_one_color;
    private int currentColorIndex = 0;

    private bool isReady = false;
    private bool isAlive = false;


    void Start()
    {
        activeTextHolder.SetActive(false);

        switch (playerNumber)
        {
            case 1:
                colorOption = VECTORVAR.player_one_color;
                break;
            case 2:
                colorOption = VECTORVAR.player_two_color;
                break;
        }

        MoveToColor(playerNumber-1);
        SelectColor(playerNumber-1);
    }

    void Update()
    {
        switch (currentOption)
        {
            case TITLEOPTION.inactive:
                if (AnyButtonDown())
                {
                    SetPlayerAlive(true);
                }
                break;

            case TITLEOPTION.color:
                if (Input.GetKeyDown(moveKeyLeft))
                {
                    currentColorIndex = Mathf.Max(0, currentColorIndex-1);
                    MoveToColor(currentColorIndex);
                }
                if (Input.GetKeyDown(moveKeyRight))
                {
                    currentColorIndex = Mathf.Min(colorImages.Count-1, currentColorIndex+1);
                    MoveToColor(currentColorIndex);
                }
                if (Input.GetKeyDown(moveKeyDown))
                {
                    currentOption = TITLEOPTION.ready;
                    MoveToReady();
                }
                if (Input.GetKeyDown(selectKey))
                {
                    SelectColor(currentColorIndex);
                }
                break;

            case TITLEOPTION.ready:
                if (Input.GetKeyDown(selectKey))
                {
                    SetPlayerReady(!isReady);
                }
                if (!isReady)
                {
                    if (Input.GetKeyDown(moveKeyUp))
                    {
                        currentOption = TITLEOPTION.color;
                        MoveToColor(currentColorIndex);
                    }
                    if (Input.GetKeyDown(moveKeyDown))
                    {
                        currentOption = TITLEOPTION.backout;
                        MoveToBackout();
                    }
                }
                break;

            case TITLEOPTION.backout:
                if (Input.GetKeyDown(selectKey))
                {
                    SetPlayerAlive(false);
                }
                if (Input.GetKeyDown(moveKeyUp))
                {
                    currentOption = TITLEOPTION.ready;
                    MoveToReady();
                }
                break;
        }
    }

    public bool AnyButtonDown()
    {
        return Input.GetKeyDown(moveKeyUp)
            || Input.GetKeyDown(moveKeyDown)
            || Input.GetKeyDown(moveKeyLeft)
            || Input.GetKeyDown(moveKeyRight)
            || Input.GetKeyDown(selectKey);
    }

    public void Unready()
    {
        if (isReady)
        {
            SetPlayerReady(false);
        }
    }

    public bool IsPlayerAlive()
    {
        return isAlive;
    }

    public Color GetPlayerSelectedColor()
    {
        return currentColorImage.color;
    }

    private void SetPlayerReady(bool value)
    {
        isReady = value;

        readyImage.GetComponent<Image>().sprite = (isReady) ? readyCheckedImage : readyUncheckedImage;

        if (isReady)
        {
            startGameText.PlayerReady();
        }
        else
        {
            startGameText.PlayerUnready();
        }
    }

    private void SetPlayerAlive(bool value)
    {
        isAlive = value;

        inactiveTextHolder.SetActive(!isAlive);
        activeTextHolder.SetActive(isAlive);

        currentOption = (isAlive) ? TITLEOPTION.color : TITLEOPTION.inactive;
        MoveToColor(currentColorIndex);

        if (isAlive)
        {
            startGameText.PlayerActive();
        }
        else
        {
            startGameText.PlayerUnactive();
        }
    }

    private void SelectColor(int colorIndex)
    {
        currentColorImage.color = colorImages[colorIndex].GetComponent<Image>().color;

        Vector3 color = new Vector3();
        color.x = currentColorImage.color.r;
        color.y = currentColorImage.color.g;
        color.z = currentColorImage.color.b;

        GlobalVariableManager.SetVectorVariable(colorOption, color);
    }

    private void MoveToColor(int colorIndex)
    {
        selector.localPosition = colorImages[colorIndex].GetComponent<RectTransform>().localPosition
            + colorImages[colorIndex].transform.parent.localPosition;
    }

    private void MoveToReady()
    {
        selector.localPosition = readyImage.GetComponent<RectTransform>().localPosition;
    }

    private void MoveToBackout()
    {
        selector.localPosition = backoutImage.GetComponent<RectTransform>().localPosition;
    }
}
