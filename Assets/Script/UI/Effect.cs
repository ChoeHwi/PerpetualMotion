﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Effect : MonoBehaviour
{
	/// <summary>敵に追いかけられているか</summary>
	bool m_beChased = false;
	/// <summary>呼び出された数</summary>
	int callCount = 0;
	/// <summary>このスクリプトがついているImage</summary>
	Image effectImg;
	[Header("0は赤色,1は緑色,2は青色,3は透過度")]
	[Header("ここで細かい色を調整する")]
	/// <summary>敵に見った時の変数</summary>
	public float[] startColor = new float[4];
	/// <summary>敵に見ってない時の変数</summary>
	float[] stopColor = new float[4] { 0, 0, 0, 0 };
	/// <summary>ループしたときの変数</summary>
	float loopTime;
	/// <summary> フェイドの消える時間を調整</summary>
	[Header("フェイドの消える時間を調整する")]
	public float fadeTime = 0.0f;
	//音
	/// <summary>BGM,SEを管理する</summary>
	AudioManager audioManager;
	void Start()
	{
		if (GameObject.FindObjectOfType<AudioManager>())
		{
			audioManager = GameObject.FindObjectOfType<AudioManager>();
		}
		effectImg = GetComponent<Image>();
		effectImg.color = Color.clear;
		loopTime = startColor[3];
	}

	void Update()
	{
        if (m_beChased)
        {
			if (audioManager)
            {
				audioManager.PlaySELoop(audioManager.audioClips[9]);
			}

            this.effectImg.color = new Color(startColor[0], startColor[1], startColor[2], startColor[3]);
			startColor[3] -= Time.deltaTime / fadeTime;
			if (startColor[3] < 0)
			{
				//audioManager.PlaySE(audioManager.audioClips[9]);
				startColor[3] = loopTime;
			}
		}
		else
		{
			this.effectImg.color = new Color(stopColor[0], stopColor[1], stopColor[2], stopColor[3]);
		}
	}

	public void EffectStart()
    {
		m_beChased = true;
		callCount++;
	}

	public void EffectStop()
	{
		callCount--;
		if (callCount == 0)
        {
			m_beChased = false;
		}
	}
}
