using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TITLEOPTION
{
    inactive,
    color,
    ready,
    play
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
    public RectTransform playImage;

    private TITLEOPTION currentOption = TITLEOPTION.inactive;
    private VECTORVAR colorOption = VECTORVAR.player_one_color;
    private int currentColorIndex = 0;

    private bool isReady = false;


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
                    inactiveTextHolder.SetActive(false);
                    activeTextHolder.SetActive(true);
                    currentOption = TITLEOPTION.color;
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
                    isReady = !isReady;
                    Debug.Log(isReady);
                    readyImage.GetComponent<Image>().sprite = (isReady) ? readyCheckedImage : readyUncheckedImage;
                }
                if (Input.GetKeyDown(moveKeyUp))
                {
                    currentOption = TITLEOPTION.color;
                    MoveToColor(currentColorIndex);
                }
                if (Input.GetKeyDown(moveKeyDown) && isReady)
                {
                    currentOption = TITLEOPTION.play;
                    MoveToPlay();
                }
                break;

            case TITLEOPTION.play:
                if (Input.GetKeyDown(selectKey))
                {
                    PlayTheGame();
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

    private void MoveToPlay()
    {
        selector.position = playImage.GetComponent<RectTransform>().localPosition;
    }

    private void PlayTheGame()
    {
        ///TODO
    }
}
