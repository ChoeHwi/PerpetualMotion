﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour
{
    PlayerController con;
    [SerializeField] Image LifeImage;
    public int life;
    // Start is called before the first frame update
    void Start()
    {
        con = GameObject.Find("Player").GetComponent<PlayerController>();
        life = con.playerHp;
    }

    // Update is called once per frame
    void Update()
    {
        life = con.playerHp;
        CHANG_COLOR();
    }
    public void CHANG_COLOR()
    {
        switch (life)
        {
            case 4:
                LifeImage.fillAmount = 0.8f;
                break;
            case 3:
                LifeImage.fillAmount = 0.6f;
                break;
            case 2:
                LifeImage.fillAmount = 0.4f;
                break;
            case 1:
                LifeImage.fillAmount = 0.2f;
                break;
            case 0:
                LifeImage.fillAmount = 0;
                break;
            default:
                break;
        }
    }
}
