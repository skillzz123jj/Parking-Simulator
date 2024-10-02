using UnityEngine;

public class FlashingLight : MonoBehaviour
{
    public Material reverseOnMaterial;
    public Material reverseOffMaterial;
    public Material brakeOnMaterial;
    public Material brakeOffMaterial;
    public Material indicatorOnMaterial;
    public Material indicatorOffMaterial;

    public Renderer reverseLights;
    public Renderer brakeLights;
    public Renderer leftIndicatorLightMesh;
    public Renderer rightIndicatorLightMesh;

    public static bool isFlash = false;       
    public float flashInterval = 0.5f;        
    private float flashTimer = 0f;
    void Update()
    {
        flashTimer += Time.deltaTime;

        if (flashTimer >= flashInterval)
        {
            isFlash = !isFlash;  
            flashTimer = 0f;     
        }

        if (isFlash)
        {
            reverseLights.sharedMaterial = reverseOnMaterial;
            brakeLights.sharedMaterial = brakeOnMaterial;
            leftIndicatorLightMesh.sharedMaterial = indicatorOnMaterial;
            rightIndicatorLightMesh.sharedMaterial = indicatorOnMaterial;
        }
        else
        {
            reverseLights.sharedMaterial = reverseOffMaterial;
            brakeLights.sharedMaterial = brakeOffMaterial;
            leftIndicatorLightMesh.sharedMaterial = indicatorOffMaterial;
            rightIndicatorLightMesh.sharedMaterial = indicatorOffMaterial;
        }
    }

    private void OnDisable()
    {
        reverseLights.sharedMaterial = reverseOffMaterial;
        brakeLights.sharedMaterial = brakeOffMaterial;
        leftIndicatorLightMesh.sharedMaterial = indicatorOffMaterial;
        rightIndicatorLightMesh.sharedMaterial = indicatorOffMaterial;
        isFlash = false;
    }
}
