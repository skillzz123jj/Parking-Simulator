using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CarCustomization : MonoBehaviour
{
    [Header("Car Bodies")]
    [SerializeField] private GameObject familyCarBody;
    [SerializeField] private GameObject pickupTruckBody;
    private GameObject currentCarBody;

    [Header("Car Parts")]
    [SerializeField] private GameObject carLight;
    [SerializeField] private GameObject colorObject;
    [SerializeField] private GameObject lightObject;

    [Header("Materials")]
    [SerializeField] private Material carBodyMaterial;
    [SerializeField] private Material wheelMaterial;
    [SerializeField] private Material matteMaterial;
    [SerializeField] private Material metallicMaterial;
    [SerializeField] private Material defaultMaterial;

    [Header("Cars")]
    [SerializeField] private GameObject familyCar;
    [SerializeField] private GameObject pickupTruck;

    [Header("UI Elements")]
    [SerializeField] private Button matteButton;
    [SerializeField] private Button metallicButton;

    [Header("Lighting")]
    [SerializeField] private ChangeUnderLight rainbowLight;

    public void ChangeCarMaterial(string carTexture)
    {
        carBodyMaterial = carTexture == "Metallic" ? metallicMaterial : matteMaterial;
        currentCarBody.GetComponent<MeshRenderer>().material = carBodyMaterial;
        GameData.carTexture = carTexture;
    }
    public void ChangeCarColor(Image colorImage)
    {
      GameData.carColor = colorImage.color;
        metallicMaterial.color = colorImage.color;
        matteMaterial.color = colorImage.color;
    }

    public void ChangeRimColor(Image colorImage)
    {
         GameData.wheelColor = colorImage.color;
        wheelMaterial.color = colorImage.color;
    }

    public void ChangeCarLight(Image colorImage)
    {
        StartCoroutine(DisableRainbowLightAndChangeColor(colorImage));
    }

    private IEnumerator DisableRainbowLightAndChangeColor(Image colorImage)
    {

        rainbowLight.enabled = false;
        GameData.rainbowOn = "Disabled";

        yield return null;

        GameData.lightsOn = true;
        Color color = colorImage.color;
        GameData.lightColor = color;
        carLight.GetComponent<Light>().color = color;
    }

    public void ChangeCar(string carModel)
    {
        GameData.carModel = carModel;
        currentCarBody = carModel == "FamilyCar" ? familyCarBody : pickupTruckBody;
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
    }

    public void CarInitialization(Color carBody, Color lightColor, Color wheelColor, string carTexture, string carModel, string rainbowOn)
    {
        ChangeCar(carModel);
        ChangeCarMaterial(carTexture);
        carBodyMaterial.color = carBody;
        wheelMaterial.color = wheelColor;
        carLight.GetComponent<Light>().color = lightColor;
        ChangeUnderLight isRainbowEnabled = carLight.GetComponent<ChangeUnderLight>();
        isRainbowEnabled.enabled = GameData.rainbowOn == "Enabled";
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

    public void RainbowLight()
    {
        GameData.rainbowOn = "Enabled";
        GameData.lightsOn = true;
        rainbowLight.enabled = true;
    }

}