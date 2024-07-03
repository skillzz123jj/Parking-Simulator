using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


public class CarCustomization : MonoBehaviour
{
    [SerializeField] GameObject carBody;
    [SerializeField] GameObject carLight;
    [SerializeField] GameObject colorObject;
    [SerializeField] GameObject lightObject;
    [SerializeField] List<MeshRenderer> wheelMeshes = new List<MeshRenderer>();
    [SerializeField] Material carBodyMaterial;
    [SerializeField] Material wheelMaterial;


    [SerializeField] Material defaultMaterial;

    [SerializeField] TMP_Text text;

    bool changeLights;


    // Start is called before the first frame update
    void Start()
    {

       // if (GameData.carColor == null)
       // {
       //     GameData.carColor = Color.blue;
       // }
       ////ChangeCarColor(GameData.carColor);
       if (GameData.lightsOn)
       {
         
            carLight.SetActive(true);
            carLight.GetComponent<Light>().color = GameData.lightColor;
       }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeCarColor(Image colorImage)
    {
       // carBody.GetComponent<MeshRenderer>().material = color;
      GameData.carColor = colorImage.color;
      carBodyMaterial.color = colorImage.color;
    }

    public void ChangeRimColor(Image colorImage)
    {
        // carBody.GetComponent<MeshRenderer>().material = color;
         GameData.wheelColor = colorImage.color;
   
        wheelMaterial.color = colorImage.color;
    }

    public void ChangeCarLight(Image colorImage)
    {
        GameData.lightsOn = true;
        carLight.SetActive(true);
        Color color = colorImage.color;
        GameData.lightColor = color;
        carLight.GetComponent<Light>().color = color;
      
    }

    public void CarInitialization(Color carBody, Color lightColor, Color wheelColor)
    {
        print("i was called");
        carBodyMaterial.color = carBody;
        wheelMaterial.color = wheelColor;
        carLight.GetComponent<Light>().color = lightColor;
  


    }

    public void SettingChange()
    {
        changeLights = !changeLights;

        text.text = changeLights ? "Change color" : "Change lights";

        lightObject.SetActive(changeLights);
        colorObject.SetActive(!changeLights);
    }

    public void CloseLights()
    {
        GameData.lightsOn = false;
        carLight.SetActive(false);
    }

}