using Cinemachine;
using System.Collections;
using UnityEngine;
public class ChangeCamHeight : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;
    private Transform packageContainer;
    private float previousYAngle;

    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    public void StartHeightAdjust(Transform packageC)
    {
        StopAllCoroutines();
        packageContainer = packageC;
        StartCoroutine(AdjustHeight());
    }

    IEnumerator AdjustHeight()
    {
        while (true)
        {
            if (!GameManager.IsPaused)
            {
                yield return new WaitForSeconds(1.5f);

                //adjust package offset Y to the height of the highest package
                float highestPackageY = 0;
                foreach (Transform child in packageContainer)
                {
                    if (child.CompareTag("Present"))
                    {
                        if (child.position.y > highestPackageY)
                        {
                            highestPackageY = child.position.y;
                        }
                    }
                }

                if (highestPackageY > 2.3f)
                {
                    vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x, highestPackageY + 2.3f, vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z);

                }
                else
                {
                    vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x, 6f, vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z);
                }

            }
            yield return new WaitForEndOfFrame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the difference in Y angle
        float deltaYAngle = vcam.m_Follow.eulerAngles.y - previousYAngle;

        // Rotate the camera based on the difference in Y angle around the target
        transform.RotateAround(vcam.m_Follow.position, Vector3.up, deltaYAngle);
        // Update the previous Y angle for the next frame
        previousYAngle = vcam.m_Follow.eulerAngles.y;
    }
}
