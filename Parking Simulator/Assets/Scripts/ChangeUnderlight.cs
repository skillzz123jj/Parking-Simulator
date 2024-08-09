using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChangeUnderLight : MonoBehaviour
{
    [SerializeField] Light lightComponent;
    private Coroutine colorCycleCoroutine;  

    public List<Color> colors;
    public float transitionDuration = 2.0f;


    private void OnEnable()
    {
        if (colorCycleCoroutine == null) 
        {
            colorCycleCoroutine = StartCoroutine(CycleColors());
        }
    }

    private void OnDisable()
    {
        if (colorCycleCoroutine != null)  
        {
            StopCoroutine(colorCycleCoroutine);
            colorCycleCoroutine = null;
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
