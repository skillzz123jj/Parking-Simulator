using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class Indicators : MonoBehaviour
{
    public GameObject leftIndicatorLight;
    public GameObject rightIndicatorLight;
    private bool leftIndicatorOn = false;
    private bool rightIndicatorOn = false;
    private Coroutine leftCoroutine;
    private Coroutine rightCoroutine;
    [SerializeField] AudioSource indicatorSound;

    [SerializeField] List<MeshRenderer> brakelights = new List<MeshRenderer>();

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
    }

    void OnDisable()
    {
        inputActions.SteeringWheel.L1.performed -= LeftIndicator;
        inputActions.SteeringWheel.R1.performed -= RightIndicator;
        inputActions.Disable();
    }

    void Update()
    {
        Brakelights();

  
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

    void Brakelights()
    {
        // float direction = Input.GetAxis("Vertical");


        if (WheelInteraction.BrakeInput > 0)
        {
            foreach (MeshRenderer light in brakelights)
            {
                light.enabled = false;
            }

        }
        else
        {
            foreach (MeshRenderer light in brakelights)
            {
                light.enabled = true;
            }


        }
    }
}