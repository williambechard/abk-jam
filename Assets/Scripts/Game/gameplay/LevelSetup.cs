using Cinemachine;
using System.Collections.Generic;
using UnityEngine;


public class LevelSetup : MonoBehaviour
{
    public GenerateRoom roomGen;
    public BalanceController balanceController;
    public GameObject camera;
    public GameObject presentPREFAB;
    public GameObject santaPREFAB;
    public CinemachineVirtualCamera vcam;

    Vector3 camPosition;
    Vector3 camRotation;

    // Start is called before the first frame update
    void Start()
    {
        camPosition = camera.transform.position;
        camRotation = camera.transform.eulerAngles;
        quickSetup();
    }

    public void quickSetup()
    {
        Debug.Log("quickSetup called");
        //create random rooms
        roomGen.CreateRooms(4);
        //balanceController.Reset();

        //destroy any old santas :)
        GameObject oldSanta = GameObject.FindGameObjectWithTag("Player");
        Destroy(oldSanta);

        //set player position
        GameObject santa = Instantiate(santaPREFAB);
        santa.transform.position = new Vector3(0, 1, 0);
        camera.GetComponent<SimpleCameraFollow>().targetFollow = santa.transform;
        camera.GetComponent<SimpleCameraFollow>().packageContainer = santa.GetComponentInChildren<presentsContainer>().transform;

        //balanceController.target = santa;
        santa.GetComponentInChildren<BalanceController>().Reset();

        camera.transform.position = camPosition;
        camera.transform.eulerAngles = camRotation;

        camera.GetComponent<SimpleCameraFollow>().CameraSetup();
        //setup walk path
        //for each gameobject in roomGeg.Rooms, return the path component and add it to the list of paths
        //yield return new WaitForSeconds(1);
        List<path> paths = new List<path>();
        foreach (GameObject room in roomGen.Rooms)
        {
            paths.Add(room.GetComponentInChildren<path>());
        }

        //remove the first path
        paths.RemoveAt(0);


        santa.GetComponent<WalkPath>().paths = paths;
        santa.GetComponent<WalkPath>().currentPathIndex = 0;
        santa.GetComponent<WalkPath>().Reset();
        GameManager.IsGoalMet = false;
    }



    // Update is called once per frame
    void Update()
    {

    }
}
