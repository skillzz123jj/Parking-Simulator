using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] GameObject[] levels;
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
    }

    public void OnNavigateRight()
    {
        levels[currentIndex].SetActive(false);
        currentIndex = (currentIndex < levels.Length - 1) ? currentIndex + 1 : 0;
        levels[currentIndex].SetActive(true);
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene(currentIndex + 1);
    }
}
