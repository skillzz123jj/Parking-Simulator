using UnityEngine;

public class GameData : MonoBehaviour
{
    [Header("Car data")]
    public static Color carColor;
    public static Color lightColor;
    public static Color wheelColor;
    public static bool lightsOn;
    public static string carTexture;
    public static string rainbowOn;
    public static string carModel;

    [Header("Progress data")]
    public static bool dataFetched;
    public static bool parked;
    public static bool levelFinished;
    public static bool menuOpen;

    public static GameData Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }
}
