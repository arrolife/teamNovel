using System;
using System.Collections;
using UnityEngine;

public class CurtainController : MonoBehaviour
{
    [SerializeField]
    private RectTransform curtain;

    [SerializeField]
    private float moveTime = 0.5f;

    public Action OnBlackOut;
    public Action OnTransitionEnd;

    private bool isPlaying;

    public void StartTransition()
    {
        if (isPlaying)
            return;

        StartCoroutine(Transition());
    }

    IEnumerator Transition()
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
}