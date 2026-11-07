using System;
using System.Collections;
using UnityEngine;

public class BlinkController : MonoBehaviour
{
    [Header("Upper Eyelid")]
    [SerializeField] private RectTransform upperLid;

    [Header("Lower Eyelid")]
    [SerializeField] private RectTransform lowerLid;

    [Header("Animation")]
    [SerializeField] private float moveTime = 0.15f;

    [SerializeField] private float closeWaitTime = 0.1f;

    public Action OnBlackOut;
    public Action OnTransitionEnd;

    private bool isPlaying;

    private float halfScreenHeight;

    private void Awake()
    {
        halfScreenHeight = Screen.height * 0.5f;

        ResetPosition();
    }

    /// 瞬き開始
    public void StartTransition()
    {
        if (isPlaying)
            return;

        StartCoroutine(BlinkTransition());
    }

    private IEnumerator BlinkTransition()
    {
        isPlaying = true;

        // 目を閉じる
        yield return CloseEye();

        // 完全に閉じた瞬間
        OnBlackOut?.Invoke();

        yield return new WaitForSeconds(closeWaitTime);

        // 目を開く
        yield return OpenEye();

        isPlaying = false;

        OnTransitionEnd?.Invoke();
    }

    private IEnumerator CloseEye()
    {
        float timer = 0f;

        Vector2 upperStart =
            new Vector2(0f, halfScreenHeight);

        Vector2 upperEnd =
            Vector2.zero;

        Vector2 lowerStart =
            new Vector2(0f, -halfScreenHeight);

        Vector2 lowerEnd =
            Vector2.zero;

        while (timer < moveTime)
        {
            timer += Time.deltaTime;

            float rate =
                Mathf.Clamp01(timer / moveTime);

            rate = Mathf.SmoothStep(0f, 1f, rate);

            upperLid.anchoredPosition =
                Vector2.Lerp(
                    upperStart,
                    upperEnd,
                    rate);

            lowerLid.anchoredPosition =
                Vector2.Lerp(
                    lowerStart,
                    lowerEnd,
                    rate);

            yield return null;
        }

        upperLid.anchoredPosition = upperEnd;
        lowerLid.anchoredPosition = lowerEnd;
    }

    private IEnumerator OpenEye()
    {
        float timer = 0f;

        Vector2 upperStart =
            Vector2.zero;

        Vector2 upperEnd =
            new Vector2(0f, halfScreenHeight);

        Vector2 lowerStart =
            Vector2.zero;

        Vector2 lowerEnd =
            new Vector2(0f, -halfScreenHeight);

        while (timer < moveTime)
        {
            timer += Time.deltaTime;

            float rate =
                Mathf.Clamp01(timer / moveTime);

            rate = Mathf.SmoothStep(0f, 1f, rate);

            upperLid.anchoredPosition =
                Vector2.Lerp(
                    upperStart,
                    upperEnd,
                    rate);

            lowerLid.anchoredPosition =
                Vector2.Lerp(
                    lowerStart,
                    lowerEnd,
                    rate);

            yield return null;
        }

        upperLid.anchoredPosition = upperEnd;
        lowerLid.anchoredPosition = lowerEnd;
    }

    // 初期状態へ戻す
    public void ResetPosition()
    {
        upperLid.anchoredPosition =
            new Vector2(0f, halfScreenHeight);

        lowerLid.anchoredPosition =
            new Vector2(0f, -halfScreenHeight);
    }
}