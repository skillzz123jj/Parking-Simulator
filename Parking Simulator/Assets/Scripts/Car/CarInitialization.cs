using UnityEngine;

public class CarInitialization : MonoBehaviour
{
    [SerializeField] MeshRenderer body;
    [SerializeField] Material wheels;
    [SerializeField] Light underLight;
    [SerializeField] Material metallicMaterial;
    [SerializeField] Material matteMaterial;
    [SerializeField] string carType;

    void Start()
    {
        Debug.Log(GameData.carModel);
        if (GameData.carModel == carType)
        {
            body.material = GameData.carTexture == "Metallic" ? metallicMaterial : matteMaterial;
            body.material.color = GameData.carColor;
            wheels.color = GameData.wheelColor;
            underLight.color = GameData.lightColor;
        }
        else
        {
            gameObject.SetActive(false);
            Debug.Log("Im not chosen");
        }
    

    }
}
