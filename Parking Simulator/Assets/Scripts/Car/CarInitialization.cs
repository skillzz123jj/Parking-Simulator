using UnityEngine;

public class CarInitialization : MonoBehaviour
{
    [SerializeField] MeshRenderer body;
    [SerializeField] GameObject underLight;
    [SerializeField] Material wheels;
    [SerializeField] Material metallicMaterial;
    [SerializeField] Material matteMaterial;
    [SerializeField] string carType;

    void Start()
    {
        if (GameData.carModel == carType)
        {
            body.material = GameData.carTexture == "Metallic" ? metallicMaterial : matteMaterial;
            ChangeUnderLight isRainbowEnabled = underLight.GetComponent<ChangeUnderLight>();
            isRainbowEnabled.enabled = GameData.rainbowOn == "Enabled";
            body.material.color = GameData.carColor;
            wheels.color = GameData.wheelColor;
            underLight.GetComponent<Light>().color = GameData.lightColor;
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
