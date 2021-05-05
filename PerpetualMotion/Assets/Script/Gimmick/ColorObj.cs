using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorObj : ColorInfo
{
    [Header("このオブジェクトの色")]
    public COLOR_TYPE nowColor = COLOR_TYPE.Blank;
    SpriteRenderer objImage;

    private void Start()
    {
        objImage = GetComponent<SpriteRenderer>();
        Form_Color(nowColor);
    }

    public void Form_Color(COLOR_TYPE color)
    {
        nowColor = color;
        objImage.sprite = SelectColor(nowColor)[0];
    }
}
