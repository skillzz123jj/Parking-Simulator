using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FetchData : MonoBehaviour
{
    [SerializeField] PlayFabSetup playFabSetup;
    [SerializeField] CarCustomization carCustomization;
  //  void OnLoginSuccess(LoginResult result)
  //  {
  //      Debug.Log("ID: " + result.PlayFabId);
  //      Debug.Log("worked: " + result.PlayFabId);

  //      PlayerPrefs.SetString("PlayFabId", result.PlayFabId);

  //      // Access PlayFabSetup instance through Inspector or by finding it dynamically
  ////      playFabSetup = FindObjectOfType<PlayFabSetup>();

     
  //  }

    //private void Start()
    //{
    //    // Example: Load player data from PlayFab
    //    playFabSetup.LoadGameData();

    //    // Example: Update GameData with fetched car data
    //    UpdateGameData();
    //    carCustomization.CarInitialization(GameData.carColor, GameData.lightColor, GameData.wheelColor);
    //}

    //void UpdateGameData()
    //{
    //    Dictionary<string, string> carData = PlayFabSetup.carData;

    //    string carColorHex = carData.ContainsKey("CarColor") ? carData["CarColor"] : "#FF0000FF"; // Default to red if not found
    //    GameData.carColor = HexToColor(carColorHex);

    //    string lightsColorHex = carData.ContainsKey("CarLights") ? carData["CarLights"] : "#00FF00FF"; // Default to green if not found
    //    GameData.lightColor = HexToColor(lightsColorHex);

    //    string wheelColorHex = carData.ContainsKey("WheelColor") ? carData["WheelColor"] : "#FFFFFFFF"; // Default to white if not found
    //    GameData.wheelColor = HexToColor(wheelColorHex);

    //    Debug.Log("GameData updated with car data.");
    //}

    // Helper method to convert hexadecimal color string to Color
    public static Color HexToColor(string hex)
    {
        if (hex.StartsWith("#"))
        {
            hex = hex.Substring(1);
        }

        Color color = new Color();
        if (ColorUtility.TryParseHtmlString("#" + hex, out color))
        {
            return color;
        }
        else
        {
            Debug.LogError("Invalid hexadecimal color string: " + hex);
            return Color.white; // Default color if conversion fails
        }
    }
}
