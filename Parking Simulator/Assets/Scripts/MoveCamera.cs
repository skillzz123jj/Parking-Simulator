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

    void Awake()
    {
        inputActions = new Inputs();

        // Register input action callbacks
        inputActions.SteeringWheel.Triangle.performed += ctx => SwitchCamera(cameraFront);
        inputActions.SteeringWheel.Cross.performed += ctx => SwitchCamera(cameraBack);
        inputActions.SteeringWheel.Square.performed += ctx => SwitchCamera(cameraLeft);
        inputActions.SteeringWheel.Circle.performed += ctx => SwitchCamera(cameraRight);
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
            Debug.Log("Key Y pressed - Switching to Front Camera");
            previousCamera.Priority = 1;
            cameraFront.Priority = 10;
            previousCamera = cameraFront;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("Key J pressed - Switching to Right Camera");
            previousCamera.Priority = 1;
            cameraRight.Priority = 10;
            previousCamera = cameraRight;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Key G pressed - Switching to Left Camera");
            previousCamera.Priority = 1;
            cameraLeft.Priority = 10;
            previousCamera = cameraLeft;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("Key H pressed - Switching to Back Camera");
            previousCamera.Priority = 1;
            cameraBack.Priority = 10;
            previousCamera = cameraBack;
        }
    }

    private void SwitchCamera(CinemachineVirtualCamera newCamera)
    {
        Debug.Log($"Switching camera to {newCamera.name}");
        if (previousCamera != null)
        {
            previousCamera.Priority = 1;
        }
        newCamera.Priority = 10;
        previousCamera = newCamera;
    }
}
