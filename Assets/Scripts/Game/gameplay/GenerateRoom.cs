using System.Collections.Generic;
using UnityEngine;

public class GenerateRoom : MonoBehaviour
{
    public GameObject RoomPREFAB;
    public Vector3 startPoint = new Vector3(0, 0, 0);
    public Vector3 currentPoint = new Vector3(0, 0, 0);
    public Vector3 roomSize = new Vector3(15, 0, 15);
    Direction previousDirection = Direction.Forward;
    public List<GameObject> Rooms = new List<GameObject>();
    public GameObject GoalPREFAB;


    public enum Direction
    {
        Forward,
        Back,
        Left,
        Right
    }

    public enum OppositeDirection
    {
        back,
        forward,
        right,
        left,
    }


    public void CreateRooms(int numberOfRooms)
    {

        if (Rooms.Count > 0)
        {
            foreach (GameObject room in Rooms)
            {
                Destroy(room);
            }
        }
        //destroy any previously created rooms
        Rooms.Clear();

        // Create the first room
        CreateRoom(startPoint, Direction.Forward);

        // Repeat for a certain number of rooms
        for (int i = 0; i < numberOfRooms; i++)
        {
            List<Direction> availableDirections = GetAvailableDirections(previousDirection);

            // If there are no available directions, break out of the loop
            if (availableDirections.Count == 0)
            {
                Debug.Log("out of directions");
                break;
            }

            // Randomly choose a direction from the available directions
            int randomIndex = UnityEngine.Random.Range(0, availableDirections.Count);
            Direction newDirection = availableDirections[randomIndex];

            // Update current point based on the chosen direction
            UpdateCurrentPoint(newDirection);

            // Create the room
            CreateRoom(currentPoint, newDirection);

            // Update the previous direction
            previousDirection = newDirection;
        }

        //add in the back wall for the first room
        Rooms[0].GetComponent<RoomManager>().WallsVisibility[3] = false;
        Rooms[0].GetComponent<RoomManager>().SetWalls();

        //instantiate the goal prefab in the last room
        GameObject Goal = Instantiate(GoalPREFAB, Rooms[Rooms.Count - 1].transform.position, Quaternion.identity, Rooms[Rooms.Count - 1].transform);
        //get direction vector from last room to second to last room as a unit vector

        Vector3 direction = (Rooms[Rooms.Count - 1].transform.position - Rooms[Rooms.Count - 2].transform.position).normalized;
        Debug.Log("direction for goal " + direction);

        if (direction == Vector3.forward)
        {
            Goal.transform.Rotate(0, 270, 0);
            Goal.transform.localPosition = new Vector3(0, 0, 3f);
        }
        else if (direction == Vector3.back)
        {
            Goal.transform.Rotate(0, 90, 0);
            Goal.transform.localPosition = new Vector3(0, 0, -3f);
        }
        else if (direction == Vector3.left)
        {
            Goal.transform.Rotate(0, 180, 0);
            Goal.transform.localPosition = new Vector3(-3f, 0, 0);
        }
        else if (direction == Vector3.right)
        {
            Goal.transform.Rotate(0, 0, 0);
            Goal.transform.localPosition = new Vector3(3f, 0, 0);
        }



    }

    private List<Direction> GetAvailableDirections(Direction pDirection)
    {
        List<Direction> availableDirections = new List<Direction>
        {
            Direction.Forward,
            Direction.Back,
            Direction.Left,
            Direction.Right
        };

        switch (pDirection)
        {
            case Direction.Forward:
                availableDirections.Remove(Direction.Back);
                break;
            case Direction.Back:
                availableDirections.Remove(Direction.Forward);
                break;
            case Direction.Left:
                availableDirections.Remove(Direction.Right);
                break;
            case Direction.Right:
                availableDirections.Remove(Direction.Left);
                break;
        }


        for (int j = 0; j < availableDirections.Count; j++)
        {
            Vector3 potentialPosition = GetPotentialPosition(availableDirections[j]);
            for (int k = 0; k < Rooms.Count; k++)
            {
                if (Rooms[k].transform.position == potentialPosition)
                {
                    availableDirections.Remove(availableDirections[j]);
                    j--; // Adjust the index after removal
                    break;
                }
            }
        }

        return availableDirections;
    }

    private Vector3 GetPotentialPosition(Direction direction)
    {
        switch (direction)
        {
            case Direction.Forward:
                return currentPoint + new Vector3(0, 0, roomSize.x);
            case Direction.Back:
                return currentPoint - new Vector3(0, 0, roomSize.x);
            case Direction.Left:
                return currentPoint - new Vector3(roomSize.x, 0, 0);
            case Direction.Right:
                return currentPoint + new Vector3(roomSize.x, 0, 0);
            default:
                return currentPoint;
        }
    }

    private void UpdateCurrentPoint(Direction direction)
    {
        switch (direction)
        {
            case Direction.Forward:
                currentPoint += new Vector3(0, 0, roomSize.x);
                break;
            case Direction.Back:
                currentPoint -= new Vector3(0, 0, roomSize.x);
                break;
            case Direction.Left:
                currentPoint -= new Vector3(roomSize.x, 0, 0);
                break;
            case Direction.Right:
                currentPoint += new Vector3(roomSize.x, 0, 0);
                break;
        }
    }

    public void CreateRoom(Vector3 roomPosition, Direction targetDirection)
    {
        GameObject room = Instantiate(RoomPREFAB, roomPosition, Quaternion.identity);
        room.name = "Room " + Rooms.Count + " " + targetDirection;
        RoomManager RM = room.GetComponent<RoomManager>();
        switch (targetDirection)
        {
            case Direction.Forward:
                room.GetComponent<RoomManager>().WallsVisibility[3] = true;
                if (Rooms.Count > 0)
                    RemoveWall(1, Rooms[Rooms.Count - 1].GetComponent<RoomManager>());
                break;
            case Direction.Back:
                room.GetComponent<RoomManager>().WallsVisibility[1] = true;
                if (Rooms.Count > 0)
                    RemoveWall(3, Rooms[Rooms.Count - 1].GetComponent<RoomManager>());
                break;
            case Direction.Left:
                room.GetComponent<RoomManager>().WallsVisibility[2] = true;
                if (Rooms.Count > 0)
                    RemoveWall(0, Rooms[Rooms.Count - 1].GetComponent<RoomManager>());
                break;
            case Direction.Right:
                room.GetComponent<RoomManager>().WallsVisibility[0] = true;
                if (Rooms.Count > 0)
                    RemoveWall(2, Rooms[Rooms.Count - 1].GetComponent<RoomManager>());

                break;

        }
        RM.SetWalls();
        previousDirection = targetDirection;
        currentPoint = roomPosition;
        Rooms.Add(room);
    }

    public void RemoveWall(int index, RoomManager RM)
    {
        RM.WallsVisibility[index] = true;
        RM.SetWalls();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
