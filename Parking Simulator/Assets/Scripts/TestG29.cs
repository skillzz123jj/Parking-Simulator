using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestG29 : MonoBehaviour
{
    // Use this for initialization 
    void Start()
    {
        //not ignoring xinput in this example 
        LogitechGSDK.LogiSteeringInitialize(false);
    }
    // Update is called once per frame 
    void Update()
    {
        //All the test functions are called on the first device plugged in(index = 0) 
        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
        {
            if (LogitechGSDK.LogiIsPlaying(0, LogitechGSDK.LOGI_FORCE_SPRING))
            {
                LogitechGSDK.LogiStopSpringForce(0);
            }
            else
            {
                LogitechGSDK.LogiPlaySpringForce(0, 100, 100, 100);
            }
        }
    }
    // Use this for shutdown 
    void Stop()
    {
        LogitechGSDK.LogiSteeringShutdown();
    }
}
