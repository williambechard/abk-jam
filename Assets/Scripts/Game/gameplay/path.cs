using UnityEngine;

public class path : MonoBehaviour
{
    private GameObject target;
    public Transform waypoint;
    // Start is called before the first frame update
    void Start()
    {
        target = transform.gameObject;
        waypoint = transform;
    }
    // Getter method to provide the waypoint to other scripts
    public Transform GetWaypoint()
    {
        return waypoint;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
