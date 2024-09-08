using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InitializeLevels : MonoBehaviour
{
    [SerializeField] List<GameObject> stars = new List<GameObject>();
    [SerializeField] Sprite starSprite;
    [SerializeField] Sprite starOutlineSprite;
    [SerializeField] GameObject lockedState;
    
    void Update()
    {
        if (GameData.Instance.DataFetched == true)
        {
            if (PlayFabPlayerData.levelsCompleted[gameObject.name] > -1)
            {
                ShowScore(PlayFabPlayerData.levelsCompleted[gameObject.name]);
                lockedState.SetActive(false);
            }
            else
            {
                ShowScore(PlayFabPlayerData.levelsCompleted[gameObject.name]);
                lockedState.SetActive(true);

            }
        }
        
    }

    private void ShowScore(int score)
    {
        foreach (var star in stars)
        {
            star.GetComponent<Image>().sprite = starOutlineSprite;

        }
        for (int i = 0; i < score; i++)
        {
            GameObject star = stars[i];
            star.GetComponent<Image>().sprite = starSprite;
        }
    }


}
