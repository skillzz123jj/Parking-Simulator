using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class CarLights : MonoBehaviour
{
    public GameObject leftIndicatorLight;
    public GameObject rightIndicatorLight;
    private bool leftIndicatorOn = false;
    private bool rightIndicatorOn = false;
    private Coroutine leftCoroutine;
    private Coroutine rightCoroutine;
    [SerializeField] AudioSource indicatorSound;
    [SerializeField] AudioSource carHornSource;
    [SerializeField] AudioClip carHorn;



    [SerializeField] List<MeshRenderer> brakeLights = new List<MeshRenderer>();
    [SerializeField] List<MeshRenderer> reverseLights = new List<MeshRenderer>();


    private Inputs inputActions;

    void Awake()
    {
        inputActions = new Inputs();
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

  
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (leftCoroutine != null)
            {
                indicatorSound.Stop();
                StopCoroutine(leftCoroutine);
                leftIndicatorLight.SetActive(false);
            }

            leftIndicatorOn = !leftIndicatorOn;
            rightIndicatorOn = false;
            rightIndicatorLight.SetActive(false); // Ensure the right indicator is off
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
                rightIndicatorLight.SetActive(false);

            }

            rightIndicatorOn = !rightIndicatorOn;
            leftIndicatorOn = false;
            leftIndicatorLight.SetActive(false); // Ensure the left indicator is off
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
                leftIndicatorLight.SetActive(false);
            }

            leftIndicatorOn = !leftIndicatorOn;
            rightIndicatorOn = false;
            rightIndicatorLight.SetActive(false); // Ensure the right indicator is off
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
                rightIndicatorLight.SetActive(false);

            }

            rightIndicatorOn = !rightIndicatorOn;
            leftIndicatorOn = false;
            leftIndicatorLight.SetActive(false); // Ensure the left indicator is off
            if (rightIndicatorOn)
            {
                indicatorSound.Stop();
                rightCoroutine = StartCoroutine(BlinkIndicator(rightIndicatorLight, () => rightIndicatorOn));
            }
        }

    }
    IEnumerator BlinkIndicator(GameObject indicatorLight, System.Func<bool> isIndicatorOn)
    {
        while (isIndicatorOn())
        {
            indicatorSound.Play();
            //indicatorLight.enabled = !indicatorLight.enabled;
            indicatorLight.SetActive(!indicatorLight.activeSelf);
            yield return new WaitForSeconds(0.5f);
        }
        indicatorLight.SetActive(false);
        indicatorSound.Stop();
    }

    private void Honk(InputAction.CallbackContext context)
    {
        carHornSource.PlayOneShot(carHorn);
    }

    void ReverseLights()
    {
        
        if (WheelInteraction.GasInput > 0 && CarStates.currentState == "R")
        {
            foreach (MeshRenderer light in reverseLights)
            {
                light.enabled = true;
            }

        }
        else
        {
            foreach (MeshRenderer light in reverseLights)
            {
                light.enabled = false;
            }


        }

    }
    void Brakelights()
    {
        // float direction = Input.GetAxis("Vertical");


        if (WheelInteraction.BrakeInput > 0)
        {
            foreach (MeshRenderer light in brakeLights)
            {
                light.enabled = false;
            }

        }
        else
        {
            foreach (MeshRenderer light in brakeLights)
            {
                light.enabled = true;
            }


        }
    }
}