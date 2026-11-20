using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlashFadeController : MonoBehaviour
{
    [SerializeField]
    private RectTransform flashRect;

    [SerializeField]
    private Image flashImage;

    [SerializeField]
    [Min(0.01f)]
    private float expandTime = 0.5f;

    [SerializeField]
    [Min(0.01f)]
    private float fadeOutTime = 1.0f;

    [SerializeField]
    private float maxScale = 50f;

    // 画面が完全に白くなった瞬間
    // （背景切り替え用）
    public Action OnWhiteOut;

    // 演出終了
    public Action OnFlashEnd;

    private bool isPlaying;

    private void Awake()
    {
        if (flashRect == null)
        {
            flashRect = GetComponent<RectTransform>();
        }

        if (flashImage == null)
        {
            flashImage = GetComponent<Image>();
        }

        ResetState();
    }

    private void ResetState()
    {
        flashRect.localScale = Vector3.zero;

        Color color = flashImage.color;
        color.a = 1f;
        flashImage.color = color;
    }

    // CSVから呼び出す
    public void StartFlash()
    {
        if (isPlaying)
        {
            return;
        }

        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        isPlaying = true;

        yield return ExpandFlash();

        // 完全に白くなった瞬間
        OnWhiteOut?.Invoke();

        yield return new WaitForSeconds(0.2f);

        yield return FadeOut();

        isPlaying = false;

        OnFlashEnd?.Invoke();

        ResetState();
    }

    private IEnumerator ExpandFlash()
    {
        float t = 0f;

        while (t < expandTime)
        {
            t += Time.deltaTime;

            float rate =
                Mathf.SmoothStep(
                    0f,
                    1f,
                    t / expandTime);

            float scale =
                Mathf.Lerp(
                    0f,
                    maxScale,
                    rate);

            flashRect.localScale =
                Vector3.one * scale;

            yield return null;
        }

        flashRect.localScale =
            Vector3.one * maxScale;
    }

    private IEnumerator FadeOut()
    {
        float t = 0f;

        Color color = flashImage.color;

        while (t < fadeOutTime)
        {
            t += Time.deltaTime;

            float rate =
                Mathf.SmoothStep(
                    1f,
                    0f,
                    t / fadeOutTime);

            color.a = rate;
            flashImage.color = color;

            yield return null;
        }

        color.a = 0f;
        flashImage.color = color;
    }
}
