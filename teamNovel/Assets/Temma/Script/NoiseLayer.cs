using UnityEngine;
using UnityEngine.UI;

public class NoiseLayer : MonoBehaviour
{
    private RectTransform rect;
    private Image image;

    [SerializeField]
    private float moveRange = 150f;

    [SerializeField]
    private float minAlpha = 0.03f;

    [SerializeField]
    private float maxAlpha = 0.10f;

    private bool isNoiseActive = false;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();

        SetAlpha(0f);
    }

    private void Update()
    {
        if (!isNoiseActive)
        {
            return;
        }

        rect.anchoredPosition = new Vector2(
            Random.Range(-moveRange, moveRange),
            Random.Range(-moveRange, moveRange));

        Color c = image.color;

        c.a = Random.Range(minAlpha, maxAlpha);

        image.color = c;
    }

    public void NoiseOn()
    {
        isNoiseActive = true;
    }

    public void NoiseOff()
    {
        isNoiseActive = false;
        SetAlpha(0f);
    }

    private void SetAlpha(float alpha)
    {
        Color c = image.color;
        c.a = alpha;
        image.color = c;
    }
}