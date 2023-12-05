using UnityEngine;

public class DetachOnExit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the exiting object is the collider's trigger
        if (other.CompareTag("Present"))
        {
            if (!other.GetComponent<PresentFall>().isLocked)
            {
                Debug.Log("Present Fell!" + other.name);
                DetachFromParents(other);
            }
        }
    }

    private void DetachFromParents(Collider other)
    {

        other.gameObject.transform.SetParent(null);

    }
}
