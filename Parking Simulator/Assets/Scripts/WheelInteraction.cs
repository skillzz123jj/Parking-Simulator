using UnityEngine;

public class WheelInteraction : MonoBehaviour
{
    LogitechGSDK.LogiControllerPropertiesData properties;

    public static float xAxes, GasInput, BrakeInput, ClutchInput;
    public float wheelSteering;

    [SerializeField] RectTransform steeringWheelRect;
    [SerializeField] GameObject steeringWheelUI;


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

            UpdateSteeringWheelUI(xAxes);
            steeringWheelUI.SetActive(true);

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
                BrakeInput = 0;

            }
            else if (rec.lRz < 0)
            {
                BrakeInput = rec.lRz / -32768f;
            }

            if (rec.rglSlider[0] > 0)
            {
                ClutchInput = 0;

            }
            else if (rec.rglSlider[0] < 0)
            {
                ClutchInput = rec.lRz / -32768f;
            }

        }
        else
        {
            steeringWheelUI.SetActive(false);

        }
    }

    private void UpdateSteeringWheelUI(float steeringInput)
    {
        if (steeringWheelUI != null)
        {
            float uiRotationAngle = wheelSteering * steeringInput;
            steeringWheelRect.localEulerAngles = new Vector3(0, 0, -uiRotationAngle); // Rotate around the Z-axis
        }
    }
    // Use this for shutdown 
    void Stop()
    {
        LogitechGSDK.LogiSteeringShutdown();
    }
}
