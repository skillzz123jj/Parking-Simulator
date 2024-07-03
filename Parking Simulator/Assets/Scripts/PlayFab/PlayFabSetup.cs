using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using static External.RimuruDevUtils.Helpers.Colors.ColorUtility;
using UnityEngine;

public class PlayFabSetup : MonoBehaviour
{

    //public static Dictionary<string, int> levelsCompleted = new Dictionary<string, int>
    //{
    //    {"Level1", 0},
    //    {"Level2", 0},
    //    {"Level3", 0}
    //};

    //public static Dictionary<string, string> carData = new Dictionary<string, string>
    //{
    //    {"CarModel", "Car"},
    //    {"CarColor", "E2EAF4"},
    //    {"CarLights", "FE9900"},
    //    {"WheelColor", "EFC3CA"}
    //};

    [SerializeField] CarCustomization carCustomization;

    void Start()
    {
        LoginWithDeviceID();

      
    }

    void LoginWithDeviceID()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("ID: " + result.PlayFabId);
        PlayerPrefs.SetString("PlayFabId", result.PlayFabId);
        LoadGameData();
    }

    void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError("Error: " + error.GenerateErrorReport());
    }

    void SaveGameData()
    {
        //    Dictionary<string, int> levelsCompleted = new Dictionary<string, int>
        //{
        //    {"Level1", 0},
        //    {"Level2", 0},
        //    {"Level3", 0}
        //};
        //  levelsCompleted
        ConvertDataToJSON();
        PlayFabPlayerData playerData = new PlayFabPlayerData();
        playerData.SavePlayerData(PlayFabPlayerData.levelsCompleted, PlayFabPlayerData.carData);

        foreach (var level in PlayFabPlayerData.levelsCompleted)
        {
            print("Level: " + level.Key + "Score: " + level.Value);
        }
        foreach (var level in PlayFabPlayerData.carData)
        {
            print("CarComponent: " + level.Key + "Value: " + level.Value);
        }

    }
    public void ConvertDataToJSON()
    {
        PlayFabPlayerData.carData["CarColor"] = ColorToHex(GameData.carColor);
        PlayFabPlayerData.carData["CarLights"] = ColorToHex(GameData.lightColor);
        PlayFabPlayerData.carData["WheelColor"] = ColorToHex(GameData.wheelColor);


    }
    public static string ColorToHex(Color color)
    {
        int r = Mathf.RoundToInt(color.r * 255f);
        int g = Mathf.RoundToInt(color.g * 255f);
        int b = Mathf.RoundToInt(color.b * 255f);

        return string.Format("{0:X2}{1:X2}{2:X2}", r, g, b);
    }
    public void LoadGameData()
    {
        PlayFabPlayerData playerData = new PlayFabPlayerData();
        playerData.GetPlayerData();
       // UpdateGameData();
        foreach (var level in PlayFabPlayerData.levelsCompleted)
        {
            print("Level: " + level.Key + "Score: " + level.Value);
        }
        foreach (var level in PlayFabPlayerData.carData)
        {
            print("CarComponent: " + level.Key + "Value: " + level.Value);
        }

    //    UpdateGameData();
      //  carCustomization.CarInitialization(HexToColor(carData["CarColor"]), HexToColor(carData["CarLights"]), HexToColor(carData["WheelColor"]));
    }

    void UpdateGameData()
    {
      //  Dictionary<string, string> carData = playFabSetup.carData;

        string carColorHex = PlayFabPlayerData.carData.ContainsKey("CarColor") ? PlayFabPlayerData.carData["CarColor"] : "#FF0000FF"; // Default to red if not found
        GameData.carColor = HexToColor(carColorHex);

        string lightsColorHex = PlayFabPlayerData.carData.ContainsKey("CarLights") ? PlayFabPlayerData.carData["CarLights"] : "#00FF00FF"; // Default to green if not found
        GameData.lightColor = HexToColor(lightsColorHex);

        string wheelColorHex = PlayFabPlayerData.carData.ContainsKey("WheelColor") ? PlayFabPlayerData.carData["WheelColor"] : "#FFFFFFFF"; // Default to white if not found
        GameData.wheelColor = HexToColor(wheelColorHex);

        Debug.Log("GameData updated with car data.");
    }

    // Helper method to convert hexadecimal color string to Color
    //public static Color HexToColor(string hex)
    //{
    //    if (hex.StartsWith("#"))
    //    {
    //        hex = hex.Substring(1);
    //    }

    //    Color color = new Color();
    //    if (ColorUtility.TryParseHtmlString("#" + hex, out color))
    //    {
    //        return color;
    //    }
    //    else
    //    {
    //        Debug.LogError("Invalid hexadecimal color string: " + hex);
    //        return Color.white; // Default color if conversion fails
    //    }
    //}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayFabPlayerData.levelsCompleted["Level1"] += 1;
            SaveGameData();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayFabPlayerData.levelsCompleted["Level2"] += 1;
            SaveGameData();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayFabPlayerData.levelsCompleted["Level3"] += 1;
            SaveGameData();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            LoadGameData();
            carCustomization.CarInitialization(
             GameData.carColor, GameData.lightColor, GameData.wheelColor
             
         ); 
        }



    }
}









