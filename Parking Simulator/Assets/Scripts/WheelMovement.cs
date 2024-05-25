using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WheelMovement : MonoBehaviour
{
    [SerializeField] Inputs input;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(InputAction.CallbackContext context)
    {

        Vector2 moveVector = context.ReadValue<Vector2>();
        Debug.Log(moveVector);
    }
}
