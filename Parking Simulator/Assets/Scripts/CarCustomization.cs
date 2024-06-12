using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarCustomization : MonoBehaviour
{
    [SerializeField] GameObject carBody;
    [SerializeField] GameObject carLight;
    [SerializeField] GameObject colorObject;
    [SerializeField] GameObject lightObject;

    [SerializeField] Material defaultMaterial;

    [SerializeField] TMP_Text text;

    bool changeLights;


    // Start is called before the first frame update
    void Awake()
    {
       
       if (GameData.carColor == null)
        {
            GameData.carColor = defaultMaterial;
        }
       ChangeCarColor(GameData.carColor);
       if (GameData.lightsOn)
       {
         
            carLight.SetActive(true);
            carLight.GetComponent<Light>().color = GameData.underLightColor;
       }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeCarColor(Material color)
    {
        carBody.GetComponent<MeshRenderer> ().material = color;
        GameData.carColor = color;
    }

       public void ChangeCarLight(Image colorImage)
    {
        GameData.lightsOn = true;
        carLight.SetActive(true);
        Color color = colorImage.color;
        GameData.underLightColor = color;
        carLight.GetComponent<Light>().color = color;
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