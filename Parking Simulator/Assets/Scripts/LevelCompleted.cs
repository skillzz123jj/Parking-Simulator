using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleted : MonoBehaviour
{
   
    [SerializeField] GameObject wheel;
    [SerializeField] GameObject gears;
    [SerializeField] GameObject radio;
    [SerializeField] GameObject levelFinished;
    [SerializeField] AudioClip levelFinishedClip;
    [SerializeField] Button restartButton;
    [SerializeField] AudioSource wonSource;
    [SerializeField] GameObject instruction;

    [SerializeField] VehicleController carScript;


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
           instruction.SetActive(false);
          wonSource.PlayOneShot(levelFinishedClip);
          restartButton.Select();
        carScript.enabled = false;
        }
    }
}
