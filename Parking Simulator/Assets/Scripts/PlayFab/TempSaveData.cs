using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSaveData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {

        StartCoroutine(Test());
    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(2);
        if (GameData.Instance.DataFetched == true)
        {

            PlayFabPlayerData playerData = new PlayFabPlayerData();
            playerData.SavePlayerData(PlayFabPlayerData.levelsCompleted, PlayFabPlayerData.carData);
        }
    }
}
