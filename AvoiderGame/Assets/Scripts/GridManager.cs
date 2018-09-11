using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DIRECTION
{
    UP,
    DOWN,
    LEFT,
    RIGHT
};

public class GridLocation
{
    public GridLocation(int r = 0, int c = 0)
    {
        row = r;
        col = c;
    }

    public int row;
    public int col;
}

public class GridManager : MonoBehaviour
{
    public float moveAmount = 0;
    public int gridSize = 0;

    private static GridManager instance = null;

    private List<List<PlayerController>> grid = new List<List<PlayerController>>();
    private Dictionary<PlayerController, GridLocation> playerPositions = new Dictionary<PlayerController, GridLocation>();


    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            // Initialize grid
            for (int row = 0; row < gridSize; ++row)
            {
                grid.Add(new List<PlayerController>());

                for (int col = 0; col < gridSize; ++col)
                {
                    grid[row].Add(null);
                }
            }
        }
    }

    public static GridManager GetInstance()
    {
        return instance;
    }

    public void RegisterPlayer(PlayerController player)
    {
        if (!playerPositions.ContainsKey(player))
        {
            GridLocation startingLocation = GetStartingPosition(player.playerNumber);

            if (IsLocationAvailable(startingLocation))
            {
                MovePlayerToLocation(player, startingLocation);
            }
        }
    }

    public GridLocation GetStartingPosition(int playerNumber)
    {
        GridLocation position = new GridLocation(-1, -1);

        switch (playerNumber)
        {
            case 1:
                position.row = gridSize-1;
                position.col = 0;
                break;
            case 2:
                position.row = gridSize-1;
                position.col = gridSize-1;
                break;
            case 3:
                position.row = 0;
                position.col = 0;
                break;
            case 4:
                position.row = 0;
                position.col = gridSize-1;
                break;
            default:
                Debug.LogError("GetStartingPosition invalid player number!");
                break;
        }

        return position;
    }

    public void MovePlayer(DIRECTION direction, PlayerController player)
    {
        GridLocation currentPosition = playerPositions[player];
        Vector2 dir = DirectionToVector(direction);

        GridLocation destination = new GridLocation(currentPosition.row + (int)dir.y,
                                                    currentPosition.col + (int)dir.x);

        if (IsLocationAvailable(destination))
        {
            ClearLocation(currentPosition);
            MovePlayerToLocation(player, destination);
        }
    }

    public void ClearLocation(GridLocation location)
    {
        grid[location.row][location.col] = null;
    }

    public void MovePlayerToLocation(PlayerController player, GridLocation location)
    {
        Debug.Log("MOVE " + location.row + "," + location.col);

        // Update grid
        grid[location.row][location.col] = player;
        playerPositions[player] = location;

        // Move player transform to location
        player.transform.position = GetRealLocation(location.row, location.col);
    }

    public bool IsLocationAvailable(GridLocation location)
    {
        bool withinBoundsRow = (location.row < gridSize) && (location.row >= 0);
        bool withinBoundsCol = (location.col < gridSize) && (location.col >= 0);

        if (withinBoundsRow && withinBoundsCol)
        {
            return (grid[location.row][location.col] == null);
        }
        else
        {
            return false;
        }
    }

    public Vector2 GetRealLocation(int row, int col)
    {
        return (new Vector2(col, row) * moveAmount);
    }

    public static Vector2 DirectionToVector(DIRECTION direction)
    {
        Vector2 dir = Vector2.zero;

        switch (direction)
        {
            case DIRECTION.UP:
                dir = Vector2.up;
                break;
            case DIRECTION.DOWN:
                dir = Vector2.down;
                break;
            case DIRECTION.LEFT:
                dir = Vector2.left;
                break;
            case DIRECTION.RIGHT:
                dir = Vector2.right;
                break;
        }

        return dir;
    }

    public static DIRECTION GetOppositeDirection(DIRECTION direction)
    {
        DIRECTION dir = DIRECTION.DOWN;

        switch (direction)
        {
            case DIRECTION.UP:
                dir = DIRECTION.DOWN;
                break;
            case DIRECTION.DOWN:
                dir = DIRECTION.UP;
                break;
            case DIRECTION.LEFT:
                dir = DIRECTION.RIGHT;
                break;
            case DIRECTION.RIGHT:
                dir = DIRECTION.LEFT;
                break;
        }

        return dir;
    }

} // end of class PlayerMovementManager.cpp
