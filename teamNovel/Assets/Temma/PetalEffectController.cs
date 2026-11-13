using System;
using System.Collections;
using UnityEngine;

public class FlowerPetalController : MonoBehaviour
{
    [Header("Flower Effect")]
    [SerializeField] private ParticleSystem petalParticleSystem; // 花吹雪のパーティクル

    public Action OnEffectStart;
    public Action OnEffectEnd;

    private bool isPlaying;

    /// 花吹雪エフェクトを開始する（CSV等の外部から呼び出す用）
    public void StartFlowerEffect()
    {
        if (isPlaying)
            return;

        StartCoroutine(FlowerEffectRoutine());
    }

    IEnumerator FlowerEffectRoutine()
    {
        isPlaying = true;

        // エフェクト開始時のコールバック（必要に応じてゲームマネージャー等に通知）
        OnEffectStart?.Invoke();

        if (petalParticleSystem != null)
        {
            petalParticleSystem.gameObject.SetActive(true);
            petalParticleSystem.Play();
        }

        // Particle Systemの再生が自然に終了する（すべてのパーティクルが消える）まで待機
        if (petalParticleSystem != null)
        {
            while (petalParticleSystem.isPlaying)
            {
                yield return null;
            }

            petalParticleSystem.gameObject.SetActive(false);
        }

        isPlaying = false;

        // エフェクト終了時のコールバック（「演出が終わるまで次の行に進まない」等の制御に利用可能）
        OnEffectEnd?.Invoke();
    }
}