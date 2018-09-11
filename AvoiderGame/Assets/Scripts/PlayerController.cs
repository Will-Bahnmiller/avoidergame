using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int playerNumber = 1;
    public int score = 0;

    public KeyCode moveKeyUp;
    public KeyCode moveKeyDown;
    public KeyCode moveKeyLeft;
    public KeyCode moveKeyRight;

    private GridManager gridManager = null;


    void Start()
    {
        gridManager = GridManager.GetInstance();

        gridManager.RegisterPlayer(this);
    }

    void Update()
    {
        // Handle movement
        if (Input.GetKeyDown(moveKeyUp))
        {
            gridManager.MovePlayer(DIRECTION.UP, this);
        }
        if (Input.GetKeyDown(moveKeyDown))
        {
            gridManager.MovePlayer(DIRECTION.DOWN, this);
        }
        if (Input.GetKeyDown(moveKeyLeft))
        {
            gridManager.MovePlayer(DIRECTION.LEFT, this);
        }
        if (Input.GetKeyDown(moveKeyRight))
        {
            gridManager.MovePlayer(DIRECTION.RIGHT, this);
        }
    }

    public void ObtainPoints(int pointsAmount)
    {
        score += pointsAmount;
    }


} // end of class PlayerController.cpp
