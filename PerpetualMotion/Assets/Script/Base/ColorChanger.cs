using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : ColorInfo
{
    SpriteRenderer sprite;
    
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void ColorChange(COLOR_TYPE color)
    {
        sprite.color = SelectColor(color);
    }


    
}
