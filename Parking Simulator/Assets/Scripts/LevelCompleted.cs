using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleted : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        wheel.SetActive(true);
        gears.SetActive(true);
        radio.SetActive(true);
        levelFinished.SetActive(false);
        carScript.enabled = true;
        GameData.menuOpen = false;
         GameData.parked = false;
    }

    [SerializeField] GameObject wheel;
    [SerializeField] GameObject gears;
    [SerializeField] GameObject radio;
    [SerializeField] GameObject levelFinished;

    [SerializeField] VehicleController carScript;





    // Update is called once per frame
    void Update()
    {
        if (GameData.levelFinished)
        {
        GameData.levelFinished = false;
        GameData.menuOpen = true;
        wheel.SetActive(false);
        gears.SetActive(false);
        radio.SetActive(false);
        levelFinished.SetActive(true);
        carScript.enabled = false;
        }
    }
}
