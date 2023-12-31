using System.Collections;
using UnityEngine;

public class PresentFall : MonoBehaviour
{

    public float moveSpeed = 2f;
    private Rigidbody rb;
    bool hasCollided = false;
    public Transform presentsContainer;
    public bool isLocked = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        presentsContainer = GameObject.Find("PresentsContainer").transform;
    }

    public void Fall()
    {
        StartCoroutine(MoveDownCoroutine());
    }

    void OnCollisionEnter(Collision collision)
    {
        if (rb.isKinematic)
        {

            // Check if the collision is not part of the same object
            if (collision.gameObject != gameObject)
            {

                // Collision detected, set rigidbody.isKinematic to false
                rb.isKinematic = false;
                hasCollided = true;


                //unparent
                if (collision.gameObject.CompareTag("Present"))
                    transform.SetParent(presentsContainer);
                else transform.parent = null;


                StopCoroutine(MoveDownCoroutine());
            }
        }
    }

    IEnumerator MoveDownCoroutine()
    {
        while (!hasCollided)
        {
            if (!GameManager.IsPaused)
            {
                // Move the object down in local Y
                transform.Translate(Vector3.down * moveSpeed * Time.deltaTime, Space.Self);
            }

            yield return null; // Yield each frame
        }
    }
}
