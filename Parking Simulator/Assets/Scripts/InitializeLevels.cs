using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitializeLevels : MonoBehaviour
{
    [SerializeField] List<GameObject> stars = new List<GameObject>();
    [SerializeField] Sprite starSprite;
    [SerializeField] Sprite starOutlineSprite;
    [SerializeField] GameObject lockedState;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.Instance.DataFetched == true)
        {
            if (PlayFabPlayerData.levelsCompleted[gameObject.name] > -1)
            {
                ShowScore(PlayFabPlayerData.levelsCompleted[gameObject.name]);
                lockedState.SetActive(false);
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
}
