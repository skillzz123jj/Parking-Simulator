using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] GameObject[] levels;
    private int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Initially, activate the first level and deactivate the rest
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(i == currentIndex);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Your update logic here
    }

    public void OnNavigateLeft()
    {
        // Deactivate the current level
        levels[currentIndex].SetActive(false);
        // Move to the previous level
        currentIndex = (currentIndex > 0) ? currentIndex - 1 : levels.Length - 1;
        // Activate the new current level
        levels[currentIndex].SetActive(true);
    }

    public void OnNavigateRight()
    {
        // Deactivate the current level
        levels[currentIndex].SetActive(false);
        // Move to the next level
        currentIndex = (currentIndex < levels.Length - 1) ? currentIndex + 1 : 0;
        // Activate the new current level
        levels[currentIndex].SetActive(true);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(currentIndex + 1);
    }
}
