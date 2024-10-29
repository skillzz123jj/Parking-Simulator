using System.Collections;
using TMPro;
using UnityEngine;

public class FadeText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    void Start()
    {
        StartCoroutine(FadeTextCoroutine());
    }

   IEnumerator FadeTextCoroutine()
    {
        float startAlpha = text.alpha;
        yield return new WaitForSeconds(10);

        for (float t = 0; t < 5; t += Time.deltaTime)
        {
            text.alpha = Mathf.Lerp(startAlpha, 0, t / 5);
            yield return null;
        }

        text.alpha = 0;
    }
}
