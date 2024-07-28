using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{

    public static Color carColor;
    public static Color lightColor;
    public static Color wheelColor;
    public static string carTexture;
    public static bool lightsOn;
    public static string carModel;
    public static bool dataFetched;

    public static bool parked;

    public static bool levelFinished;
    public static bool menuOpen;
       void Start()
    {
        if (this == null)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {

            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
