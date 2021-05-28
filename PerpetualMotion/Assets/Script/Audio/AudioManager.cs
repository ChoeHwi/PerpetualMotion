using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    /// <summary>タイトル画面のBGM</summary>
    public AudioClip titleBGM;
    /// <summary>ゲームオーバーのSE</summary>
    public AudioClip gameOverBGM;
    /// <summary>各ステージのBGM</summary>
    public AudioClip[] gameBGM;
    /// <summary> 複数のAudioClipを扱う。index番号を設定 </summary>
    [Header("複数のAudioClipを扱う。index番号を設定")]
    public AudioClip[] audioClips;
    /// <summary>trueでmute</summary>
    public bool m_mute = false;
    /// <summary>BGMのフェードアウトスピード</summary>
    [SerializeField] float fadeSpeed;

    [SerializeField] public AudioSource audioSourceBGM;
    [SerializeField] public AudioSource audioSourceSE;
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (SceneManager.GetActiveScene().name == "Title")
        {
            PlayBgm(titleBGM);
        }
    }
    // シーンが切り替わったときにシーン名ごとにBGMを切り替える
    void OnSceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        switch (SceneManager.GetActiveScene().name)
        { 
            case "Title":
                PlayBgm(titleBGM);
                break;
            case "Tutorial":
                PlayBgm(gameBGM[0]);
                break;
                //case "Scene1":
                //    PlayBgm(gameBGM[0]);
                //    break;
        }
    }

    //BGMを再生
    public void PlayBgm(AudioClip bgm)
    {
        audioSourceBGM.clip = bgm;
        audioSourceBGM.loop = true;
        audioSourceBGM.Play();
    }

    //ミュートを切り替える
    public void Mute()
    {
        audioSourceSE.mute = m_mute;
        audioSourceBGM.mute = m_mute;
    }

    //現在再生中のBGMをフェードアウト
    public IEnumerator FadeOutBGM()
    {
        while (audioSourceBGM.volume > 0)
        {
            audioSourceBGM.volume -= fadeSpeed;
            yield return new WaitForSeconds(0.017f);
        }
    }  
    //SEを再生
    public void PlaySE(AudioClip SE)
    {
        audioSourceSE.pitch = 1;
        audioSourceSE.PlayOneShot(SE);
    }
}
