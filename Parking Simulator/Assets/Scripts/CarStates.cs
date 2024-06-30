using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class CarStates : MonoBehaviour
{
    [SerializeField] TMP_Text pState;
    [SerializeField] TMP_Text rState;
    [SerializeField] TMP_Text nState;
    [SerializeField] TMP_Text dState;

    [SerializeField] GameObject car;
    [SerializeField] GameObject carLights;

    public static string currentState;

    public string[] gears = new string[] { "P", "R", "N", "D" }; // Array of items to navigate through
    private int currentIndex = 0;

    // Reference to the InputActionAsset
    public Inputs inputActions;


    private void Awake()
    {
        inputActions = new Inputs();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.SteeringWheel.DpadUp.performed += ctx => OnNavigateUp();
        inputActions.SteeringWheel.DpadDown.performed += ctx => OnNavigateDown();
        inputActions.Keyboard.Park.performed += ctx => ChangeGear("P");
        inputActions.Keyboard.Reverse.performed += ctx => ChangeGear("R");
        inputActions.Keyboard.Neutral.performed += ctx => ChangeGear("N");
        inputActions.Keyboard.Drive.performed += ctx => ChangeGear("D");

    }

    private void OnDisable()
    {
        inputActions.SteeringWheel.DpadUp.performed -= ctx => OnNavigateUp();
        inputActions.SteeringWheel.DpadDown.performed -= ctx => OnNavigateDown();
        inputActions.Keyboard.Park.performed -= ctx => ChangeGear("P");
        inputActions.Keyboard.Reverse.performed -= ctx => ChangeGear("R");
        inputActions.Keyboard.Neutral.performed -= ctx => ChangeGear("N");
        inputActions.Keyboard.Drive.performed -= ctx => ChangeGear("D");
        inputActions.Disable();
    }

    private void OnNavigateUp()
    {
        currentIndex = (currentIndex > 0) ? currentIndex - 1 : gears.Length - 1;
        ChangeGear(gears[currentIndex]);
    }

    private void OnNavigateDown()
    {
        currentIndex = (currentIndex < gears.Length - 1) ? currentIndex + 1 : 0;
        ChangeGear(gears[currentIndex]);
        print(gears[currentIndex]);
    }

    void Start()
    {
        currentState = "P"; // Default to Park
        ChangeGear(currentState); // Initialize the default state
    }

    void ChangeGear(string state)
    {
        currentState = state;
       

        // Reset all states
        pState.color = Color.white;
        rState.color = Color.white;
        nState.color = Color.white;
        dState.color = Color.white;

        switch (state)
        {
            case "P":
                ParkState();
                break;
            case "R":
                ReverseState();
                break;
            case "N":
                NeutralState();
                break;
            case "D":
                DriveState();
                break;
        }
    }

    void ParkState()
    {
        // car.GetComponent<TESTCarController>().enabled = true;
      //  carLights.SetActive(true);
        pState.color = Color.red;
    }

    private void ReverseState()
    {
        rState.color = Color.red;
   
    }

    private void NeutralState()
    {
        nState.color = Color.red;
      
    }

    private void DriveState()
    {
        dState.color = Color.red;
     

    }
}
