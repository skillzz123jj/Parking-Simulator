using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarLights : MonoBehaviour
{
    public MeshRenderer leftIndicatorLightMesh;
    public MeshRenderer rightIndicatorLightMesh;
    private bool leftIndicatorOn = false;
    private bool rightIndicatorOn = false;
    private bool indicatorsActive;
    private Coroutine leftCoroutine;
    private Coroutine rightCoroutine;
    float verticalInput;
    public float horizontalInput;
    [SerializeField] AudioSource indicatorSound;
    [SerializeField] AudioSource carHornSource;
    [SerializeField] AudioClip carHorn;

    [SerializeField] GameObject car;
    [SerializeField] GameObject underLight;


    [SerializeField] Material indicatorOnMaterial;
    [SerializeField] Material indicatorOffMaterial;

    [SerializeField] Material reverseOnMaterial;
    [SerializeField] Material reverseOffMaterial;

    [SerializeField] Material brakeOnMaterial;
    [SerializeField] Material brakeOffMaterial;

    [SerializeField] MeshRenderer brakeLights;
    [SerializeField] MeshRenderer reverseLights;

    private Inputs inputActions;

    void Awake()
    {
        inputActions = new Inputs();
    }

    void OnEnable()
    {
        
        inputActions.Enable();
        inputActions.SteeringWheel.L1.performed += LeftIndicatorInputActions;
        inputActions.SteeringWheel.R1.performed += RightIndicatorInputActions;
        //inputActions.Keyboard.LeftIndicator.performed += LeftIndicator;
        //inputActions.Keyboard.RightIndicator.performed += RightIndicator;
        inputActions.SteeringWheel.WheelMiddle.performed += Honk;

    }

    void OnDisable()
    {
        inputActions.SteeringWheel.L1.performed -= LeftIndicatorInputActions;
        inputActions.SteeringWheel.R1.performed -= RightIndicatorInputActions;
        //inputActions.Keyboard.LeftIndicator.performed -= LeftIndicator;
        //inputActions.Keyboard.RightIndicator.performed += RightIndicator;
        inputActions.SteeringWheel.WheelMiddle.performed -= Honk;
        inputActions.Disable();
    }

    void Update()
    {
        Brakelights();
        ReverseLights();
        IndicatorsWithKeyboard();
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        if (GameData.Instance.LevelFinished)
        {
            this.enabled = false;
        }

    }
    private void LeftIndicatorInputActions(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            LeftIndicator();
        }

    }
    private float lastHorizontalInput = 0f; // Track the previous state of horizontal input

    private void IndicatorsWithKeyboard()
    {
        if (horizontalInput == 1 && lastHorizontalInput != 1 && rightCoroutine == null)
        {
            indicatorsActive = true;
            RightIndicator();
        }
        else if (horizontalInput == -1 && lastHorizontalInput != -1 && leftCoroutine == null)
        {
            indicatorsActive = true;
            LeftIndicator();
        }
        else if (horizontalInput == 0 && lastHorizontalInput != 0)
        {
            indicatorsActive = false;
            StopIndicators();
        }
        lastHorizontalInput = horizontalInput;
    }

    private void RightIndicatorInputActions(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            RightIndicator();
        }

    }


    private void RightIndicator()
    {
        
            if (rightCoroutine != null)
            {
              //  indicatorSound.Stop();
                StopCoroutine(rightCoroutine);
                rightIndicatorLightMesh.GetComponent<MeshRenderer>().material = indicatorOffMaterial;

            }

            rightIndicatorOn = !rightIndicatorOn;
            leftIndicatorOn = false;
            leftIndicatorLightMesh.GetComponent<MeshRenderer>().material = indicatorOffMaterial;

            if (rightIndicatorOn)
            {
              //  indicatorSound.Stop();
                rightCoroutine = StartCoroutine(BlinkIndicator(rightIndicatorLightMesh, () => rightIndicatorOn));
           }
        

    }

    private void LeftIndicator()
    {
            if (leftCoroutine != null)
            {
              //  indicatorSound.Stop();
                StopCoroutine(leftCoroutine);
                leftIndicatorLightMesh.GetComponent<MeshRenderer>().material = indicatorOffMaterial;
            }

            leftIndicatorOn = !leftIndicatorOn;
            rightIndicatorOn = false;
            rightIndicatorLightMesh.GetComponent<MeshRenderer>().material = indicatorOffMaterial;

            if (leftIndicatorOn)
            {
            //    indicatorSound.Stop();
                leftCoroutine = StartCoroutine(BlinkIndicator(leftIndicatorLightMesh, () => leftIndicatorOn));
            }
        

    }
    IEnumerator BlinkIndicator(MeshRenderer indicatorMesh, System.Func<bool> isIndicatorOn)
    {
        
        while (isIndicatorOn())
        {
          //  indicatorSound.Play();

            if (indicatorMesh.sharedMaterial == indicatorOffMaterial)
            {
                indicatorMesh.sharedMaterial = indicatorOnMaterial;
            }
            else
            {
                indicatorMesh.sharedMaterial = indicatorOffMaterial;

            }
            yield return new WaitForSeconds(0.5f);
        }
        indicatorMesh.sharedMaterial = indicatorOffMaterial;
      //  indicatorSound.Stop();
    }

    private void StopIndicators()
    {
        // Stop both coroutines and turn off the lights
        if (rightCoroutine != null)
        {
            StopCoroutine(rightCoroutine);
            rightCoroutine = null;
        }

        if (leftCoroutine != null)
        {
            StopCoroutine(leftCoroutine);
            leftCoroutine = null;
        }

        // Turn off both indicator lights
        rightIndicatorOn = false;
        leftIndicatorOn = false;
        rightIndicatorLightMesh.GetComponent<MeshRenderer>().material = indicatorOffMaterial;
        leftIndicatorLightMesh.GetComponent<MeshRenderer>().material = indicatorOffMaterial;

     //   indicatorSound.Stop();
    }


    private void Honk(InputAction.CallbackContext context)
    {
        carHornSource.PlayOneShot(carHorn);
    }

    void ReverseLights()
    {

        if (CarStates.currentState == "R" && WheelInteraction.GasInput > 0 || verticalInput < 0)
        {
            reverseLights.GetComponent<MeshRenderer>().sharedMaterial = reverseOnMaterial;

        }
        else
        {
            reverseLights.GetComponent<MeshRenderer>().sharedMaterial = reverseOffMaterial;
        }

    }
    void Brakelights()
    {
      
        if (WheelInteraction.BrakeInput > 0 || Input.GetKey(KeyCode.Space))
        {
            brakeLights.GetComponent<MeshRenderer>().sharedMaterial = brakeOnMaterial;

        }
        else
        {
            brakeLights.GetComponent<MeshRenderer>().sharedMaterial = brakeOffMaterial;

        }
    }
}