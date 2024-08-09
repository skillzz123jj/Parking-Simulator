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

    public string[] gears = new string[] { "P", "R", "N", "D" }; 
    private int currentIndex = 0;

    public Inputs inputActions;


    private void Awake()
    {
        inputActions = new Inputs();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.SteeringWheel.DpadUp.performed += _ => OnNavigateUp();
        inputActions.SteeringWheel.DpadDown.performed += _ => OnNavigateDown();
        inputActions.Keyboard.Park.performed += _ => ChangeGear("P");
        inputActions.Keyboard.Reverse.performed += _ => ChangeGear("R");
        inputActions.Keyboard.Neutral.performed += _ => ChangeGear("N");
        inputActions.Keyboard.Drive.performed += _ => ChangeGear("D");
        inputActions.Keyboard.Scroll.performed += OnMouseScroll;
    }

    private void OnDisable()
    {
        inputActions.SteeringWheel.DpadUp.performed -= _ => OnNavigateUp();
        inputActions.SteeringWheel.DpadDown.performed -= _ => OnNavigateDown();
        inputActions.Keyboard.Park.performed -= _ => ChangeGear("P");
        inputActions.Keyboard.Reverse.performed -= _ => ChangeGear("R");
        inputActions.Keyboard.Neutral.performed -= _ => ChangeGear("N");
        inputActions.Keyboard.Drive.performed -= _ => ChangeGear("D");
        inputActions.Keyboard.Scroll.performed -= OnMouseScroll;

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
    }

    private void OnMouseScroll(InputAction.CallbackContext ctx)
    {
        float scrollValue = ctx.ReadValue<float>();

        if (scrollValue > 0)
        {
            OnNavigateUp();
        }
        else if (scrollValue < 0)
        {
            OnNavigateDown();
        }
    }
    void Start()
    {
        currentState = "P"; 
        ChangeGear(currentState); 
    }

    void ChangeGear(string state)
    {
        currentState = state;
       
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
