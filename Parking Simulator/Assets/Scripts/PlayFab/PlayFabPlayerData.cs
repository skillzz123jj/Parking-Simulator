using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;
using PlayFab; 

public class PlayFabPlayerData
{
    public delegate void DataReceivedHandler();
    public event DataReceivedHandler OnDataReceivedEvent;

    public static Dictionary<string, int> levelsCompleted = new Dictionary<string, int>
    {
        {"Level1", 0},
        {"Level2", -1},
        {"Level3", -1},
        {"Level4", -1},
        {"Level5", -1},
        {"Level6", -1}


    };

    public static Dictionary<string, string> carData = new Dictionary<string, string>
    {
        {"CarModel", "FamilyCar"},
        {"CarTexture", "Metallic"},
        {"CarColor", "E2EAF4"},
        {"CarLights", "FE9900"},
        {"RainbowLight", "Disabled"},
        {"WheelColor", "EFC3CA"}
    };


    public void SavePlayerData(Dictionary<string, int> levelsCompleted, Dictionary<string, string> carData)
    {
        // Serialize the dictionary to a JSON string
        string levelsCompletedJson = JsonUtility.ToJson(new Serialization<string, int>(levelsCompleted));
        string carDataJson = JsonUtility.ToJson(new Serialization<string, string>(carData));

        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"LevelsCompleted", levelsCompletedJson},
                {"CarData", carDataJson}
            }
        };

        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }

    public void GetPlayerData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataReceived, OnError);
    }

    void OnDataSend(UpdateUserDataResult result)
    {
        Debug.Log("Data successfully saved.");
    }

    void OnDataReceived(GetUserDataResult result)
    {
        if (result.Data != null && result.Data.ContainsKey("LevelsCompleted"))
        {
            // Deserialize the JSON string back to a dictionary
            string levelsCompletedJson = result.Data["LevelsCompleted"].Value;
            levelsCompleted = JsonUtility.FromJson<Serialization<string, int>>(levelsCompletedJson).ToDictionary();
            if (!levelsCompleted.ContainsKey("Level6"))
            {
                levelsCompleted.Add("Level6", -1); 
            }

        }
        else
        {
            Debug.Log("No data found.");
        }

        if (result.Data != null && result.Data.ContainsKey("CarData"))
        {
            // Deserialize the JSON string back to a dictionary
            string carDataJson = result.Data["CarData"].Value;
            carData = JsonUtility.FromJson<Serialization<string, string>>(carDataJson).ToDictionary();

            GameData.Instance.CarColor = External.RimuruDevUtils.Helpers.Colors.ColorUtility.HexToColor(carData["CarColor"]);
            GameData.Instance.LightColor = External.RimuruDevUtils.Helpers.Colors.ColorUtility.HexToColor(carData["CarLights"]);
            GameData.Instance.WheelColor = External.RimuruDevUtils.Helpers.Colors.ColorUtility.HexToColor(carData["WheelColor"]);
            GameData.Instance.CarTexture = carData["CarTexture"];
            GameData.Instance.CarModel = carData["CarModel"];
            GameData.Instance.RainbowOn = carData["RainbowLight"];

        }
        else
        {
            Debug.Log("No car data found.");
        }
        OnDataReceivedEvent?.Invoke();
        Debug.Log("Data successfully fetched.");
        GameData.Instance.DataFetched = true;
    

    }

    void OnError(PlayFabError error)
    {
        Debug.LogError("Error: " + error.GenerateErrorReport());
    }
}

[System.Serializable]
public class Serialization<TKey, TValue>
{
    public List<TKey> keys;
    public List<TValue> values;

    public Serialization(Dictionary<TKey, TValue> dictionary)
    {
        keys = new List<TKey>(dictionary.Keys);
        values = new List<TValue>(dictionary.Values);
    }

    public Dictionary<TKey, TValue> ToDictionary()
    {
        Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>();
        for (int i = 0; i < keys.Count; i++)
        {
            result[keys[i]] = values[i];
        }
        return result;
    }
}
