using System;
using System.Collections;
using UnityEngine;

public class CurtainController : MonoBehaviour
{
    public enum CurtainType
    {
        Vertical,
        Horizontal
    }

    [Header("Curtain")]

    [SerializeField] private RectTransform curtain;

    [SerializeField] private float moveTime = 0.5f;

    public Action OnBlackOut;
    public Action OnTransitionEnd;

    private bool isPlaying;

    public void StartTransition(CurtainType type)
    {
        if (isPlaying)
            return;

        switch (type)
        {
            case CurtainType.Vertical:
                StartCoroutine(VerticalTransition());
                break;

            case CurtainType.Horizontal:
                StartCoroutine(HorizontalTransition());
                break;
        }
    }

    IEnumerator VerticalTransition()
    {
        isPlaying = true;

        // è„Ç©ÇÁç~ÇËÇÈ
        yield return MoveDown();

        // ê^Ç¡çï
        OnBlackOut?.Invoke();

        yield return new WaitForSeconds(0.2f);

        // â∫Ç÷î≤ÇØÇÈ
        yield return MoveOutDown();

        isPlaying = false;

        OnTransitionEnd?.Invoke();
    }

    IEnumerator MoveDown()
    {
        float h = curtain.rect.height;

        Vector2 startPos = new Vector2(0, h);
        Vector2 endPos = Vector2.zero;

        float t = 0;

        while (t < moveTime)
        {
            t += Time.deltaTime;

            curtain.anchoredPosition =
                Vector2.Lerp(startPos, endPos, t / moveTime);

            yield return null;
        }

        curtain.anchoredPosition = endPos;
    }

    IEnumerator MoveOutDown()
    {
        float h = curtain.rect.height;

        Vector2 startPos = Vector2.zero;
        Vector2 endPos = new Vector2(0, -h);

        float t = 0;

        while (t < moveTime)
        {
            t += Time.deltaTime;

            curtain.anchoredPosition =
                Vector2.Lerp(startPos, endPos, t / moveTime);

            yield return null;
        }

        curtain.anchoredPosition = endPos;
    }

    IEnumerator HorizontalTransition()
    {
        isPlaying = true;

        // ç∂Ç©ÇÁó¨ì¸
        yield return MoveInLeft();

        // ëSñ Çï¢Ç¡ÇΩèuä‘
        OnBlackOut?.Invoke();

        yield return new WaitForSeconds(0.2f);

        // âEÇ÷ó¨èo
        yield return MoveOutRight();

        isPlaying = false;

        OnTransitionEnd?.Invoke();
    }

    IEnumerator MoveInLeft()
    {
        float w = curtain.rect.width;

        Vector2 startPos = new Vector2(-w, 0);
        Vector2 endPos = Vector2.zero;

        float t = 0;

        while (t < moveTime)
        {
            t += Time.deltaTime;

            curtain.anchoredPosition =
                Vector2.Lerp(startPos, endPos, t / moveTime);

            yield return null;
        }

        curtain.anchoredPosition = endPos;
    }

    IEnumerator MoveOutRight()
    {
        float w = curtain.rect.width;

        Vector2 startPos = Vector2.zero;
        Vector2 endPos = new Vector2(w, 0);

        float t = 0;

        while (t < moveTime)
        {
            t += Time.deltaTime;

            curtain.anchoredPosition =
                Vector2.Lerp(startPos, endPos, t / moveTime);

            yield return null;
        }

        curtain.anchoredPosition = endPos;
    }
}