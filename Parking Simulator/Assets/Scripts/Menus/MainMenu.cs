using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject blackOutSquare;

    private bool fadeToBlackActive = true;
    private Coroutine runningCoroutine;

    private void Start()
    {
    
       // runningCoroutine = StartCoroutine(CheckConditionCoroutine());
      //  StartCoroutine(CheckConditionCoroutine());
        SceneManager.activeSceneChanged += OnSceneChanged;

    }
    IEnumerator CheckConditionCoroutine()
    {
        blackOutSquare.GetComponent<Image>().color = new Color(0, 0, 0, 1);

       

        // Coroutine stops when shouldRun becomes false
        yield return new WaitForSeconds(3);
        StartCoroutine(SceneTransition(false));
    }

    private void OnDestroy()
    {
        // Unsubscribe from the scene change event
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }

    private void OnSceneChanged(Scene previousScene, Scene newScene)
    {
        // Start the fade transition when the scene changes
        fadeToBlackActive = true;
        StartCoroutine(SceneTransition(fadeToBlackActive));
    }

    public void LoadScene(int scene)
    {
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

    public void CloseGame()
    {
        Application.Quit();
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
