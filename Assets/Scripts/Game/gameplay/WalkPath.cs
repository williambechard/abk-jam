using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkPath : MonoBehaviour
{
    public List<path> paths = new List<path>();
    public GameObject targetObject;
    public GameObject visualObject;
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;
    public float lerpDuration = 2f;

    private int currentPathIndex = 0;
    private float initialY;
    private float lerpTime = 0f;

    void Start()
    {
        initialY = targetObject.transform.position.y; // Store the initial Y-value
        Invoke("startWalk", 1f);
    }

    void startWalk()
    {
        StartCoroutine(WalkPaths());
    }

    IEnumerator WalkPaths()
    {
        while (currentPathIndex < paths.Count && !GameManager.IsPaused)
        {
            Transform currentWaypoint = paths[currentPathIndex].GetWaypoint();
            Debug.Log("currentWaypoint " + currentWaypoint.position);

            // Lerp between current position and next waypoint
            lerpTime = 0f;
            Vector3 initialPosition = targetObject.transform.position;
            Vector3 targetPosition = new Vector3(currentWaypoint.position.x, initialY, currentWaypoint.position.z);

            // Lerp rotation to face the upcoming path
            Quaternion initialRotation = targetObject.transform.rotation;
            Quaternion targetRotation = Quaternion.LookRotation(targetPosition - initialPosition, Vector3.up);

            while (lerpTime < lerpDuration)
            {
                lerpTime += Time.deltaTime;
                float t = lerpTime / lerpDuration;

                targetObject.transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
                targetObject.transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, t);

                // If you want the visualObject to follow the same lerp
                visualObject.transform.position = targetObject.transform.position;
                // visualObject.transform.rotation = targetObject.transform.rotation;

                yield return null;
            }

            // Ensure the final position and rotation are exactly the target position and rotation
            targetObject.transform.position = targetPosition;
            targetObject.transform.rotation = targetRotation;

            visualObject.transform.position = targetPosition;
            visualObject.transform.rotation = targetRotation;

            // Move to the next waypoint
            currentPathIndex++;

            yield return null;
        }
    }
}
