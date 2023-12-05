using UnityEngine;

public class MaintainLocalRotation : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Reset the local rotation to identity (no rotation)
        transform.localRotation = Quaternion.identity;

        // Apply the desired rotation to the local rotation
        // You can set your specific rotation here
        transform.Rotate(Vector3.up * Time.deltaTime * 30f); // Rotate around the local Y-axis, adjust as needed
    }
}
