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


    [SerializeField] AudioSource engineSound;
    [SerializeField] AudioClip accelerating;
    [SerializeField] AudioClip idle;
    [SerializeField] AudioClip revving;


    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
        private void FixedUpdate()
    {
        if (CarStates.currentState == "R")
        {
            ApplyReverse();

        }
        else if (CarStates.currentState == "D")
        {
             ApplyDrive();

        }
        else if (CarStates.currentState == "P" || CarStates.currentState == "N")
        {
            RevCar();
        }

        ApplyBrake();
        ApplySteering();
        UpdateWheelPoses();
    }

    private void RevCar()
    {
        if (WheelInteraction.GasInput > 0)
        {
            if (engineSound.clip != revving || !engineSound.isPlaying)
            {
                engineSound.clip = revving;
                engineSound.Play();
            }
        }
        else
        {
            if (engineSound.clip != idle || !engineSound.isPlaying)
            {
                engineSound.clip = idle;
                engineSound.Play();
            }
        }
    }
    private void ApplyReverse()
    {
        if (WheelInteraction.GasInput > 0)
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
        if (WheelInteraction.GasInput > 0)
        {
            float motor = maxMotorTorque * WheelInteraction.GasInput;
            if (engineSound.clip != accelerating || !engineSound.isPlaying)
            {
                engineSound.clip = accelerating;
                engineSound.Play();
            }
            frontLeftWheel.motorTorque = motor;
            frontRightWheel.motorTorque = motor;
        }
        else
        {
            if (engineSound.clip != idle || !engineSound.isPlaying)
            {
                engineSound.clip = idle;
                engineSound.Play();
            }
            frontLeftWheel.motorTorque = 0;
            frontRightWheel.motorTorque = 0;
        }
    }

    private void ApplyBrake()
    {
        if (WheelInteraction.BrakeInput > 0)
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

    //private void UpdateWheelPoses()
    //{
    //    UpdateWheelPose(frontLeftWheel, frontLeftTransform);
    //    UpdateWheelPose(frontRightWheel, frontRightTransform);
    //    UpdateWheelPose(rearLeftWheel, rearLeftTransform);
    //    UpdateWheelPose(rearRightWheel, rearRightTransform);
    //}

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
