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

    public void Init(DIRECTION directionToMove)
    {
        direction = GridManager.DirectionToVector(directionToMove);
    }

    void Update()
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

    public void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        
        if (player != null)
        {
            player.ObtainPoints(scoreAmount);

            Destroy(gameObject);
        }
    }

} // end of class PointController.cpp
