using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarLights : MonoBehaviour
{
    public MeshRenderer leftIndicatorLightMesh;
    public MeshRenderer rightIndicatorLightMesh;
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
        inputActions.Keyboard.LeftIndicator.performed += LeftIndicator;
        inputActions.Keyboard.RightIndicator.performed += RightIndicator;
        inputActions.SteeringWheel.WheelMiddle.performed += Honk;

    }

    void OnDisable()
    {
        inputActions.SteeringWheel.L1.performed -= LeftIndicator;
        inputActions.SteeringWheel.R1.performed -= RightIndicator;
        inputActions.Keyboard.LeftIndicator.performed -= LeftIndicator;
        inputActions.Keyboard.RightIndicator.performed += RightIndicator;
        inputActions.SteeringWheel.WheelMiddle.performed -= Honk;
        inputActions.Disable();
    }

    void Update()
    {
        Brakelights();
        ReverseLights();
        verticalInput = Input.GetAxis("Vertical");
        if (GameData.Instance.LevelFinished)
        {
            this.enabled = false;
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
                leftIndicatorLightMesh.GetComponent<MeshRenderer>().material = indicatorOffMaterial;
            }

            leftIndicatorOn = !leftIndicatorOn;
            rightIndicatorOn = false;
            rightIndicatorLightMesh.GetComponent<MeshRenderer>().material = indicatorOffMaterial;

            if (leftIndicatorOn)
            {
                indicatorSound.Stop();
                leftCoroutine = StartCoroutine(BlinkIndicator(leftIndicatorLightMesh, () => leftIndicatorOn));
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
                rightIndicatorLightMesh.GetComponent<MeshRenderer>().material = indicatorOffMaterial;

            }

            rightIndicatorOn = !rightIndicatorOn;
            leftIndicatorOn = false;
            leftIndicatorLightMesh.GetComponent<MeshRenderer>().material = indicatorOffMaterial;

            if (rightIndicatorOn)
            {
                indicatorSound.Stop();
                rightCoroutine = StartCoroutine(BlinkIndicator(rightIndicatorLightMesh, () => rightIndicatorOn));
            }
        }

    }
    IEnumerator BlinkIndicator(MeshRenderer indicatorMesh, System.Func<bool> isIndicatorOn)
    {
        
        while (isIndicatorOn())
        {
            indicatorSound.Play();

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