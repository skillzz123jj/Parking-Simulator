using UnityEngine;

public class GameData
{
    // Singleton instance
    private static GameData instance;

    // Car data
    public Color CarColor { get; set; }
    public Color LightColor { get; set; }
    public Color WheelColor { get; set; }
    public bool LightsOn { get; set; }
    public string CarTexture { get; set; }
    public string RainbowOn { get; set; }
    public string CarModel { get; set; }

    // Progress data
    public bool DataFetched { get; set; }
    public bool Parked { get; set; }
    public bool LevelFinished { get; set; }
    public bool MenuOpen { get; set; }

    // Private constructor to prevent instantiation
    private GameData() { }

    // Public method to access the instance
    public static GameData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameData();
            }
            return instance;
        }
    }
}

