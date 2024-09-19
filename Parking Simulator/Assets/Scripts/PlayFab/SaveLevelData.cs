using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SaveLevelData : MonoBehaviour
{
    [SerializeField] List<Button> buttons = new List<Button>();
 
    private void OnEnable()
    {
        if (GameData.Instance.DataFetched == true)
        {
            StartCoroutine(SaveData());
        }
    }

    IEnumerator SaveData()
    {
        foreach (Button button in buttons)
        {
            button.enabled = false;
        }
        yield return new WaitForSeconds(2);
            PlayFabPlayerData playerData = new PlayFabPlayerData();
            playerData.SavePlayerData(PlayFabPlayerData.levelsCompleted, PlayFabPlayerData.carData);
        foreach (Button button in buttons)
        {
            button.enabled = true;
        }
    }
    }

