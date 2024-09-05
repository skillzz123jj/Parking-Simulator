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
        PlayFabPlayerData.carData["CarColor"] = ColorToHex(GameData.Instance.CarColor);
        PlayFabPlayerData.carData["CarLights"] = ColorToHex(GameData.Instance.LightColor);
        PlayFabPlayerData.carData["WheelColor"] = ColorToHex(GameData.Instance.WheelColor);
        PlayFabPlayerData.carData["CarTexture"] = GameData.Instance.CarTexture;
        PlayFabPlayerData.carData["CarModel"] = GameData.Instance.CarModel;
        PlayFabPlayerData.carData["RainbowLight"] = GameData.Instance.RainbowOn;
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
        carCustomization.CarInitialization(GameData.Instance.CarColor, GameData.Instance.LightColor, GameData.Instance.WheelColor, GameData.Instance.CarTexture, GameData.Instance.CarModel, GameData.Instance.RainbowOn);
        GameData.Instance.DataFetched = true;

    }
    void UpdateGameData()
    {

        string carColorHex = PlayFabPlayerData.carData.ContainsKey("CarColor") ? PlayFabPlayerData.carData["CarColor"] : "#FF0000FF"; //Default to red if not found
        GameData.Instance.CarColor = HexToColor(carColorHex);

        string lightsColorHex = PlayFabPlayerData.carData.ContainsKey("CarLights") ? PlayFabPlayerData.carData["CarLights"] : "#00FF00FF"; //Default to green if not found
        GameData.Instance.LightColor = HexToColor(lightsColorHex);

        string wheelColorHex = PlayFabPlayerData.carData.ContainsKey("WheelColor") ? PlayFabPlayerData.carData["WheelColor"] : "#FFFFFFFF"; //Default to white if not found
        GameData.Instance.WheelColor = HexToColor(wheelColorHex);

        string carTexture = PlayFabPlayerData.carData.ContainsKey("CarTexture") ? PlayFabPlayerData.carData["CarTexture"] : "Metallic"; //Default to metallic if not found
        GameData.Instance.CarTexture = carTexture;

        string carModel = PlayFabPlayerData.carData.ContainsKey("CarModel") ? PlayFabPlayerData.carData["CarModel"] : "FamilyCar"; //Default to family car if not found
        GameData.Instance.CarModel = carModel;

        string isRainbowEnabled = PlayFabPlayerData.carData.ContainsKey("CarModel") ? PlayFabPlayerData.carData["RainbowLight"] : "Disabled"; //Default to off if not found
        GameData.Instance.RainbowOn = isRainbowEnabled;

        Debug.Log("GameData updated with car data.");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SaveGameData();
        }
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









