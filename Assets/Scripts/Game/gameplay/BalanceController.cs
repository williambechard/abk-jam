using UnityEngine;
using UnityEngine.InputSystem;

public class BalanceController : MonoBehaviour
{
    public GameObject target;
    public float tiltSpeed = 0.02f;
    public float maxRotationAngle = 25f;

    private float currentRotationX = 0f;
    private float currentRotationZ = 0f;

    public bool isActive = false;


    private void Start()
    {
        Invoke("Reset", 1);
    }

    public void Reset()
    {
        currentRotationX = 0f;
        currentRotationZ = 0f;
        target.transform.localEulerAngles = new Vector3(currentRotationX, 0f, currentRotationZ);
        isActive = true;
    }

    private void OnBalance(InputValue value)
    {
        if (!GameManager.IsPaused && isActive)
        {
            // Get small mouse movements
            float mouseX = value.Get<Vector2>().x;
            float mouseY = value.Get<Vector2>().y;

            // Invert mouseY for reverse rotation on mouse up and down
            mouseY = -mouseY;

            // Scale down mouse movements for smaller tilts
            float scaledMouseX = mouseX * tiltSpeed;
            float scaledMouseY = mouseY * tiltSpeed;

            TiltTarget(scaledMouseX, scaledMouseY);
        }
    }

    private void TiltTarget(float mouseX, float mouseY)
    {
        if (target != null)
        {
            // Apply tilt and clamp rotation angles
            currentRotationX = Mathf.Clamp(currentRotationX - mouseY, -maxRotationAngle, maxRotationAngle);
            currentRotationZ = Mathf.Clamp(currentRotationZ - mouseX, -maxRotationAngle, maxRotationAngle);

            target.transform.localEulerAngles = new Vector3(currentRotationX, 0f, currentRotationZ);
        }
    }
}
