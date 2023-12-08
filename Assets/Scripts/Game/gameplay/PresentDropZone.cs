using System.Collections;
using UnityEngine;

public class PresentDropZone : MonoBehaviour
{
    public Transform t;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Present") && !other.GetComponent<Rigidbody>().isKinematic)
        {
            //get position of collision
            Vector3 collisionPoint = other.ClosestPointOnBounds(t.position);

            //move t position Y Up so that it is outside the collision point
            t.position = new Vector3(t.position.x, collisionPoint.y + 1.55f, t.position.z);
        }
    }

    IEnumerator adjustYPosition()
    {
        while (true)
        {
            if (!GameManager.IsPaused && !GameManager.IsGoalMet)
            {
                //shoot a ray down and determine if it hits a present and if that present is not kinematic
                RaycastHit hit;
                if (Physics.Raycast(t.position, Vector3.down, out hit, 10f))
                {
                    if (hit.collider.gameObject.CompareTag("Present") && !hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic)
                    {
                        //get position of collision
                        Vector3 collisionPoint = hit.collider.ClosestPointOnBounds(t.position);

                        //move t position Y Up so that it is outside the collision point
                        t.position = new Vector3(t.position.x, collisionPoint.y + 1.55f, t.position.z);
                    }
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        t = this.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
