using UnityEngine;

public class TestG29 : MonoBehaviour
{
    LogitechGSDK.LogiControllerPropertiesData properties;

    public float xAxes, GasInput, BreakInput, ClutchInput;

    public int CurrentGear;


    public static TestG29 testG29;
    void Start()
    {
    
        LogitechGSDK.LogiSteeringInitialize(false);
    }
  
    void Update()
    {
        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
        {
            LogitechGSDK.DIJOYSTATE2ENGINES rec;
            rec = LogitechGSDK.LogiGetStateUnity(0);

            xAxes = rec.lX / 32768f;

            if (rec.lY > 0)
            {
                GasInput = 0;
            }
            else if (rec.lY < 0)
            {
                GasInput = rec.lY / -32768f;

            }
            
            if (rec.lRz > 0)
            {
                BreakInput = 0;

            }
            else if (rec.lRz < 0)
            {
                BreakInput = rec.lRz / -32768f;
            }

            if (rec.rglSlider[0] > 0)
            {
                ClutchInput = 0;

            }
            else if (rec.rglSlider[0] < 0)
            {
                BreakInput = rec.lRz / -32768f;
            }

        }
        else
        {
            Debug.Log("No Steering Wheel");
        }
    }
    // Use this for shutdown 
    void Stop()
    {
        LogitechGSDK.LogiSteeringShutdown();
    }
}
