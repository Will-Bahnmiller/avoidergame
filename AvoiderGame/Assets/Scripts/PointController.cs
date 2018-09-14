using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointController : MonoBehaviour
{
    public int scoreAmount = 0;
    public float lifetime = 0;
    public float moveSpeed = 0;
    
    private Vector2 direction = Vector2.zero;
    private Vector3 initialScale;

    private bool isGameActive = true;


    public void Init(DIRECTION directionToMove)
    {
        direction = GridManager.DirectionToVector(directionToMove);
    }

    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        if (isGameActive)
        {
            // Update timer
            if (lifetime <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                lifetime -= Time.deltaTime;
            }

            // Update movement
            Vector2 position = transform.position;
            position += direction * moveSpeed * Time.deltaTime;
            transform.position = position;
        }
        else
        {
            float postGameSeconds = GameTimeManager.GetInstance().finishTimeSeconds;
            float currentSeconds = GameTimeManager.GetInstance().GetCurrentGameTime();
            transform.localScale = initialScale * (1 - (postGameSeconds - currentSeconds)/postGameSeconds);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        
        if (player != null)
        {
            player.ObtainPoints(scoreAmount);

            Destroy(gameObject);
        }
    }

    public void OnGameEnd()
    {
        isGameActive = false;
    }

} // end of class PointController.cpp
