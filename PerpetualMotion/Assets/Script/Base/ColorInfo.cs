using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorInfo : MonoBehaviour
{
    public COLOR_TYPE colorType;
    public enum COLOR_TYPE 
    {
        White,
        Black,
        Red,
        Bulu,
        Yellow,
        Green
    }
    
    public Color SelectColor(COLOR_TYPE colorType)
    {
        Color color = new Color(255, 255, 255);
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
        }

        return color;
    }
}
