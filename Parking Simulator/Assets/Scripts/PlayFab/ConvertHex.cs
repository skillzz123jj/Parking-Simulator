using UnityEngine;

public class ConvertHex : MonoBehaviour
{
    [SerializeField] PlayFabHandleData playFabSetup;
    [SerializeField] CarCustomization carCustomization;

    public static Color HexToColor(string hex)
    {
        if (hex.StartsWith("#"))
        {
            hex = hex.Substring(1);
        }

        Color color = new Color();
        if (ColorUtility.TryParseHtmlString("#" + hex, out color))
        {
            return color;
        }
        else
        {
            Debug.LogError("Invalid hexadecimal color string: " + hex);
            return Color.white; 
        }
    }
}
