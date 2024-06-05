using System.Collections;
using UnityEngine;

public class Indicators : MonoBehaviour
{
    public MeshRenderer leftIndicatorLight;
    public MeshRenderer rightIndicatorLight;
    private bool leftIndicatorOn = false;
    private bool rightIndicatorOn = false;
    private Coroutine leftCoroutine;
    private Coroutine rightCoroutine;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (leftCoroutine != null)
            {
                StopCoroutine(leftCoroutine);
                leftIndicatorLight.enabled = false;
            }

            leftIndicatorOn = !leftIndicatorOn;
            rightIndicatorOn = false;
            rightIndicatorLight.enabled = false; // Ensure the right indicator is off
            if (leftIndicatorOn)
            {
                leftCoroutine = StartCoroutine(BlinkIndicator(leftIndicatorLight, () => leftIndicatorOn));
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (rightCoroutine != null)
            {
                StopCoroutine(rightCoroutine);
                rightIndicatorLight.enabled = false;

            }

            rightIndicatorOn = !rightIndicatorOn;
            leftIndicatorOn = false;
            leftIndicatorLight.enabled = false; // Ensure the left indicator is off
            if (rightIndicatorOn)
            {
                rightCoroutine = StartCoroutine(BlinkIndicator(rightIndicatorLight, () => rightIndicatorOn));
            }
        }
    }

    IEnumerator BlinkIndicator(MeshRenderer indicatorLight, System.Func<bool> isIndicatorOn)
    {
        while (isIndicatorOn())
        {
            indicatorLight.enabled = !indicatorLight.enabled;
            yield return new WaitForSeconds(0.5f);
        }
        indicatorLight.enabled = false;
    }
}
