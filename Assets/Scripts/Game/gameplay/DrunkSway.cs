using System.Collections;
using UnityEngine;

public class DrunkSway : MonoBehaviour
{
    public float rotationSpeed = 30f; // Adjust the speed of the rotation
    public float xAxisLimit = 30f; // Adjust the limit for X-axis rotation
    public float zAxisLimit = 30f; // Adjust the limit for Z-axis rotation

    void Start()
    {
        StartCoroutine(DrunkenRotate());
    }

    IEnumerator DrunkenRotate()
    {
        while (true)
        {
            // Rotate positively on the X axis
            float elapsedTimeX = 0f;
            Quaternion startRotationX = transform.rotation;
            Quaternion targetRotationX = Quaternion.Euler(xAxisLimit, 0f, 0f);

            while (elapsedTimeX < 2f)
            {
                transform.rotation = Quaternion.Lerp(startRotationX, targetRotationX, elapsedTimeX / 2f);
                elapsedTimeX += Time.deltaTime * rotationSpeed;
                yield return null;
            }

            // Rotate negatively on the X axis
            float elapsedTimeNegX = 0f;
            Quaternion startRotationNegX = transform.rotation;
            Quaternion targetRotationNegX = Quaternion.Euler(-xAxisLimit, 0f, 0f);

            while (elapsedTimeNegX < 2f)
            {
                transform.rotation = Quaternion.Lerp(startRotationNegX, targetRotationNegX, elapsedTimeNegX / 2f);
                elapsedTimeNegX += Time.deltaTime * rotationSpeed;
                yield return null;
            }

            // Rotate positively on the Z axis
            float elapsedTimeZ = 0f;
            Quaternion startRotationZ = transform.rotation;
            Quaternion targetRotationZ = Quaternion.Euler(0f, 0f, zAxisLimit);

            while (elapsedTimeZ < 2f)
            {
                transform.rotation = Quaternion.Lerp(startRotationZ, targetRotationZ, elapsedTimeZ / 2f);
                elapsedTimeZ += Time.deltaTime * rotationSpeed;
                yield return null;
            }

            // Rotate negatively on the Z axis
            float elapsedTimeNegZ = 0f;
            Quaternion startRotationNegZ = transform.rotation;
            Quaternion targetRotationNegZ = Quaternion.Euler(0f, 0f, -zAxisLimit);

            while (elapsedTimeNegZ < 2f)
            {
                transform.rotation = Quaternion.Lerp(startRotationNegZ, targetRotationNegZ, elapsedTimeNegZ / 2f);
                elapsedTimeNegZ += Time.deltaTime * rotationSpeed;
                yield return null;
            }
        }
    }
}
