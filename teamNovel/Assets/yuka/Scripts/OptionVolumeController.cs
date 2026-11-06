using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class OptionVolumeController : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Slider bgmSlider;
    [SerializeField] UnityEngine.UI.Slider seSlider;
    [SerializeField] AudioClip testSE;
    [SerializeField] float testSEInterval = 0.2f;

    [SerializeField] float initialBgmVolume = 0.5f;
    [SerializeField] float initialSeVolume = 0.5f;

    float lastTestSETime = -1f;

    SoundManager soundManager;

    void Start()
    {
        soundManager = SoundManager.Instance;

        if (soundManager == null)
        {
            Debug.LogError("SoundManagerが見つかりません");
            return;
        }

        // 保存済みの音量を読み込む
        float bgmVolume = PlayerPrefs.GetFloat("BGMVolume", initialBgmVolume);
        float seVolume = PlayerPrefs.GetFloat("SEVolume", initialSeVolume);

        // スライダーに初期値を設定
        bgmSlider.SetValueWithoutNotify(bgmVolume);
        seSlider.SetValueWithoutNotify(seVolume);

        // 音量を反映
        soundManager.BgmVolume = bgmVolume;
        soundManager.SeVolume = seVolume;

        // スライダー変更時の処理を登録
        bgmSlider.onValueChanged.AddListener(SetBgmVolume);
        seSlider.onValueChanged.AddListener(SetSeVolume);
    }

    // BGM音量変更
    public void SetBgmVolume(float value)
    {
        soundManager.BgmVolume = value;

        PlayerPrefs.SetFloat("BGMVolume", value);
        PlayerPrefs.Save();

        PlayTestSE();
    }

    // SE音量変更
    public void SetSeVolume(float value)
    {
        soundManager.SeVolume = value;

        PlayerPrefs.SetFloat("SEVolume", value);
        PlayerPrefs.Save();

        PlayTestSE();
    } 

    public void ResetVolume() 
    {
        if (soundManager == null)
            return;

        //スライダーを初期値に戻す
        bgmSlider.value = initialBgmVolume;
        seSlider.value = initialSeVolume;
        
        SetBgmVolume(initialBgmVolume);
        SetSeVolume(initialSeVolume);
    } 

    // テスト用SE
    void PlayTestSE()
    {
        if (testSE == null || soundManager == null)
            return;

        // 短時間に連続再生しない
        if (Time.unscaledTime - lastTestSETime < testSEInterval)
            return;

        lastTestSETime = Time.unscaledTime;

        soundManager.PlaySe(testSE);
    }

    void OnDestroy()
    {
        if (bgmSlider != null)
            bgmSlider.onValueChanged.RemoveListener(SetBgmVolume);

        if (seSlider != null)
            seSlider.onValueChanged.RemoveListener(SetSeVolume);
    }
}
