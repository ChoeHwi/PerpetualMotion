using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorInfo : MonoBehaviour
{
    /// <summary>オブジェクトの画像</summary>
    private Sprite image;

    [SerializeField] private Sprite blankImage;
    [SerializeField] private Sprite redImage;
    [SerializeField] private Sprite buluImage;
    [SerializeField] private Sprite greenImge;

    public enum COLOR_TYPE 
    {
        Blank,
        Red,
        Bulu,
        Green,
    }
    public COLOR_TYPE type;
    
    public Sprite SelectColor(COLOR_TYPE colorType)
    {
        switch (colorType)
        {
            case COLOR_TYPE.Blank:
                image = blankImage;
                break;
            case COLOR_TYPE.Red :
                image = redImage;
                break;
            case COLOR_TYPE.Bulu:
                image = buluImage;
                break;
            case COLOR_TYPE.Green:
                image = greenImge;
                break;
        }

        return image;
    }
}
