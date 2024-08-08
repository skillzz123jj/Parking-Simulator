using PlayFab;
using PlayFab.ClientModels;
using static External.RimuruDevUtils.Helpers.Colors.ColorUtility;
using UnityEngine;
using System.Net;

public class PlayFabHandleData : MonoBehaviour
{
    [SerializeField] CarCustomization carCustomization;
    PlayFabPlayerData playerData;
    [SerializeField] GameObject connectionStatus;
    bool wasConnected;
    void Start()
    {
        playerData = new PlayFabPlayerData();
        playerData.OnDataReceivedEvent += OnDataLoaded;
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
        connectionStatus.SetActive(false);

        LoadGameData();
    }

    void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError("Error: " + error.GenerateErrorReport());
        connectionStatus.SetActive(true);

    }

    public void SaveGameData()
    {
        ConvertDataToJSONFormat();
        PlayFabPlayerData playerData = new PlayFabPlayerData();
        playerData.SavePlayerData(PlayFabPlayerData.levelsCompleted, PlayFabPlayerData.carData);

    }
    public void ConvertDataToJSONFormat()
    {
        PlayFabPlayerData.carData["CarColor"] = ColorToHex(GameData.carColor);
        PlayFabPlayerData.carData["CarLights"] = ColorToHex(GameData.lightColor);
        PlayFabPlayerData.carData["WheelColor"] = ColorToHex(GameData.wheelColor);
        PlayFabPlayerData.carData["CarTexture"] = GameData.carTexture;
        PlayFabPlayerData.carData["CarModel"] = GameData.carModel;
        PlayFabPlayerData.carData["RainbowLight"] = GameData.rainbowOn;



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
       
        playerData.GetPlayerData();

    }


    void OnDataLoaded()
    {
        UpdateGameData();
        carCustomization.CarInitialization(GameData.carColor, GameData.lightColor, GameData.wheelColor, GameData.carTexture, GameData.carModel, GameData.rainbowOn);
    }
    void UpdateGameData()
    {

        string carColorHex = PlayFabPlayerData.carData.ContainsKey("CarColor") ? PlayFabPlayerData.carData["CarColor"] : "#FF0000FF"; //Default to red if not found
        GameData.carColor = HexToColor(carColorHex);

        string lightsColorHex = PlayFabPlayerData.carData.ContainsKey("CarLights") ? PlayFabPlayerData.carData["CarLights"] : "#00FF00FF"; //Default to green if not found
        GameData.lightColor = HexToColor(lightsColorHex);

        string wheelColorHex = PlayFabPlayerData.carData.ContainsKey("WheelColor") ? PlayFabPlayerData.carData["WheelColor"] : "#FFFFFFFF"; //Default to white if not found
        GameData.wheelColor = HexToColor(wheelColorHex);

        string carTexture = PlayFabPlayerData.carData.ContainsKey("CarTexture") ? PlayFabPlayerData.carData["CarTexture"] : "Metallic"; //Default to metallic if not found
        GameData.carTexture = carTexture;

        string carModel = PlayFabPlayerData.carData.ContainsKey("CarModel") ? PlayFabPlayerData.carData["CarModel"] : "FamilyCar"; //Default to family car if not found
        GameData.carModel = carModel;

        string isRainbowEnabled = PlayFabPlayerData.carData.ContainsKey("CarModel") ? PlayFabPlayerData.carData["RainbowLight"] : "Disabled"; //Default to off if not found
        GameData.rainbowOn = isRainbowEnabled;

        Debug.Log("GameData updated with car data.");
    }

    private void Update()
    {
        if (IsInternetConnected())
        {
            if (!wasConnected)
            {
                connectionStatus.SetActive(false);
                wasConnected = true;
                LoginWithDeviceID();
            }
            connectionStatus.SetActive(false);

        }
        else
        {
            connectionStatus.SetActive(true);
        }
    }

    static bool IsInternetConnected()
    {
        try
        {
            Dns.GetHostEntry("www.google.com"); 
            return true;
        }
        catch
        {
            return false; 
        }
    }
}









