﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorInfo : MonoBehaviour
{
    public Color color;
    public enum COLOR_TYPE 
    {
        White,
        Black,
        Red,
        Bulu,
        Yellow,
        Green,
        Gray
    }
    public COLOR_TYPE type;
    
    public Color SelectColor(COLOR_TYPE colorType)
    {
        color = new Color(255, 255, 255);
        switch (colorType)
        {
            case COLOR_TYPE.White:
                color = new Color(255, 255, 255);
                break;
            case COLOR_TYPE.Black:
                color = new Color(0, 0, 0);
                break;
            case COLOR_TYPE.Red :
                color = new Color(255, 0, 0);
                break;
            case COLOR_TYPE.Bulu:
                color = new Color(0, 255, 0);
                break;
            case COLOR_TYPE.Yellow:
                color = new Color(255, 255, 0);
                break;
            case COLOR_TYPE.Green:
                color = new Color(0, 0, 255);
                break;
            case COLOR_TYPE.Gray:
                color = new Color(116, 126, 130);
                break;
        }

        return color;
    }
}
