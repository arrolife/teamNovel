using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    [SerializeField]
    private Image fadeImage;

    [SerializeField]
    private float fadeTime = 1.0f;

    public Action OnFadeOutComplete;
    public Action OnFadeComplete;

    private bool isPlaying;

    public void StartFade()
    {
        if (isPlaying)
            return;

        StartCoroutine(FadeRoutine());
    }

    IEnumerator FadeRoutine()
    {
        isPlaying = true;

        yield return FadeOut();

        OnFadeOutComplete?.Invoke();

        yield return new WaitForSeconds(0.2f);

        yield return FadeIn();

        isPlaying = false;

        OnFadeComplete?.Invoke();
    }

    IEnumerator FadeOut()
    {
        float t = 0;

        Color color = fadeImage.color;

        while (t < fadeTime)
        {
            t += Time.deltaTime;

            float alpha = Mathf.Lerp(0f, 1f, t / fadeTime);

            color.a = alpha;
            fadeImage.color = color;

            yield return null;
        }

        color.a = 1f;
        fadeImage.color = color;
    }

    IEnumerator FadeIn()
    {
        float t = 0;

        Color color = fadeImage.color;

        while (t < fadeTime)
        {
            t += Time.deltaTime;

            float alpha = Mathf.Lerp(1f, 0f, t / fadeTime);

            color.a = alpha;
            fadeImage.color = color;

            yield return null;
        }

        color.a = 0f;
        fadeImage.color = color;
    }
}