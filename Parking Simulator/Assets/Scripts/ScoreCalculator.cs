using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCalculator : MonoBehaviour
{
    [SerializeField] string levelName;
    [SerializeField] string nextLevelName;
    [SerializeField] List<GameObject> stars = new List<GameObject>();
    [SerializeField] Sprite starSprite;
    [SerializeField] Sprite starOutlineSprite;
    [SerializeField] float timeToFinishLevel;

    float currentTime;
    public int score = 3;
    bool timeRanOut;
    bool crashed;

    void Start()
    {
        currentTime = timeToFinishLevel;
    }

    void Update()
    {
        Timer();
        if (GameData.Instance.LevelFinished)
        {
            ShowScore(score);
            if (PlayFabPlayerData.levelsCompleted[levelName] < score)
            {
                PlayFabPlayerData.levelsCompleted[levelName] = score;
            }
            if (nextLevelName != "Null" && PlayFabPlayerData.levelsCompleted.ContainsKey(nextLevelName) && PlayFabPlayerData.levelsCompleted[nextLevelName] <= -1)
            {
                PlayFabPlayerData.levelsCompleted[nextLevelName] = 0;

            }
        }
    }

    private void ShowScore(int score)
    {
        for (int i = 0; i < score; i++)
        {
            GameObject star = stars[i];
            star.GetComponent<Image>().sprite = starSprite;
        }
    }

    private void Timer()
    {
        if (!timeRanOut)
        {
            currentTime -= Time.deltaTime;

            currentTime = Mathf.Clamp(currentTime, 0f, timeToFinishLevel);

            if (currentTime <= 0)
            {
                timeRanOut = true;
                score--;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "ParkingSpace" && !crashed)
        {
            crashed = true;
            score--;
        }
    }
}
