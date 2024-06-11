using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class CarRadio : MonoBehaviour
{
    public AudioClip[] soundtrack;
    public Slider volumeSlider;
    private AudioSource audioSource;
    public bool isRadioOn;
    private Inputs inputActions;
    [SerializeField] TMP_Text radioStatus;
    [SerializeField] TMP_Text songName;

    [SerializeField] private float fadeOutTime = 2.0f;
    private Coroutine fadeOutCoroutine;
    private float lastInputTime;
    [SerializeField] private float idleTimeToFadeOut = 5.0f;

    void Awake()
    {
        inputActions = new Inputs();
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = soundtrack[Random.Range(0, soundtrack.Length)];
        audioSource.mute = true; // Mute the audio on startup
        isRadioOn = false; // Set radio off on startup
        radioStatus.text = "Radio Off"; // Display "Radio Off" text

        volumeSlider.value = audioSource.volume;
        lastInputTime = Time.time;
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = soundtrack[Random.Range(0, soundtrack.Length)];
            audioSource.Play();
            UpdateSongName();
        }

        // Check for idle time to start fade out
        if (Time.time - lastInputTime >= idleTimeToFadeOut)
        {
            StartFadeOut();
        }
    }

    void OnEnable()
    {
        // Register Slider Events
        volumeSlider.onValueChanged.AddListener(ChangeVolume);

        inputActions.Enable();
        inputActions.SteeringWheel.Plus.performed += ctx => IncreaseVolume();
        inputActions.SteeringWheel.Minus.performed += ctx => DecreaseVolume();
        inputActions.SteeringWheel.DpadLeft.performed += ctx => ToggleRadio();
        inputActions.SteeringWheel.DpadRight.performed += ctx => ToggleRadio();
    }

    void OnDisable()
    {
        volumeSlider.onValueChanged.RemoveListener(ChangeVolume);

        inputActions.SteeringWheel.Plus.performed -= ctx => IncreaseVolume();
        inputActions.SteeringWheel.Minus.performed -= ctx => DecreaseVolume();
        inputActions.SteeringWheel.DpadLeft.performed -= ctx => ToggleRadio();
        inputActions.SteeringWheel.DpadRight.performed -= ctx => ToggleRadio();
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

    private void ToggleRadio()
    {
        isRadioOn = !isRadioOn;
        radioStatus.text = isRadioOn ? "Radio On" : "Radio Off";

        if (isRadioOn)
        {
            audioSource.mute = false;
            audioSource.Play();
            UpdateSongName();
        }
        else
        {
            audioSource.mute = true;
            audioSource.Pause();
        }

        lastInputTime = Time.time;
        StopFadeOut();
        radioStatus.alpha = 1.0f; // Reset text alpha
    }

    private void UpdateSongName()
    {
        if (isRadioOn)
        {

            songName.text = audioSource.clip.name;
            songName.alpha = 1.0f;
            StartCoroutine(FadeOutSongName());
        }
    }

    private void StartFadeOut()
    {
        if (fadeOutCoroutine == null)
        {
            fadeOutCoroutine = StartCoroutine(FadeOutText());
        }
    }

    private void StopFadeOut()
    {
        if (fadeOutCoroutine != null)
        {
            StopCoroutine(fadeOutCoroutine);
            fadeOutCoroutine = null;
        }
    }

    private IEnumerator FadeOutText()
    {
        float startAlpha = radioStatus.alpha;
        for (float t = 0; t < fadeOutTime; t += Time.deltaTime)
        {
            radioStatus.alpha = Mathf.Lerp(startAlpha, 0, t / fadeOutTime);
            yield return null;
        }

        radioStatus.alpha = 0;
        fadeOutCoroutine = null;
    }

    private IEnumerator FadeOutSongName()
    {
        float startAlpha = songName.alpha;
        yield return new WaitForSeconds(idleTimeToFadeOut); // Wait for the idle time
        for (float t = 0; t < fadeOutTime; t += Time.deltaTime)
        {
            songName.alpha = Mathf.Lerp(startAlpha, 0, t / fadeOutTime);
            yield return null;
        }

        songName.alpha = 0;
    }
}


