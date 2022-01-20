﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary> ゲーム開始時にカウントダウンを行うクラス </summary>
public class CountDownSprite : MonoBehaviour
{
    /// <summary> スプライトレンダラーの変数 </summary>
    Image m_spriteImage;
    /// <summary> Countdownのスプライトの変数 </summary>
    [Header("Countdownのスプライト入れる")]
    [SerializeField] private Sprite[] m_countDownSprite = null;
    /// <summary> 参照先の変数 </summary>
    PlayerController m_playerController;

    void Start()
    {
        m_spriteImage = GetComponent<Image>();

        StartCoroutine(CountDown());

        m_playerController = FindObjectOfType<PlayerController>();
        m_playerController.m_active = false;
    }

    /// <summary>
    /// カウントダウンスプライトを表示させるコルーチン
    /// </summary>
    /// <returns></returns>
    public IEnumerator CountDown()
    {
        m_spriteImage.sprite = m_countDownSprite[0];
        yield return new WaitForSeconds(1.0f);
        m_spriteImage.sprite = m_countDownSprite[1];
        yield return new WaitForSeconds(1.0f);
        m_spriteImage.sprite = m_countDownSprite[2];
        yield return new WaitForSeconds(1.0f);
        m_spriteImage.sprite = m_countDownSprite[3];
        yield return new WaitForSeconds(1.0f);
        m_spriteImage.gameObject.SetActive(false);

        //インデックスが3であるならばプレイヤーオブジェクトをアクティブにしてコルーチンを停止させる。
        if (m_countDownSprite[3])
        {
            m_playerController.m_active = true;
            yield break;
        }
    }
}
