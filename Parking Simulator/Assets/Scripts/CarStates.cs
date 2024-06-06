using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarStates : MonoBehaviour
{
    [SerializeField] TMP_Text pState;
    [SerializeField] TMP_Text rState;
    [SerializeField] TMP_Text nState;
    [SerializeField] TMP_Text dState;

    [SerializeField] GameObject car;
    [SerializeField] GameObject carLights;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            TurnVehicleOn();
        }
    }

    void TurnVehicleOn()
    {
       // car.GetComponent<TESTCarController>().enabled = true;
        carLights.SetActive(true);
        pState.color = Color.red;
    }
}
