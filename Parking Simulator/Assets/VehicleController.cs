using System.ComponentModel;
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

    public bool reverese;

    public bool isCarMoving;

    private const float stoppedThreshold = 0.1f;
    private float stoppedDurationThreshold = 1.0f; // Time in seconds to consider the car stopped
    private float stoppedTimer = 0f;

    Vector3 previousPosition;

  //  [SerializeField] WheelInteraction wheelInteractionCS;

    [SerializeField] AudioSource engineSound;
    [SerializeField] AudioClip accelerating;
    [SerializeField] AudioClip idle;



    private Rigidbody rb;

    private float motorInput;
    private float steeringInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        previousPosition = transform.position;
    }

    private void Update()
    {
        motorInput = Input.GetAxis("Vertical");
        steeringInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.R))
        {
            reverese = !reverese;
        }
    }

    private void FixedUpdate()
    {
        if (reverese)
        {
            ApplyReverse();

        }
        else
        {
             ApplyDrive();

        }
        ApplyBrake();


        ApplySteering();
        UpdateWheelPoses();

        if (Vector3.Distance(transform.position, previousPosition) < stoppedThreshold)
        {
            // Increment timer if car is within the threshold
            stoppedTimer += Time.deltaTime;
            if (stoppedTimer >= stoppedDurationThreshold)
            {
                // Car is considered stopped after the duration threshold
                isCarMoving = false;
            }
        }
        else
        {
            // Reset the timer if the car has moved beyond the threshold
            stoppedTimer = 0f;
            isCarMoving = true;
        }

        // Update previousPosition to the current position for the next frame
        previousPosition = transform.position;
    }

    private void ApplyReverse()
    {

        // Check if the car is stationary and the brake input is applied
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
      
        
        //else
        //{
        //    // Ensure no reverse torque is applied if not braking while stationary
        //    if (wheelInteractionCS.GasInput == 0)
        //    {
        //        frontLeftWheel.motorTorque = 0;
        //        frontRightWheel.motorTorque = 0;
        //    }
        //}
    }

    private bool IsReversing()
    {
        // Check if the car is currently reversing
        return frontLeftWheel.motorTorque > 0 || frontRightWheel.motorTorque > 0;
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
        //float motor = maxMotorTorque * motorInput;

        //frontLeftWheel.motorTorque = motor;
        //frontRightWheel.motorTorque = motor;
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
        float steering = maxSteeringAngle * steeringInput;//wheelInteractionCS.xAxes;
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
