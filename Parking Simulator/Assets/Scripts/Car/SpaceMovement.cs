using UnityEngine;

public class SpaceMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;  // Speed of forward/backward movement
    public float rotationSpeed = 100.0f;  // Speed of rotation

    void Update()
    {
        // Get input for forward/backward movement (Vertical axis)
        float moveVertical = Input.GetAxis("Vertical");

        // Get input for rotation (Horizontal axis)
        float rotateHorizontal = Input.GetAxis("Horizontal");

        // Move the object forward/backward
        transform.Translate(Vector3.forward * moveVertical * moveSpeed * Time.deltaTime);

        // Rotate the object around the Y axis
        transform.Rotate(Vector3.up, rotateHorizontal * rotationSpeed * Time.deltaTime);
    }
}
