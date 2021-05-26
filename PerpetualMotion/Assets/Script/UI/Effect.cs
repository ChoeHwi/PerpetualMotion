using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Effect : MonoBehaviour
{/// <summary>敵に見つかった時の演出</summary>
	EnemyController e_controller;
	Image effectImg;
	bool effectStart = true;
	float[] startColor = new float[4] { 10.0f, 0f, 0f, 0.6f };
	float[] stopColor = new float[4] { 0, 0, 0, 0 };
	void Start()
	{
		e_controller = FindObjectOfType<EnemyController>();
		effectImg = GetComponent<Image>();
		effectImg.color = Color.clear;
	}

	void Update()
	{
        if (e_controller.tracking == true)
        {
			if (effectStart)
			{
				this.effectImg.color = new Color(startColor[0], startColor[1], startColor[2], startColor[3]);
				startColor[3] -= Time.deltaTime/2f;
				if (startColor[3] < 0)
                {
					startColor[3] = 0.6f;
				}
			}
		}
		else
		{
			this.effectImg.color = new Color(stopColor[0], stopColor[1], stopColor[2], stopColor[3]);
		}
	}
}
