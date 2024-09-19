using UnityEngine;

public class SpaceMovement : MonoBehaviour
{
    // Speed settings for movement and rotation
    public float moveSpeed = 10f;
    public float rotationSpeed = 50f;

    // Rotation limits for X-axis (pitch)
    public float maxRotationX = 20f;

    // Rotation smoothing factor
    public float rotationSmoothing = 5f;

    // Rigidbody reference
    private Rigidbody rb;

    // Variable to store the target X rotation (pitch)
    private float targetRotationX = 0f;

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Disable gravity (optional for space-like movement)
        rb.useGravity = false;

        // Optional: Freeze Z rotation to prevent rolling
        rb.constraints = RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        // Get input for rotation (based on vertical and horizontal input)
        float rotateVertical = Input.GetAxis("Vertical");    // W/S or Up/Down Arrow keys for pitch
        float rotateHorizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow keys for yaw

        // Constrain X-axis (pitch) rotation between -20 and 20 degrees based on vertical input
        targetRotationX = Mathf.Lerp(maxRotationX, -maxRotationX, (rotateVertical + 1f) / 2f);

        // Get the current Y rotation (yaw, free rotation around the Y axis)
        float currentRotationY = rb.rotation.eulerAngles.y;

        // Create a target rotation with the constrained X rotation and free Y rotation
        Quaternion targetRotation = Quaternion.Euler(targetRotationX, currentRotationY, 0f);

        // Smoothly rotate towards the target rotation for the X-axis (pitch)
        rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSmoothing * Time.deltaTime);

        // Rotate freely around the Y-axis (yaw) using Transform.Rotate only while key is pressed
        if (rotateHorizontal != 0f)
        {
            transform.Rotate(Vector3.up, rotateHorizontal * rotationSpeed * Time.deltaTime);
        }

        // Move the object forward when spacebar is pressed, stop when released
        if (Input.GetKey(KeyCode.Space))
        {
            // Move forward using Rigidbody (adding force in the forward direction)
            rb.AddForce(transform.forward * moveSpeed, ForceMode.Acceleration);
        }
        else
        {
            // Stop forward movement by setting velocity to zero when spacebar is not pressed
            rb.velocity = Vector3.zero;
        }
    }
}


