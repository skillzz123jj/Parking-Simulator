using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class CarLights : MonoBehaviour
{
    public MeshRenderer leftIndicatorLight;
    public MeshRenderer rightIndicatorLight;
    private bool leftIndicatorOn = false;
    private bool rightIndicatorOn = false;
    private Coroutine leftCoroutine;
    private Coroutine rightCoroutine;
    float verticalInput;
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


    //[SerializeField] List<MeshRenderer> brakeLights = new List<MeshRenderer>();
    //  [SerializeField] List<MeshRenderer> reverseLights = new List<MeshRenderer>();


    private Inputs inputActions;

    void Awake()
    {
        inputActions = new Inputs();
     //   car.GetComponent<MeshRenderer> ().material = GameData.carColor;
        if (GameData.lightsOn)
        {
            underLight.SetActive(true);
            underLight.GetComponent<Light>().color = GameData.lightColor;

        }
        else
        {
            underLight.SetActive(false);
        }
    }

    void OnEnable()
    {
        
        inputActions.Enable();
        inputActions.SteeringWheel.L1.performed += LeftIndicator;
        inputActions.SteeringWheel.R1.performed += RightIndicator;
        inputActions.SteeringWheel.WheelMiddle.performed += Honk;

        

    }

    void OnDisable()
    {
        inputActions.SteeringWheel.L1.performed -= LeftIndicator;
        inputActions.SteeringWheel.R1.performed -= RightIndicator;
        inputActions.SteeringWheel.WheelMiddle.performed -= Honk;
        inputActions.Disable();
    }

    void Update()
    {
        Brakelights();
        ReverseLights();
        verticalInput = Input.GetAxis("Vertical");


        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (leftCoroutine != null)
            {
                indicatorSound.Stop();
                StopCoroutine(leftCoroutine);
                //   leftIndicatorLight.SetActive(false);
                leftIndicatorLight.sharedMaterial = indicatorOffMaterial;
            }

            leftIndicatorOn = !leftIndicatorOn;
            rightIndicatorOn = false;
          //  rightIndicatorLight.SetActive(false); // Ensure the right indicator is off
            if (leftIndicatorOn)
            {
                indicatorSound.Stop();
                leftCoroutine = StartCoroutine(BlinkIndicator(leftIndicatorLight, () => leftIndicatorOn));
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (rightCoroutine != null)
            {
                indicatorSound.Stop();
                StopCoroutine(rightCoroutine);
                //      rightIndicatorLight.SetActive(false);
                rightIndicatorLight.sharedMaterial = indicatorOffMaterial;


            }

            rightIndicatorOn = !rightIndicatorOn;
            leftIndicatorOn = false;
        //    leftIndicatorLight.SetActive(false); // Ensure the left indicator is off
            if (rightIndicatorOn)
            {
                indicatorSound.Stop();
                rightCoroutine = StartCoroutine(BlinkIndicator(rightIndicatorLight, () => rightIndicatorOn));
            }
        }
    }
    private void LeftIndicator(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            if (leftCoroutine != null)
            {
                indicatorSound.Stop();
                StopCoroutine(leftCoroutine);
                // leftIndicatorLight.SetActive(false);
                leftIndicatorLight.GetComponent<MeshRenderer>().material = indicatorOffMaterial;
            }

            leftIndicatorOn = !leftIndicatorOn;
            rightIndicatorOn = false;
            //  rightIndicatorLight.SetActive(false); // Ensure the right indicator is off
            rightIndicatorLight.GetComponent<MeshRenderer>().material = indicatorOffMaterial;

            if (leftIndicatorOn)
            {
                indicatorSound.Stop();
                leftCoroutine = StartCoroutine(BlinkIndicator(leftIndicatorLight, () => leftIndicatorOn));
            }
        }

    }

    private void RightIndicator(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            if (rightCoroutine != null)
            {
                indicatorSound.Stop();
                StopCoroutine(rightCoroutine);
            //    rightIndicatorLight.SetActive(false);
                rightIndicatorLight.GetComponent<MeshRenderer>().material = indicatorOffMaterial;


            }

            rightIndicatorOn = !rightIndicatorOn;
            leftIndicatorOn = false;
 //           leftIndicatorLight.SetActive(false); // Ensure the left indicator is off
            leftIndicatorLight.GetComponent<MeshRenderer>().material = indicatorOffMaterial;

            if (rightIndicatorOn)
            {
                indicatorSound.Stop();
                rightCoroutine = StartCoroutine(BlinkIndicator(rightIndicatorLight, () => rightIndicatorOn));
            }
        }

    }
    IEnumerator BlinkIndicator(MeshRenderer indicator, System.Func<bool> isIndicatorOn)
    {
        
        while (isIndicatorOn())
        {
            indicatorSound.Play();
            //indicatorLight.enabled = !indicatorLight.enabled;
//            indicatorLight.SetActive(!indicatorLight.activeSelf);

            if (indicator.sharedMaterial == indicatorOffMaterial)
            {
                indicator.sharedMaterial = indicatorOnMaterial;
            }
            else
            {
                indicator.sharedMaterial = indicatorOffMaterial;
            }
            yield return new WaitForSeconds(0.5f);
        }
   //     indicatorLight.SetActive(false);
        indicator.sharedMaterial = indicatorOffMaterial;
        indicatorSound.Stop();
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