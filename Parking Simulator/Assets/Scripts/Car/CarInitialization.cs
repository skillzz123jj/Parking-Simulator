using UnityEngine;

public class CarInitialization : MonoBehaviour
{
    [SerializeField] MeshRenderer body;
    [SerializeField] Material wheels;
    [SerializeField] Light underLight;
    [SerializeField] Material metallicMaterial;
    [SerializeField] Material matteMaterial;

    void Start()
    {
        body.material = GameData.carTexture == "Metallic" ? metallicMaterial : matteMaterial;
        body.material.color = GameData.carColor;
        wheels.color = GameData.wheelColor;
        underLight.color = GameData.lightColor;

    }
}
