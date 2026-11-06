using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] AudioSource bgmAudioSource;
    [SerializeField] AudioSource seAudioSource;
    void Start()
    {
        // Inspectorに設定されたBGMを再生
        if (bgmAudioSource.clip != null)
        {
            PlayBgm(bgmAudioSource.clip);
        }
    }

    public float BgmVolume
    {
        get => bgmAudioSource.volume;
        set => bgmAudioSource.volume = Mathf.Clamp01(value);
    }

    public float SeVolume
    {
        get => seAudioSource.volume;
        set => seAudioSource.volume = Mathf.Clamp01(value);
    }

    void Awake()
    {
        // 重複生成を防ぐ
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // シーン移動しても破棄しない
        DontDestroyOnLoad(gameObject); 

        // 保存された音量を読み込む
        BgmVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        SeVolume = PlayerPrefs.GetFloat("SEVolume", 1f);
    }

    // BGM再生
    public void PlayBgm(AudioClip clip)
    {
        if (clip == null) return;

        // 同じBGMなら再生位置を維持
        if (bgmAudioSource.clip == clip)
        {
            if (!bgmAudioSource.isPlaying)
            {
                bgmAudioSource.Play();
            }

            return;
        }

        // 違うBGMなら切り替える
        bgmAudioSource.clip = clip;
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();
    }

    // SE再生
    public void PlaySe(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("SEのAudioClipが設定されていません");
            return;
        }

        if (seAudioSource == null)
        {
            Debug.LogError("SE用AudioSourceがありません");
            return;
        }

        Debug.Log("SE再生：" + clip.name);

        seAudioSource.PlayOneShot(clip);
    }

    // BGM停止
    public void StopBgm()
    {
        bgmAudioSource.Stop();
    }
    
    public void PlaySeAndChangeScene(AudioClip clip,string sceneName)
    {
        if (clip == null) return;
        StartCoroutine(SeAndChangeSceneCoroutine(clip, sceneName));
    }

    IEnumerator SeAndChangeSceneCoroutine(AudioClip clip,string sceneName)
    {
        PlaySe(clip);
        yield return new WaitForSecondsRealtime(clip.length);
        //SceneManager.LoadScene(sceneName);
    }

    public void PlayButtonSE(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("SEが設定されていません");
            return;
        }

        if (seAudioSource == null)
        {
            Debug.LogError("SE用AudioSourceがありません");
            return;
        }

        seAudioSource.PlayOneShot(clip);
    }
}