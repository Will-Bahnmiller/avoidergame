using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum POINTTYPE
{
    regular,
    great,
    ultra,
    master,
    MAX
}

public class PointsManager : MonoBehaviour
{
    public GameObject pointsPrefab;
    public GameObject greatPointsPrefab;

    public float offscreenOffset = 0;


    private GridManager gridManager = null;

    private List<PointController> pointsList = new List<PointController>();

    private int waveNumber = 0;
    private float timeUntilSpawn = -1;

    private bool canSpawnPoints = false;


    void Start()
    {
        gridManager = GridManager.GetInstance();

        GameTimeManager.OnGameStart += OnGameStart;
        GameTimeManager.OnGameEnd += OnGameEnd;
    }

    private void SpawnPoint(DIRECTION direction, POINTTYPE type)
    {
        GameObject pointObject = Instantiate(GetPointPrefab(type));
        PointController point = pointObject.GetComponent<PointController>();
        point.Init(direction);

        pointsList.Add(point);

        // Determine what row/column to spawn in
        int row = 0;
        int column = 0;

        switch (direction)
        {
            case DIRECTION.UP:
                row = 0;
                column = Random.Range(0, 10);
                break;
            case DIRECTION.DOWN:
                row = 9;
                column = Random.Range(0, 10);
                break;
            case DIRECTION.LEFT:
                row = Random.Range(0, 10);
                column = 9;
                break;
            case DIRECTION.RIGHT:
                row = Random.Range(0, 10);
                column = 0;
                break;
        }

        // Determine starting location
        Vector2 offsetDir = GridManager.DirectionToVector(GridManager.GetOppositeDirection(direction));
        Vector2 baseLocation = gridManager.GetRealLocation(row, column);

        pointObject.transform.position = baseLocation + offsetDir * offscreenOffset;
    }

    public void OnPointDestroyed(PointController point)
    {
        pointsList.Remove(point);
    }

    void Update()
    {
        if (canSpawnPoints)
        {
            if (timeUntilSpawn < 0)
            {
                ++waveNumber;

                SpawnPoint(GridManager.GetRandomDirection(), PointTypeFunction(waveNumber));

                timeUntilSpawn = TimeFunction(waveNumber);
            }
            else
            {
                timeUntilSpawn -= Time.deltaTime;
            }
        }
    }

    private GameObject GetPointPrefab(POINTTYPE type)
    {
        switch (type)
        {
            case POINTTYPE.regular:
                return pointsPrefab;
            case POINTTYPE.great:
                return greatPointsPrefab;
        }

        Debug.LogError("GetPointPrefab: Trying to get prefab type " + type);
        return null;
    }

    private float TimeFunction(int x)
    {
        return (2 / Mathf.Sqrt(x + 1));
    }

    private POINTTYPE PointTypeFunction(int x)
    {
        POINTTYPE type = POINTTYPE.regular;

        ///TODO do other types here first too
        if (x % 20 == 0)
        {
            type = POINTTYPE.great;
        }

        return type;
    }

    private void OnGameStart()
    {
        canSpawnPoints = true;
    }

    private void OnGameEnd()
    {
        canSpawnPoints = false;

        // Cleanup points
        foreach (PointController point in pointsList)
        {
            point.OnGameEnd();
        }
    }


} // end of class PointsManager.cpp
