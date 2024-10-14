using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class VehicleController : MonoBehaviour
{
    public float maxMotorTorque = 1500f;
    public float maxSteeringAngle = 30f;
    public float brakeTorque = 3000f;
    float verticalInput;

    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;

    public Transform frontLeftTransform;
    public Transform frontRightTransform;
    public Transform rearLeftTransform;
    public Transform rearRightTransform;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
        private void FixedUpdate()
    {
        if (GameData.Instance.LevelFinished)
        {
            this.enabled = false;
        }
        if (LogitechGSDK.LogiIsConnected(0) && !GameData.Instance.MenuOpen)
        {
            if (CarStates.currentState == "R")
            {
                ApplyReverse();

            }
            else if (CarStates.currentState == "D")
            {
                ApplyDrive();

            }

            ApplyBrake();
            ApplySteering();
            UpdateWheelPoses();
            verticalInput = Input.GetAxis("Vertical");
        }
       


    }

   
    private void ApplyReverse()
    {
        if (WheelInteraction.GasInput > 0 || verticalInput < 0)
        {
            float motor = maxMotorTorque * WheelInteraction.GasInput;
            frontLeftWheel.motorTorque = -motor;
            frontRightWheel.motorTorque = -motor;
        }
        else
        {
            frontLeftWheel.motorTorque = 0;
            frontRightWheel.motorTorque = 0;
        }
      
    }

    private void ApplyDrive()
    {
        if (WheelInteraction.GasInput > 0 || verticalInput > 0)
        {
            float motor = maxMotorTorque * WheelInteraction.GasInput;
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
        if (WheelInteraction.BrakeInput > 0 || verticalInput < 0)
        {
            float brakeForce = brakeTorque * WheelInteraction.BrakeInput;
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
        float steering = maxSteeringAngle * WheelInteraction.xAxes;
        frontLeftWheel.steerAngle = steering;
        frontRightWheel.steerAngle = steering;
    }

    private void UpdateWheelPose(WheelCollider collider, Transform trans)
    {
        Vector3 pos;
        Quaternion quat;
        collider.GetWorldPose(out pos, out quat);
        trans.position = pos;
        trans.rotation = quat;
    }

    private void UpdateWheelPoses()
    {
        RotateWheel(frontLeftTransform);
        RotateWheel(frontRightTransform);
        RotateWheel(rearLeftTransform);
        RotateWheel(rearRightTransform);
    }

    private void RotateWheel(Transform wheel)
    {
        wheel.Rotate(Vector3.right * rb.velocity.magnitude * Time.deltaTime * 360 / (2 * Mathf.PI * 0.33f)); // 0.33f is the radius of the wheel
    }


}
