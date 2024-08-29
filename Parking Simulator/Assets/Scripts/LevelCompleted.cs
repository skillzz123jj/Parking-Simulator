using UnityEngine;
using UnityEngine.UI;

public class LevelCompleted : MonoBehaviour
{
    [SerializeField] GameObject wheel;
    [SerializeField] GameObject gears;
    [SerializeField] GameObject radio;
    [SerializeField] GameObject levelFinished;
    [SerializeField] AudioClip levelFinishedClip;
    [SerializeField] Button restartButton;
    [SerializeField] AudioSource wonSource;
    [SerializeField] GameObject instruction;

   // [SerializeField] VehicleController carScript;
    void Start()
    {
        wheel.SetActive(true);
        gears.SetActive(true);
        radio.SetActive(true);
        levelFinished.SetActive(false);
        GameData.Instance.MenuOpen = false;
        GameData.Instance.Parked = false;
    }

    void Update()
    {
        if (GameData.Instance.LevelFinished)
        {
            GameData.Instance.LevelFinished = false;
            GameData.Instance.MenuOpen = true;
            wheel.SetActive(false);
            gears.SetActive(false);
            radio.SetActive(false);
            levelFinished.SetActive(true);
            instruction.SetActive(false);
            wonSource.PlayOneShot(levelFinishedClip);
            restartButton.Select();
           // carScript.enabled = false;
        }
    }
}
