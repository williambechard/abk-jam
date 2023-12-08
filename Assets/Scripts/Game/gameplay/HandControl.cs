using UnityEngine;

public class HandControl : MonoBehaviour
{
    public bool isLeftHand;
    public Transform boxTransformL;
    public Transform handBoneL; // Reference to the bone controlling the hand in the rig
    public Transform boxTransformR;
    public Transform handBoneR; // Reference to the bone controlling the hand in the rig
    void Update()
    {
        // Set the hand position to the bottom of the box
        if (isLeftHand)
            transform.position = new Vector3(boxTransformL.position.x, boxTransformL.position.y - boxTransformL.localScale.y / 2, boxTransformL.position.z);
        else
            transform.position = new Vector3(boxTransformR.position.x, boxTransformR.position.y - boxTransformR.localScale.y / 2, boxTransformR.position.z);
        // Optional: You may need to update the rotation of the handBone based on the box's rotation
        if (isLeftHand)
            handBoneL.rotation = boxTransformL.rotation;
        else
            handBoneR.rotation = boxTransformR.rotation;
    }
}
