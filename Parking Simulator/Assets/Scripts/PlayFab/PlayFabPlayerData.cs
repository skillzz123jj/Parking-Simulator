using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;
//using External.RimuruDevUtils.Helpers.Colors;
using PlayFab; // Import the correct namespace

public class PlayFabPlayerData : MonoBehaviour
{
    [SerializeField] CarCustomization carCustomization;

    public static Dictionary<string, int> levelsCompleted = new Dictionary<string, int>
    {
        {"Level1", 0},
        {"Level2", 0},
        {"Level3", 0}
    };

    public static Dictionary<string, string> carData = new Dictionary<string, string>
    {
        {"CarModel", "Car"},
        {"CarColor", "E2EAF4"},
        {"CarLights", "FE9900"},
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

            // Use the levelsCompleted dictionary in your game
            foreach (var level in levelsCompleted)
            {
                Debug.Log("Level: " + level.Key + ", Score: " + level.Value);
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

            // Use the carData dictionary in your game
            foreach (var level in carData)
            {
                Debug.Log("CarComponent: " + level.Key + ", Color: " + level.Value);
            }
            print(External.RimuruDevUtils.Helpers.Colors.ColorUtility.HexToColor(carData["CarColor"]));
            print(carData["CarColor"]);
            GameData.carColor = External.RimuruDevUtils.Helpers.Colors.ColorUtility.HexToColor(carData["CarColor"]);
            GameData.lightColor = External.RimuruDevUtils.Helpers.Colors.ColorUtility.HexToColor(carData["CarLights"]);
            GameData.wheelColor = External.RimuruDevUtils.Helpers.Colors.ColorUtility.HexToColor(carData["WheelColor"]);

            print(GameData.carColor);
            //carCustomization.CarInitialization(
            //    External.RimuruDevUtils.Helpers.Colors.ColorUtility.HexToColor(carData["CarColor"]),
            //    External.RimuruDevUtils.Helpers.Colors.ColorUtility.HexToColor(carData["CarLights"]),
            //    External.RimuruDevUtils.Helpers.Colors.ColorUtility.HexToColor(carData["WheelColor"])
            //);
        }
        else
        {
            Debug.Log("No car data found.");
        }
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
