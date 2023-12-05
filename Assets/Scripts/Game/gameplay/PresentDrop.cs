using System.Collections;
using UnityEngine;

public class PresentDrop : MonoBehaviour
{
    public float delayInSeconds;
    public GameObject presentPREFAB;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InstantiateAfterDelayCoroutine());
    }

    IEnumerator InstantiateAfterDelayCoroutine()
    {
        while (true)
        {
            float elapsedTime = 0f;

            // Continue the loop until the elapsed time is greater than or equal to the delay
            while (elapsedTime < delayInSeconds)
            {
                if (!GameManager.IsPaused)
                {
                    // Increment the elapsed time
                    elapsedTime += Time.deltaTime;
                }

                // Yield each frame
                yield return null;
            }

            // Instantiate the object after the delay
            GameObject present = Instantiate(presentPREFAB, transform.position, Quaternion.identity);
            present.transform.SetParent(transform);
            present.GetComponent<Rigidbody>().isKinematic = true;
            present.GetComponent<PresentFall>().Fall();

            yield return new WaitForEndOfFrame();
        }


    }
}
