using UnityEngine;
using UnityEngine.UI;
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

    private Coroutine fadeOutSliderCoroutine;
    [SerializeField] private float sliderFadeOutTime = 5.0f; 

    private int currentSongIndex = 0;

    [SerializeField] CanvasGroup sliderCanvasGroup;

    void Awake()
    {
        inputActions = new Inputs();
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = soundtrack[currentSongIndex];
        audioSource.mute = true; // Mute the audio on startup
        isRadioOn = false; // Set radio off on startup
        radioStatus.text = "Radio Off"; // Display "Radio Off" text

        volumeSlider.value = audioSource.volume;
        lastInputTime = Time.time;
    }

    void Update()
    {
        if (!audioSource.isPlaying && isRadioOn)
        {
            PlayNextSong();
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

        inputActions.Keyboard.VolumeUp.performed += ctx => IncreaseVolume();
        inputActions.Keyboard.VolumeDown.performed += ctx => DecreaseVolume();
        inputActions.Keyboard.ChangeStationRight.performed += ctx => ToggleRadio();
        inputActions.Keyboard.ChangeStationLeft.performed += ctx => ToggleRadio();
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
        StartFadeOutSlider(); // Start fading out the slider after it is used
    }

    void IncreaseVolume()
    {
        volumeSlider.value = Mathf.Clamp(volumeSlider.value + 0.1f, 0, 1);
        sliderCanvasGroup.alpha = 1;

        StartFadeOutSlider(); // Start fading out the slider after it is used
    }

    void DecreaseVolume()
    {
        volumeSlider.value = Mathf.Clamp(volumeSlider.value - 0.1f, 0, 1);
        sliderCanvasGroup.alpha = 1;

        StartFadeOutSlider(); // Start fading out the slider after it is used
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

    private void PlayNextSong()
    {
        currentSongIndex = (currentSongIndex + 1) % soundtrack.Length;
        audioSource.clip = soundtrack[currentSongIndex];
        audioSource.Play();
       // UpdateSongName();
    }

    private void StartFadeOutSlider()
    {
        lastInputTime = Time.time; // Reset the last input time to now
        if (fadeOutSliderCoroutine != null)
        {
            StopCoroutine(fadeOutSliderCoroutine);
        }
        fadeOutSliderCoroutine = StartCoroutine(FadeOutSlider());
    }

    private IEnumerator FadeOutSlider()
    {
        yield return new WaitForSeconds(sliderFadeOutTime); // Wait for the idle time
        float startAlpha = volumeSlider.GetComponent<CanvasGroup>().alpha;
        //sliderCanvasGroup = volumeSlider.GetComponent<CanvasGroup>();

        for (float t = 0; t < fadeOutTime; t += Time.deltaTime)
        {
            sliderCanvasGroup.alpha = Mathf.Lerp(startAlpha, 0, t / fadeOutTime);
            yield return null;
        }

        sliderCanvasGroup.alpha = 0;
    }
}
