using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthObject : ColorInfo
{
    [Header("このオブジェクトの色")]
    public COLOR_TYPE nowColor = COLOR_TYPE.Blank;
    [Header("隠れたまま移動可能かどうか")]
    public bool canMove;
    /// <summary>画像</summary>
    SpriteRenderer objImage;
    /// <summary>目</summary>
    public GameObject eyes;
    public GameObject RootObject;
    /// <summary> AudioManagerを参照する変数 </summary>
    AudioManager audioManager;

    private void Start()
    {
        objImage = GetComponent<SpriteRenderer>();
        Form_Color(nowColor);
        if (eyes)
        {
            Instantiate(eyes, this.transform);
            foreach (Transform item in transform)
            {
                item.gameObject.SetActive(false);
            }
        }

        if (GameObject.FindObjectOfType<AudioManager>())
        {
            audioManager = GameObject.FindObjectOfType<AudioManager>();
        }
    }

    public void ObjSet()
    {
        this.gameObject.transform.parent = RootObject.gameObject.transform;
    }
    public void ObjOut()
    {
        this.gameObject.transform.parent = null;
    }

    public void EyeController_Tr()
    {
        foreach (Transform item in transform)
        {
            item.gameObject.SetActive(true);
            //スイッチONのSE
            audioManager.PlaySE(audioManager.audioClips[9]);
        }
    }
    public void EyeController_Fa()
    {
        foreach (Transform item in transform)
        {
            item.gameObject.SetActive(false);
        }
    }
    public void Form_Color(COLOR_TYPE color)
    {
        nowColor = color;
        objImage.sprite = SelectColor(nowColor)[0];
    }
}
