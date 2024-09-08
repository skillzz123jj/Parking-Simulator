using UnityEngine;

public class KeyboardCarController : MonoBehaviour
{
    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentbrakeForce;
    private bool isBraking;

    [SerializeField] private float motorForce, brakeForce, maxSteerAngle;
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        if (!LogitechGSDK.LogiIsConnected(0))
        {
            HandleSteering();
        }
        UpdateWheels();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        verticalInput = Input.GetAxis("Vertical");

        isBraking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        if (CarStates.currentState == "D" && verticalInput > 0 || CarStates.currentState == "R" && verticalInput < 0 || WheelInteraction.GasInput > 0)
        {
            frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
            frontRightWheelCollider.motorTorque = verticalInput * motorForce;
       
        }
        else
        {
            frontLeftWheelCollider.motorTorque = 0;
            frontRightWheelCollider.motorTorque = 0;
        }
        currentbrakeForce = isBraking ? brakeForce : 0f;
        
        ApplyBrake();
    }

    private void ApplyBrake()
    {
        frontRightWheelCollider.brakeTorque = currentbrakeForce;
        frontLeftWheelCollider.brakeTorque = currentbrakeForce;
        rearLeftWheelCollider.brakeTorque = currentbrakeForce;
        rearRightWheelCollider.brakeTorque = currentbrakeForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }
    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}