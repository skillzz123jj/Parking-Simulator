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

    [SerializeField] GameObject familyCar;
    [SerializeField] GameObject pickupTruck;


    [SerializeField] Material matteMaterial;
    [SerializeField] Material metallicMaterial;

    [SerializeField] Material defaultMaterial;

    [SerializeField] TMP_Text text;

    [SerializeField] Button matteButton;
    [SerializeField] Button metallicButton;


    bool changeLights;

    public void ChangeCarMaterial(string carTexture)
    {
        carBodyMaterial = carTexture == "Metallic" ? metallicMaterial : matteMaterial;
        carBody.GetComponent<MeshRenderer>().material = carBodyMaterial;
        GameData.carTexture = carTexture;
    }
    public void ChangeCarColor(Image colorImage)
    {
      GameData.carColor = colorImage.color;
        metallicMaterial.color = colorImage.color;
        matteMaterial.color = colorImage.color;
    //  carBodyMaterial.color = colorImage.color;
    }

    public void ChangeRimColor(Image colorImage)
    {
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

    public void ChangeCar(string car)
    {
        GameData.carModel = car;
    }


    public void CarInitialization(Color carBody, Color lightColor, Color wheelColor, string carTexture, string carModel)
    {
        if (carModel == "FamilyCar")
        {
            familyCar.SetActive(true);
            pickupTruck.SetActive(false);
        }
        else
        {
            pickupTruck.SetActive(true);
            familyCar.SetActive(false);
        }
        ChangeCarMaterial(carTexture);
        carBodyMaterial.color = carBody;
        wheelMaterial.color = wheelColor;
        carLight.GetComponent<Light>().color = lightColor;
        carBodyMaterial = carTexture == "Metallic" ? metallicMaterial : matteMaterial;
        if (carTexture == "Matte")
        {
            matteButton.onClick.Invoke();
        }
        else
        {
            metallicButton.onClick.Invoke();
        }
        GameData.dataFetched = true;
 
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