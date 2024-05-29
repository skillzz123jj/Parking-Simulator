using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections.Generic;

public class WheelVisualizer: MonoBehaviour
{
    private Inputs inputActions;

    private Dictionary<InputAction, GameObject> inputActionMap;

    public GameObject d_PadUp;
    public GameObject d_PadDown;
    public GameObject d_PadLeft;
    public GameObject d_PadRight;
    [SerializeField] GameObject L1, L2, L3, R1, R2, R3, plus, minus, ps, share, options, triangle,
                                square, cross, circle, wheel, wheelMiddle;

    private void Awake()
    {
        inputActions = new Inputs();

        // Initialize the action map
        inputActionMap = new Dictionary<InputAction, GameObject>
        {
            { inputActions.SteeringWheel.DpadUp, d_PadUp },
            { inputActions.SteeringWheel.DpadDown, d_PadDown },
            { inputActions.SteeringWheel.DpadLeft, d_PadLeft },
            { inputActions.SteeringWheel.DpadRight, d_PadRight },

            { inputActions.SteeringWheel.L1, L1 },
            { inputActions.SteeringWheel.L2, L2 },
            { inputActions.SteeringWheel.L3, L3 },
            { inputActions.SteeringWheel.R1, R1 },

            { inputActions.SteeringWheel.R2, R2 },
            { inputActions.SteeringWheel.R3, R3 },
            { inputActions.SteeringWheel.Plus, plus },
            { inputActions.SteeringWheel.Minus, minus },

            { inputActions.SteeringWheel.PS, ps },
            { inputActions.SteeringWheel.Share, share },
            { inputActions.SteeringWheel.Options, options },
            { inputActions.SteeringWheel.Triangle, triangle },

            { inputActions.SteeringWheel.Square, square },
            { inputActions.SteeringWheel.Cross, cross },
            { inputActions.SteeringWheel.Circle, circle },
            { inputActions.SteeringWheel.Wheel, wheel },
            { inputActions.SteeringWheel.WheelMiddle, wheelMiddle }
        };
    }

    private void OnEnable()
    {
        inputActions.Enable();

        // Subscribe to performed and canceled events for each action in the map
        foreach (var entry in inputActionMap)
        {
            entry.Key.performed += context => OnActionPerformed(context, entry.Value);
            entry.Key.canceled += context => OnActionCanceled(context, entry.Value);
        }
    }

    private void OnDisable()
    {
        // Unsubscribe from performed and canceled events for each action in the map
        foreach (var entry in inputActionMap)
        {
            entry.Key.performed -= context => OnActionPerformed(context, entry.Value);
            entry.Key.canceled -= context => OnActionCanceled(context, entry.Value);
        }

        inputActions.Disable();
    }

    private void OnActionPerformed(InputAction.CallbackContext context, GameObject targetObject)
    {
        targetObject.SetActive(true);
    }

    private void OnActionCanceled(InputAction.CallbackContext context, GameObject targetObject)
    {
        targetObject.SetActive(false);
    }
}