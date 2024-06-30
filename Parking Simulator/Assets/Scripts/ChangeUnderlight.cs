using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChangeUnderLight : MonoBehaviour
{
   
    private Light lightComponent;

    public List<Color> colors;

    public float transitionDuration = 2.0f;

    void Start()
    {
      
        lightComponent = GetComponent<Light>();

        if (colors.Count > 1)
        {
            StartCoroutine(CycleColors());
        }
        else
        {
            Debug.LogError("Please assign at least two colors to the colors list.");
        }
    }

    IEnumerator CycleColors()
    {
        int colorCount = colors.Count;
        int currentColorIndex = 0;

        while (true)
        {
         
            Color currentColor = colors[currentColorIndex];
            Color nextColor = colors[(currentColorIndex + 1) % colorCount];

            yield return StartCoroutine(ChangeColorOverTime(currentColor, nextColor, transitionDuration));

            currentColorIndex = (currentColorIndex + 1) % colorCount;
        }
    }

    IEnumerator ChangeColorOverTime(Color initialColor, Color targetColor, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            Color currentColor = Color.Lerp(initialColor, targetColor, elapsedTime / duration);

            lightComponent.color = currentColor;
            yield return null;
        }

        lightComponent.color = targetColor;
    }
}
