using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayFabSetup : MonoBehaviour
{
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
        Debug.Log("Login successful! Player ID: " + result.PlayFabId);
        // Save the PlayFabId for future use
        PlayerPrefs.SetString("PlayFabId", result.PlayFabId);
    }

    void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError("Error logging in: " + error.GenerateErrorReport());
    }
}