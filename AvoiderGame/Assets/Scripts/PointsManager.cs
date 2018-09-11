using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{
    public GameObject pointsPrefab;

    public float offscreenOffset = 0;

    private GridManager gridManager = null;


    void Start()
    {
        gridManager = GridManager.GetInstance();

        // Testing
        SpawnPoint(DIRECTION.LEFT);
    }

    private void SpawnPoint(DIRECTION direction)
    {
        GameObject pointObject = Instantiate(pointsPrefab);
        PointController point = pointObject.GetComponent<PointController>();
        point.Init(direction);

        // Determine starting location
        Vector2 offsetDir = GridManager.DirectionToVector(GridManager.GetOppositeDirection(direction));
        Vector2 baseLocation = gridManager.GetRealLocation(9, 9);

        pointObject.transform.position = baseLocation + offsetDir * offscreenOffset;
    }

} // end of class PointsManager.cpp
