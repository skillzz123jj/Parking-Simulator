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
        if (GameData.Instance.CarModel == carType)
        {
            body.material = GameData.Instance.CarTexture == "Metallic" ? metallicMaterial : matteMaterial;
            ChangeUnderLight isRainbowEnabled = underLight.GetComponent<ChangeUnderLight>();
            isRainbowEnabled.enabled = GameData.Instance.RainbowOn == "Enabled";
            body.material.color = GameData.Instance.CarColor;
            wheels.color = GameData.Instance.WheelColor;
            underLight.GetComponent<Light>().color = GameData.Instance.LightColor;
            gameObject.SetActive(true);
        }
        else
        {
          //  gameObject.SetActive(false);
        }
    }
}
