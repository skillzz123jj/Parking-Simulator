using UnityEngine;
using UnityEngine.UI;

public class LevelCompleted : MonoBehaviour
{
    [SerializeField] GameObject wheel;
    [SerializeField] GameObject gears;
    [SerializeField] GameObject radio;
    [SerializeField] GameObject levelFinished;
    [SerializeField] AudioClip levelFinishedClip;
    [SerializeField] Button nextLevelButton;
    [SerializeField] AudioSource wonSource;
    [SerializeField] GameObject instruction;

    private bool audioPlayed = false;

    void Start()
    {
        gears.SetActive(true);
        radio.SetActive(true);
        levelFinished.SetActive(false);
        GameData.Instance.MenuOpen = false;
        GameData.Instance.Parked = false;
        GameData.Instance.LevelFinished = false;
    }

    void Update()
    {
        if (GameData.Instance.LevelFinished && !audioPlayed)
        {
            GameData.Instance.MenuOpen = true;
            wheel.SetActive(false);
            gears.SetActive(false);
            radio.SetActive(false);
            levelFinished.SetActive(true);
            Destroy(instruction);

            wonSource.PlayOneShot(levelFinishedClip);
            audioPlayed = true;

            if (nextLevelButton)
            {
                nextLevelButton.Select();
            }
        }

    }
}
