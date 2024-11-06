using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceMovement : MonoBehaviour
{
    // Speed settings for movement and rotation
    public float moveSpeed = 10f;
    public float rotationSpeed = 50f;

    // Rotation limits for X-axis (pitch)
    public float maxRotationX = 20f;

    // Rotation smoothing factor
    public float rotationSmoothing = 5f;

    // Damping factor for slowing down the object gradually
    public float decelerationRate = 2f;  // How fast the object slows down (higher is faster)

    bool allowMovement = true;
    public bool rotatingUp;
    public bool rotatingDown;
    // Rigidbody reference
    private Rigidbody rb;

    // Variable to store the target X rotation (pitch)
    private float targetRotationX = 0f;

    Coroutine vehicleHitCoroutine;

    [SerializeField] ParticleSystem particle;

    private Inputs inputActions;

    [SerializeField] AudioSource spaceEngineSource;
    [SerializeField] AudioClip spaceEngineIdle;
    [SerializeField] AudioClip spaceEngineMove;




    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationZ;
        inputActions = new Inputs();
     


    }

    void OnEnable()
    {
        inputActions.Enable();

        // Subscribe to both performed and canceled phases for each action
        inputActions.SteeringWheel.R1.performed += VerticalMovementWheelUp;
        inputActions.SteeringWheel.R1.canceled += VerticalMovementWheelUp;
        inputActions.SteeringWheel.L1.performed += VerticalMovementWheelDown;
        inputActions.SteeringWheel.L1.canceled += VerticalMovementWheelDown;

        inputActions.Keyboard.VolumeUp.performed += VerticalMovementWheelUp;
        inputActions.Keyboard.VolumeUp.canceled += VerticalMovementWheelUp;
        inputActions.Keyboard.VolumeDown.performed += VerticalMovementWheelDown;
        inputActions.Keyboard.VolumeDown.canceled += VerticalMovementWheelDown;
    }

    void OnDisable()
    {
        inputActions.SteeringWheel.R1.performed -= VerticalMovementWheelUp;
        inputActions.SteeringWheel.R1.canceled -= VerticalMovementWheelUp;
        inputActions.SteeringWheel.L1.performed -= VerticalMovementWheelDown;
        inputActions.SteeringWheel.L1.canceled -= VerticalMovementWheelDown;

        inputActions.Keyboard.VolumeUp.performed -= VerticalMovementWheelUp;
        inputActions.Keyboard.VolumeUp.canceled -= VerticalMovementWheelUp;
        inputActions.Keyboard.VolumeDown.performed -= VerticalMovementWheelDown;
        inputActions.Keyboard.VolumeDown.canceled -= VerticalMovementWheelDown;

        inputActions.Disable();
        var emission = particle.emission;
        emission.enabled = false;
    }
    float rotateVertical = 0f;

    void FixedUpdate()
    {
        if (allowMovement)
        {
            // Reset vertical rotation if neither up nor down are active
            if (!rotatingUp && !rotatingDown)
            {
                rotateVertical = 0f;
            }

            float rotateHorizontal = Input.GetAxis("Horizontal");

            // Constrain X-axis (pitch) rotation between -20 and 20 degrees based on vertical input
            targetRotationX = Mathf.Lerp(maxRotationX, -maxRotationX, (rotateVertical + 1f) / 2f);

            // Get the current Y rotation (yaw, free rotation around the Y axis)
            float currentRotationY = rb.rotation.eulerAngles.y;

            // Create a target rotation with the constrained X rotation and free Y rotation
            Quaternion targetRotation = Quaternion.Euler(targetRotationX, currentRotationY, 0f);

            // Smoothly rotate towards the target rotation for the X-axis (pitch)
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSmoothing * Time.deltaTime);

            // Rotate freely around the Y-axis (yaw) using Transform.Rotate only while key is pressed
            if (rotateHorizontal != 0f || WheelInteraction.xAxes != 0f)
            {
                transform.Rotate(Vector3.up, rotateHorizontal * rotationSpeed * Time.deltaTime);
                transform.Rotate(Vector3.up, WheelInteraction.xAxes * rotationSpeed * Time.deltaTime);
            }

            // Forward movement using Left Ctrl, Right Ctrl, or Wheel Interaction
            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) || WheelInteraction.GasInput > 0)
            {
                rb.AddForce(transform.forward * moveSpeed, ForceMode.Acceleration);
                var emission = particle.emission;
                emission.enabled = true;
                spaceEngineSource.clip = spaceEngineMove;
            }
            else
            {
                rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, decelerationRate * Time.deltaTime);
                var emission = particle.emission;
                emission.enabled = false;
                spaceEngineSource.clip = spaceEngineIdle;
            }
        }
    }



    private void VerticalMovementWheelDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rotateVertical = -1f; // Move down
            rotatingDown = true;
        }
        else if (context.canceled)
        {
            rotatingDown = false;
        }
    }

    private void VerticalMovementWheelUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rotateVertical = 1f; // Move up
            rotatingUp = true;
        }
        else if (context.canceled)
        {
            rotatingUp = false;
        }
    }

    IEnumerator RecoverVehicle(float recoveryTime)
    {
        allowMovement = false;
        gameObject.GetComponent<FlashingLight>().enabled = true;
        var emission = particle.emission;
        emission.enabled = false;

        yield return new WaitForSeconds(recoveryTime);
        rb.constraints = RigidbodyConstraints.FreezeAll;

        yield return new WaitForSeconds(2.0f);
      
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotationZ;
        gameObject.GetComponent<FlashingLight>().enabled = false;
        allowMovement = true;
        vehicleHitCoroutine = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Meteorite"))
        {
            Debug.Log("Hit");
            if (vehicleHitCoroutine == null)
            {
            vehicleHitCoroutine = StartCoroutine(RecoverVehicle(10));

            }
        }
    }


}




