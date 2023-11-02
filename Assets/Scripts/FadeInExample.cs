using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInExample : MonoBehaviour
{
    public float fadeSpeed = 1.5f;
    public bool fadeInOnStart = true;
    public bool fadeOutOnExit = true;

    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (fadeInOnStart)
        {
            canvasGroup.alpha = 0f;
            StartCoroutine(FadeIn());
        }
    }

    IEnumerator FadeIn()
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }
}
