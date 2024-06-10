using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CarRadio : MonoBehaviour
{
    public AudioClip[] soundtrack;
    public Slider volumeSlider;
    private AudioSource audioSource;

    private Inputs inputActions;

    void Awake()
    {
//        DontDestroyOnLoad(transform.gameObject);
        inputActions = new Inputs();
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (!audioSource.playOnAwake)
        {
            audioSource.clip = soundtrack[Random.Range(0, soundtrack.Length)];
            audioSource.Play();
        }

        // Initialize volume slider value
        volumeSlider.value = audioSource.volume;
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = soundtrack[Random.Range(0, soundtrack.Length)];
            audioSource.Play();
        }
    }

    void OnEnable()
    {
        // Register Slider Events
        volumeSlider.onValueChanged.AddListener(ChangeVolume);

        inputActions.Enable();
        inputActions.SteeringWheel.Plus.performed += ctx => IncreaseVolume();
        inputActions.SteeringWheel.Minus.performed += ctx => DecreaseVolume();
    }

    void OnDisable()
    {
        volumeSlider.onValueChanged.RemoveListener(ChangeVolume);

        inputActions.SteeringWheel.Plus.performed += ctx => IncreaseVolume();
        inputActions.SteeringWheel.Minus.performed += ctx => DecreaseVolume();
        inputActions.Disable();
    }

    void ChangeVolume(float value)
    {
        audioSource.volume = value;
    }

    void IncreaseVolume()
    {
        volumeSlider.value = Mathf.Clamp(volumeSlider.value + 0.1f, 0, 1);
    }

    void DecreaseVolume()
    {
        volumeSlider.value = Mathf.Clamp(volumeSlider.value - 0.1f, 0, 1);
    }
}