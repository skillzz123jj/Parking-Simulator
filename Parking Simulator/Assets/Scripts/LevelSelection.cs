using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] GameObject[] levels;
    [SerializeField] GameObject startButton;
    private int currentIndex = 0;


    void Start()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(i == currentIndex);
        }
    }
    public void OnNavigateLeft()
    {
        levels[currentIndex].SetActive(false);
        currentIndex = (currentIndex > 0) ? currentIndex - 1 : levels.Length - 1;
        levels[currentIndex].SetActive(true);
        CheckLevelStatus(levels[currentIndex].name);

    }

    public void OnNavigateRight()
    {
        levels[currentIndex].SetActive(false);
        currentIndex = (currentIndex < levels.Length - 1) ? currentIndex + 1 : 0;
        levels[currentIndex].SetActive(true);
        CheckLevelStatus(levels[currentIndex].name); 
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene(currentIndex + 1);
    }

    private void CheckLevelStatus(string level)
    {
        if (PlayFabPlayerData.levelsCompleted[level] < 0)
        {
            startButton.SetActive(false);
        }
        else
        {
            startButton.SetActive(true);
        }
    }

    public void ResetProgress()
    {
        foreach (var key in PlayFabPlayerData.levelsCompleted.Keys.ToList())
        {
            if (key == "Level1")
            {
                PlayFabPlayerData.levelsCompleted[key] = 0;
            }
            else
            {

                PlayFabPlayerData.levelsCompleted[key] = -1;
            }
        }
        if (GameData.Instance.DataFetched == true)
        {

            PlayFabPlayerData playerData = new PlayFabPlayerData();
            playerData.SavePlayerData(PlayFabPlayerData.levelsCompleted, PlayFabPlayerData.carData);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            foreach (KeyValuePair<string, int> entry in PlayFabPlayerData.levelsCompleted)
            {
                print(entry);
            }
        }
       
    }
}
