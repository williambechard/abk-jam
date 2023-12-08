using System.Collections;
using UnityEngine;

public class SimpleCameraFollow : MonoBehaviour
{
    public Transform targetFollow;
    public float followSpeed = 5f;
    public float rotationSpeed = 5f;

    private Vector3 initialPositionOffset;
    private Quaternion initialRotationOffset;
    private float distanceToTarget;
    private float previousYAngle;
    private Vector3 packagesOffset = new Vector3(0, 0, 0);
    public Transform packageContainer;
    public float moveDuration = 1f; // Duration in seconds

    public Vector3 initRotation;

    public void Start()
    {
        initRotation = transform.localEulerAngles;

    }


    public void CameraSetup()
    {
        StopAllCoroutines();
        if (targetFollow == null)
        {
            Debug.LogError("Target not assigned!");
            return;
        }

        // Calculate the initial position and rotation offsets
        initialPositionOffset = transform.position - targetFollow.position;
        initialRotationOffset = Quaternion.Euler(0f, targetFollow.eulerAngles.y - transform.eulerAngles.y, 0f);

        // Calculate the initial distance to the target
        distanceToTarget = Vector3.Distance(transform.position, targetFollow.position);

        // Initialize the previous Y angle
        previousYAngle = targetFollow.eulerAngles.y;

        StartCoroutine(AdjustHeight());
    }

    public void Reset()
    {

    }

    IEnumerator AdjustHeight()
    {
        while (true)
        {
            if (!GameManager.IsPaused)
            {
                yield return new WaitForSeconds(0.5f);

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
                    StopCoroutine(LerpCameraY(packagesOffset.y, highestPackageY - 2.3f));
                    StartCoroutine(LerpCameraY(packagesOffset.y, highestPackageY - 2.3f));

                }
                else
                {
                    StopCoroutine(LerpCameraY(packagesOffset.y, 0));
                    StartCoroutine(LerpCameraY(packagesOffset.y, 0));
                }

            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator LerpCameraY(float startY, float endY)
    {
        float elapsedTime = 0f;
        Vector3 startPos = new Vector3(0, startY, 0);
        Vector3 endPos = new Vector3(0, endY, 0);
        while (elapsedTime < moveDuration)
        {
            // Calculate the interpolation factor between 0 and 1
            float t = elapsedTime / moveDuration;

            // Use Vector3.Lerp to smoothly move the camera's position
            packagesOffset = Vector3.Lerp(startPos, endPos, t);

            // Increment the elapsed time by the time passed since the last frame
            elapsedTime += Time.deltaTime;

            // Yield each frame
            yield return null;
        }

    }

    void Update()
    {
        if (targetFollow == null)
        {
            return;
        }

        // Smoothly follow the target's position with initial offset and distance
        Vector3 targetPosition = targetFollow.position + initialPositionOffset + packagesOffset - targetFollow.forward * distanceToTarget;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Calculate the difference in Y angle
        float deltaYAngle = targetFollow.eulerAngles.y - previousYAngle;

        // Rotate the camera based on the difference in Y angle around the target
        transform.RotateAround(targetFollow.position, Vector3.up, deltaYAngle);

        // Apply the initial rotation offset
        //transform.rotation *= initialRotationOffset;

        // Update the previous Y angle for the next frame
        previousYAngle = targetFollow.eulerAngles.y;
    }
}
