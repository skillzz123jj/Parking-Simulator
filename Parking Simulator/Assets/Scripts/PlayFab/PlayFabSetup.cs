using PlayFab;
using PlayFab.ClientModels;
using static External.RimuruDevUtils.Helpers.Colors.ColorUtility;
using UnityEngine;
using TMPro;

public class PlayFabSetup : MonoBehaviour
{
    [SerializeField] CarCustomization carCustomization;
    PlayFabPlayerData playerData;
    [SerializeField] GameObject connectionStatus;
    bool wasConnected;
    void Start()
    {
       // LoginWithDeviceID();
        playerData = new PlayFabPlayerData();
        playerData.OnDataReceivedEvent += OnDataLoaded;
        wasConnected = Application.internetReachability != NetworkReachability.NotReachable;
        if (wasConnected)
        {
            LoginWithDeviceID();
        }
        else
        {
            connectionStatus.SetActive(true);
        }

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

    void SaveGameData()
    {
        ConvertDataToJSON();
        PlayFabPlayerData playerData = new PlayFabPlayerData();
        playerData.SavePlayerData(PlayFabPlayerData.levelsCompleted, PlayFabPlayerData.carData);

    }
    public void ConvertDataToJSON()
    {
        PlayFabPlayerData.carData["CarColor"] = ColorToHex(GameData.carColor);
        PlayFabPlayerData.carData["CarLights"] = ColorToHex(GameData.lightColor);
        PlayFabPlayerData.carData["WheelColor"] = ColorToHex(GameData.wheelColor);
        PlayFabPlayerData.carData["CarTexture"] = GameData.carTexture;


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
        carCustomization.CarInitialization(GameData.carColor, GameData.lightColor, GameData.wheelColor, GameData.carTexture);
    }
    void UpdateGameData()
    {

        string carColorHex = PlayFabPlayerData.carData.ContainsKey("CarColor") ? PlayFabPlayerData.carData["CarColor"] : "#FF0000FF"; // Default to red if not found
        GameData.carColor = HexToColor(carColorHex);

        string lightsColorHex = PlayFabPlayerData.carData.ContainsKey("CarLights") ? PlayFabPlayerData.carData["CarLights"] : "#00FF00FF"; // Default to green if not found
        GameData.lightColor = HexToColor(lightsColorHex);

        string wheelColorHex = PlayFabPlayerData.carData.ContainsKey("WheelColor") ? PlayFabPlayerData.carData["WheelColor"] : "#FFFFFFFF"; // Default to white if not found
        GameData.wheelColor = HexToColor(wheelColorHex);

        string carTexture = PlayFabPlayerData.carData.ContainsKey("CarTexture") ? PlayFabPlayerData.carData["CarTexture"] : "Metallic"; // Default to white if not found
        GameData.carTexture = carTexture;

        Debug.Log("GameData updated with car data.");
    }

    private void Update()
    {
        bool isConnected = Application.internetReachability != NetworkReachability.NotReachable;

        if (isConnected && !wasConnected)
        {
            // Connection was restored
            connectionStatus.SetActive(false);
            LoginWithDeviceID();
        }

        if (!isConnected && wasConnected)
        {
            // Connection was lost
            connectionStatus.SetActive(true);
        }

        wasConnected = isConnected;
    }
}









