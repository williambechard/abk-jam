using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public List<GameObject> Walls = new List<GameObject>();

    public List<bool> WallsVisibility = new List<bool>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetWalls()
    {
        for (int i = 0; i < Walls.Count; i++)
        {
            if (WallsVisibility[i])
                Walls[i].SetActive(false);
            else Walls[i].SetActive(true);

        }
    }

}
