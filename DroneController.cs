using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DroneController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveForce = 15f;
    [SerializeField] private float liftForce = 10f;
    [SerializeField] private float rotationSpeed = 100f;

    [Header("Stabilization")]
    [SerializeField] private float stabilizationForce = 5f;
    [SerializeField] private float maxTiltAngle = 45f;

    [Header("Throttle")]
    [SerializeField] private float throttleSensitivity = 0.5f;
    [SerializeField] private float maxThrottle = 15f;

    private Rigidbody rb;
    private float currentThrottle;

    void Start() => rb = GetComponent<Rigidbody>();

    void FixedUpdate()
    {
        HandleMovement();
        StabilizeDrone();
        ClampThrottle();
    }

    private void HandleMovement()
    {
        // Horizontal movement
        Vector3 moveInput = new Vector3(
            Input.GetAxis("Horizontal"), 
            0, 
            Input.GetAxis("Vertical")
        );
        rb.AddRelativeForce(moveInput * moveForce);

        // Rotation
        if (Input.GetMouseButton(1)) // Right-click hold
        {
            float mouseX = Input.GetAxis("Mouse X");
            rb.AddTorque(Vector3.up * mouseX * rotationSpeed);
        }

        // Throttle
        if (Input.GetKey(KeyCode.Q)) currentThrottle += throttleSensitivity;
        if (Input.GetKey(KeyCode.E)) currentThrottle -= throttleSensitivity;
        rb.AddRelativeForce(Vector3.up * currentThrottle * liftForce);
    }

    private void StabilizeDrone()
    {
        if (Vector3.Angle(transform.up, Vector3.up) > maxTiltAngle)
        {
            Vector3 correctionTorque = Vector3.Cross(transform.up, Vector3.up);
            rb.AddTorque(correctionTorque * stabilizationForce, ForceMode.Acceleration);
        }
    }

    private void ClampThrottle() => currentThrottle = Mathf.Clamp(currentThrottle, 0, maxThrottle);
}
