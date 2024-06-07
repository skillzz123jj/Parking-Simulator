using UnityEngine;

public class CarController : WheelInteraction
{
    public Transform frontLeftWheel;
    public Transform frontRightWheel;
    public Transform rearLeftWheel;
    public Transform rearRightWheel;

    public float maxSteerAngle = 30f;
    public float motorTorque = 1500f;
    public float brakeTorque = 3000f;
    public float wheelSteering;

    [SerializeField] float moveSpeed;
  //  [SerializeField] WheelInteraction wheelInteractionCS;
     [SerializeField] RectTransform steeringWheelUI;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        float initialSteerAngle = WheelInteraction.xAxes * maxSteerAngle;
        steeringWheelUI.localEulerAngles = new Vector3(0, 0, -initialSteerAngle);



    }

    void Update()
    {
        float steer = maxSteerAngle * Input.GetAxis("Horizontal");
        float torque = motorTorque * WheelInteraction.GasInput;//Input.GetAxis("Vertical");

       // Steer(steer);
       // Drive(torque);
       // UpdateWheelPoses();
        UpdateSteeringWheelUI(WheelInteraction.xAxes);
    }

    private void Steer(float steerAngle)
    {
        frontLeftWheel.localEulerAngles = new Vector3(frontLeftWheel.localEulerAngles.x, steerAngle, frontLeftWheel.localEulerAngles.z);
        frontRightWheel.localEulerAngles = new Vector3(frontRightWheel.localEulerAngles.x, steerAngle, frontRightWheel.localEulerAngles.z);
    }

    private void Drive(float torque)
    {
        rb.AddForce(transform.forward * torque);

      //  float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
       float moveZ = torque * moveSpeed * Time.deltaTime;


        transform.Translate(0, 0, moveZ);
    }

    private void UpdateWheelPoses()
    {
        RotateWheel(frontLeftWheel);
        RotateWheel(frontRightWheel);
        RotateWheel(rearLeftWheel);
        RotateWheel(rearRightWheel);
    }

    private void RotateWheel(Transform wheel)
    {
        wheel.Rotate(Vector3.right * rb.velocity.magnitude * Time.deltaTime * 360 / (2 * Mathf.PI * 0.33f)); // 0.33f is the radius of the wheel
    }

    private void UpdateSteeringWheelUI(float steeringInput)
    {
        if (steeringWheelUI != null)
        {
            float uiRotationAngle = wheelSteering * steeringInput;
            steeringWheelUI.localEulerAngles = new Vector3(0, 0, -uiRotationAngle); // Rotate around the Z-axis
        }
    }
}
