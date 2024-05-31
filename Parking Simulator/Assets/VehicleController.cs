using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class VehicleController : MonoBehaviour
{
    public float maxMotorTorque = 1500f;
    public float maxSteeringAngle = 30f;
    public float brakeTorque = 3000f;


    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;

    public Transform frontLeftTransform;
    public Transform frontRightTransform;
    public Transform rearLeftTransform;
    public Transform rearRightTransform;

    [SerializeField] WheelInteraction wheelInteractionCS;


    private Rigidbody rb;

    private float motorInput;
    private float steeringInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        motorInput = Input.GetAxis("Vertical");
        steeringInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        ApplyDrive();
        ApplyBrake();
        ApplySteering();
        UpdateWheelPoses();
    }

    private void ApplyDrive()
    {
        if (wheelInteractionCS.GasInput > 0)
        {
            float motor = maxMotorTorque * wheelInteractionCS.GasInput;
            frontLeftWheel.motorTorque = motor;
            frontRightWheel.motorTorque = motor;
        }
        else
        {
            frontLeftWheel.motorTorque = 0;
            frontRightWheel.motorTorque = 0;
        }
    }

    private void ApplyBrake()
    {
        if (wheelInteractionCS.BrakeInput > 0)
        {
            float brakeForce = brakeTorque * wheelInteractionCS.BrakeInput;
            frontLeftWheel.brakeTorque = brakeForce;
            frontRightWheel.brakeTorque = brakeForce;
            rearLeftWheel.brakeTorque = brakeForce;
            rearRightWheel.brakeTorque = brakeForce;
        }
        else
        {
            frontLeftWheel.brakeTorque = 0;
            frontRightWheel.brakeTorque = 0;
            rearLeftWheel.brakeTorque = 0;
            rearRightWheel.brakeTorque = 0;
        }
    }

    private void ApplySteering()
    {
        float steering = maxSteeringAngle * wheelInteractionCS.xAxes;
        frontLeftWheel.steerAngle = steering;
        frontRightWheel.steerAngle = steering;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontLeftWheel, frontLeftTransform);
        UpdateWheelPose(frontRightWheel, frontRightTransform);
        UpdateWheelPose(rearLeftWheel, rearLeftTransform);
        UpdateWheelPose(rearRightWheel, rearRightTransform);
    }

    private void UpdateWheelPose(WheelCollider collider, Transform trans)
    {
        Vector3 pos;
        Quaternion quat;
        collider.GetWorldPose(out pos, out quat);
        trans.position = pos;
        trans.rotation = quat;
    }
}
