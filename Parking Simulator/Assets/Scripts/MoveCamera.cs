using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] CinemachineVirtualCamera cameraFront;
    [SerializeField] CinemachineVirtualCamera cameraBack;

    [SerializeField] CinemachineVirtualCamera cameraLeft;

    [SerializeField] CinemachineVirtualCamera cameraRight;

     [SerializeField] CinemachineVirtualCamera previousCamera;

    void Start()
    {
        previousCamera = cameraBack;
    }
    //Allows free camera movement vertically and horizontally
    void Update()
    {
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
}
