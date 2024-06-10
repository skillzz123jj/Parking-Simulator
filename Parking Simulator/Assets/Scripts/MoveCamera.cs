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
    [SerializeField] CinemachineVirtualCamera previousCamera;
    [SerializeField] GameObject car;

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
            previousCamera.Priority = 1;
            cameraFront.Priority = 10;
            previousCamera = cameraFront;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            previousCamera.Priority = 1;
            cameraRight.Priority = 10;
            previousCamera = cameraRight;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            previousCamera.Priority = 1;
            cameraLeft.Priority = 10;
            previousCamera = cameraLeft;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            previousCamera.Priority = 1;
            cameraBack.Priority = 10;
            previousCamera = cameraBack;
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

    private void FlipCar()
    {
        Vector3 currentRotation = car.transform.rotation.eulerAngles;

        car.transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0);
    }
}
