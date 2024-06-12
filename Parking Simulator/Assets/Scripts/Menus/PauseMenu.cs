using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


public class PauseMenu : MonoBehaviour
{
    [SerializeField] PostProcessProfile profile;
    DepthOfField depthOfField;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject UIWheel;
    [SerializeField] GameObject gears;

    [SerializeField] GameObject blackOutSquare;

    private bool fadeToBlackActive = true;

      private Inputs inputActions;

    
    void Awake()
    {
        inputActions = new Inputs();
    
    }

    void OnEnable()
    {
        inputActions.Enable();
        inputActions.SteeringWheel.L2.performed += PauseMenuOn;
    }

    void OnDisable()
    {
        inputActions.SteeringWheel.L2.performed -= PauseMenuOn;
        inputActions.Disable();
    }
    void Start()
    {
        if (profile != null)
        {
            profile.TryGetSettings(out depthOfField);
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        UpdateFocusDistance();
    }

    void Update()
    {

        if (GameData.menuOpen)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void PauseMenuOn(InputAction.CallbackContext context)
    {
        if (pauseMenu != null)
        {
                GameData.menuOpen = true;
                pauseMenu.SetActive(true);
                UIWheel.SetActive(false);
            gears.SetActive(false);
                Time.timeScale = 0;
                UpdateFocusDistance(); 
            
        }
    }

    public void ClosePauseMenu()
    {
        if (pauseMenu != null)
        {
                GameData.menuOpen = false;
            
                pauseMenu.SetActive(false);
            UIWheel.SetActive(true);
            gears.SetActive(true);
            Time.timeScale = 1;
                UpdateFocusDistance(); 
            
        }
    }

    void UpdateFocusDistance()
    {
        if (depthOfField != null)
        {
            float focusDistance = pauseMenu.activeSelf ? 1f : 10f;
            depthOfField.focusDistance.value = focusDistance;
        }
    }

    public void Quit(int scene)
    {
        Time.timeScale = 1;
      

        StartCoroutine(LoadSceneCoroutine(scene));

    }

    private IEnumerator LoadSceneCoroutine(int sceneIndex)
    {
        // Start the fade-to-black transition
        yield return StartCoroutine(SceneTransition(true));

        // Load the new scene
        SceneManager.LoadScene(sceneIndex);

        // Start the fade-from-black transition
        yield return StartCoroutine(SceneTransition(false));
    }

    public IEnumerator SceneTransition(bool fadeToBlack = true, int fadeSpeed = 5)
    {
        Color objectColor = blackOutSquare.GetComponent<Image>().color;
        float fadeAmount;

        if (fadeToBlack)
        {
            while (blackOutSquare.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
        else
        {
            while (blackOutSquare.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
    }
}
