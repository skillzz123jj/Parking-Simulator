using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Inputter;

public class WheelMovement : MonoBehaviour
{
    Inputs input = null;

    // Start is called before the first frame update
    void Start()
    {
        input = new Inputs();

    }


    public void Move(InputAction.CallbackContext context)
    {

        Vector2 moveVector = context.ReadValue<Vector2>();
        Debug.Log(moveVector);
    }
    public void Crouch(InputAction.CallbackContext context)
{
    if (context.performed)
    {
        Debug.Log("pressed");
    }


}
}