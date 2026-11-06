using UnityEngine;
using System.Collections;

public class CharacterJump : MonoBehaviour
{
    public RectTransform character;

    public void PlayJump(float height, float duration)
    {
        StartCoroutine(Jump(height, duration));
    }

    IEnumerator Jump(float height, float duration)
    {
        Vector2 startPos = character.anchoredPosition;

        float time = 0;

        while (time < duration)
        {
            float t = time / duration;

            float y = Mathf.Sin(t * Mathf.PI) * height;

            character.anchoredPosition =
                startPos + Vector2.up * y;

            time += Time.deltaTime;

            yield return null;
        }

        character.anchoredPosition = startPos;
    }
}