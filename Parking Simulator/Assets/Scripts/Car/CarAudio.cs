using UnityEngine;
using UnityEngine.InputSystem;

public class CarAudio : MonoBehaviour
{
    [SerializeField] AudioSource engineSound;
    [SerializeField] AudioClip accelerating;
    [SerializeField] AudioClip idle;
    [SerializeField] AudioClip revving;
    [SerializeField] AudioSource carHornSource;
    [SerializeField] AudioClip carHorn;

    float verticalInput;

    private Inputs inputActions;

    void Awake()
    {
        inputActions = new Inputs();
    }

    void OnEnable()
    {
        inputActions.Enable();
        inputActions.SteeringWheel.WheelMiddle.performed += Honk;
        inputActions.Keyboard.Honk.performed += Honk;


    }

    void OnDisable()
    {
        inputActions.SteeringWheel.WheelMiddle.performed -= Honk;
        inputActions.Keyboard.Honk.performed += Honk;
        inputActions.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");

        if (CarStates.currentState == "D")
        {
            Acceleration();

        }
         if (CarStates.currentState == "P" || CarStates.currentState == "N")
        {
            RevCar();
        }
    }

    private void RevCar()
    {
        if (WheelInteraction.GasInput > 0 || verticalInput > 0)
        {
            if (engineSound.clip != revving || !engineSound.isPlaying)
            {
                engineSound.clip = revving;
                engineSound.Play();
            }
        }
        else
        {
            if (engineSound.clip != idle || !engineSound.isPlaying)
            {
                engineSound.clip = idle;
                engineSound.Play();
            }
        }
    }

    private void Acceleration()
    {
        if (WheelInteraction.GasInput > 0 || verticalInput > 0)
        {

            if (engineSound.clip != accelerating || !engineSound.isPlaying)
            {
                engineSound.clip = accelerating;
                engineSound.Play();
            }
        }
        else
        {
            if (engineSound.clip != idle || !engineSound.isPlaying)
            {
                engineSound.clip = idle;
                engineSound.Play();
            }
        }
    }

    private void Honk(InputAction.CallbackContext context)
    {
        carHornSource.PlayOneShot(carHorn);
    }
}
