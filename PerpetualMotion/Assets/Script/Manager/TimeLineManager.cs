using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary> タイムラインを制御するクラス </summary>
public class TimeLineManager : MonoBehaviour
{
    /// <summary>  </summary>
    private PlayableDirector _playableDirector;
    /// <summary> PlayableDirectorがAddComponentされたオブジェクト </summary>
    public GameObject _director;

    private void Awake()
    {
        _playableDirector = GetComponent<PlayableDirector>();
        
    }

    public void PlayableStop() 
    {
        _playableDirector.stopped += Director_Stopped;
    }

    /// <summary>
    /// タイムライン再生時オブジェクトをアクティブにする
    /// </summary>
    /// <param name="obj"></param>
    public void Director_Played(PlayableDirector obj) 
    {
        _playableDirector.played += Director_Played;
        _director.SetActive(true);
    }

    /// <summary>
    /// タイムライン再生時オブジェクトを非アクティブにする
    /// </summary>
    /// <param name="obj"></param>
    public void Director_Stopped(PlayableDirector obj) 
    {
        _director.SetActive(false);
    }

    /// <summary>
    /// タイムラインを再生
    /// </summary>
    public void StartTimeLine()
    {
        _playableDirector.Play();
    }
}
