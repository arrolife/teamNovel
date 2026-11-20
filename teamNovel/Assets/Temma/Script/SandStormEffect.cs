using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SandStormEffect : MonoBehaviour
{
    [Header("ƒmƒCƒY‰æ‘œ")]
    [SerializeField]
    private RectTransform noiseA;

    [SerializeField]
    private RectTransform noiseB;

    [Header("—h‚ê•")]
    [SerializeField]
    private float shakeRange = 15f;

    [Header("XVŠÔŠu")]
    [SerializeField]
    private float interval = 0.03f;

    [Header("‰‰oŠÔ")]
    [SerializeField]
    private float duration = 2f;

    public Action OnSandStormEnd;

    private bool isPlaying;

    public void Play()
    {
        if (isPlaying)
            return;

        StartCoroutine(SandStorm());
    }

    public void Play(float playTime)
    {
        duration = playTime;

        if (isPlaying)
            return;

        StartCoroutine(SandStorm());
    }

    IEnumerator SandStorm()
    {
        isPlaying = true;

        noiseA.gameObject.SetActive(true);
        noiseB.gameObject.SetActive(true);

        float timer = 0f;

        while (timer < duration)
        {
            timer += interval;

            noiseA.anchoredPosition =
                UnityEngine.Random.insideUnitCircle * shakeRange;

            noiseB.anchoredPosition =
                UnityEngine.Random.insideUnitCircle * (shakeRange * 1.5f);

            yield return new WaitForSeconds(interval);
        }

        noiseA.anchoredPosition = Vector2.zero;
        noiseB.anchoredPosition = Vector2.zero;

        noiseA.gameObject.SetActive(false);
        noiseB.gameObject.SetActive(false);

        isPlaying = false;

        OnSandStormEnd?.Invoke();
    }
}