using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : ColorInfo
{
    [SerializeField]
    private Image playerImage;
    SpriteRenderer sprite;
    ColorInfo ci;
    
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        ci = GetComponent<ColorInfo>();
    }

    public void ColorChange(COLOR_TYPE color)
    {

    }


    
}
