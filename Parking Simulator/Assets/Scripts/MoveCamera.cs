using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCamera : MonoBehaviour
{
    private Inputs inputActions;
    [SerializeField] float moveSpeed;
    [SerializeField] CinemachineVirtualCamera cameraFront;
    [SerializeField] CinemachineVirtualCamera cameraBack;
    [SerializeField] CinemachineVirtualCamera cameraLeft;
    [SerializeField] CinemachineVirtualCamera cameraRight;
    [SerializeField] CinemachineVirtualCamera rotateCamera;
    [SerializeField] CinemachineVirtualCamera previousCamera;
    [SerializeField] GameObject car;

    [Header("RotateCamera")]
    public float turnSpeed = 5.0f;
    public GameObject player;

    private Transform playerTransform;
    private Vector3 offset;
    [SerializeField] float yOffset;
    [SerializeField] float zOffset;

    void Awake()
    {
        inputActions = new Inputs();

        // Register input action callbacks
        inputActions.SteeringWheel.Triangle.performed += ctx => SwitchCamera(cameraFront);
        inputActions.SteeringWheel.Cross.performed += ctx => SwitchCamera(cameraBack);
        inputActions.SteeringWheel.Square.performed += ctx => SwitchCamera(cameraLeft);
        inputActions.SteeringWheel.Circle.performed += ctx => SwitchCamera(cameraRight);
        inputActions.SteeringWheel.R2.performed += ctx => FlipCar();
    }

    void Start()
    {
        previousCamera = cameraBack;
        playerTransform = player.transform;
        offset = new Vector3(0, yOffset, zOffset);

        // Set initial camera offsets
        SetCameraOffset(cameraFront, offset);
        SetCameraOffset(cameraBack, offset);
        SetCameraOffset(cameraLeft, offset);
        SetCameraOffset(cameraRight, offset);
        SetCameraOffset(rotateCamera, offset);
    }

    void FixedUpdate()
    {
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        rotateCamera.transform.position = playerTransform.position + offset;
        rotateCamera.transform.LookAt(playerTransform.position);
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    // Allows free camera movement vertically and horizontally
    void Update()
    {
        // Debugging key input to ensure the fallback still works
        if (Input.GetKeyDown(KeyCode.Y))
        {
            SwitchCamera(cameraFront);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            SwitchCamera(cameraRight);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            SwitchCamera(cameraLeft);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            SwitchCamera(cameraBack);
        }
    }

    private void SwitchCamera(CinemachineVirtualCamera newCamera)
    {
        if (previousCamera != null)
        {
            previousCamera.Priority = 1;
        }
        newCamera.Priority = 10;
        previousCamera = newCamera;
    }

    private void SetCameraOffset(CinemachineVirtualCamera cam, Vector3 offset)
    {
        var transposer = cam.GetCinemachineComponent<CinemachineTransposer>();
        if (transposer != null)
        {
            transposer.m_FollowOffset = offset;
        }
    }

    private void FlipCar()
    {
        Vector3 currentRotation = car.transform.rotation.eulerAngles;
        car.transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0);
    }
}

