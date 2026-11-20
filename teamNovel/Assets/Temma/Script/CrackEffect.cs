using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CrackEffect : MonoBehaviour
{
    [Header("ヒビ画像")]
    public Image crack1;
    public Image crack2;
    public Image crack3;
    public Image crack4;

    [Header("フラッシュ")]
    public Image flash;

    public bool IsPlaying { get; private set; }

    private void Awake()
    {
        ResetEffect();
    }

    public void Play()
    {
        if (IsPlaying) return;

        StopAllCoroutines();
        StartCoroutine(CrackSequence());
    }

    IEnumerator CrackSequence()
    {
        IsPlaying = true;

        ResetEffect();

        crack1.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        crack1.gameObject.SetActive(false);
        crack2.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        crack2.gameObject.SetActive(false);
        crack3.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        crack3.gameObject.SetActive(false);
        crack4.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.15f);

        yield return StartCoroutine(Flash());

        IsPlaying = false;
    }

    IEnumerator Flash()
    {
        flash.gameObject.SetActive(true);

        Color c = flash.color;
        c.a = 0f;
        flash.color = c;

        float t = 0f;

        while (t < 0.05f)
        {
            t += Time.deltaTime;

            c.a = Mathf.Lerp(0f, 1f, t / 0.05f);

            flash.color = c;

            yield return null;
        }

        t = 0f;

        while (t < 0.15f)
        {
            t += Time.deltaTime;

            c.a = Mathf.Lerp(1f, 0f, t / 0.15f);

            flash.color = c;

            yield return null;
        }

        flash.gameObject.SetActive(false);
    }

    public void ResetEffect()
    {
        crack1.gameObject.SetActive(false);
        crack2.gameObject.SetActive(false);
        crack3.gameObject.SetActive(false);
        crack4.gameObject.SetActive(false);

        flash.gameObject.SetActive(false);

        IsPlaying = false;
    }
}