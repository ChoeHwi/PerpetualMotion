using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ColorInfo : MonoBehaviour
{
    
    [SerializeField] private Sprite blankImage;
    [SerializeField] private Sprite redImage;
    [SerializeField] private Sprite buleImage;
    [SerializeField] private Sprite greenImage;

    /// <summary>色の種類</summary>
    public enum COLOR_TYPE 
    {
        Blank,
        Red,
        Bule,
        Green,
    }
    /// <summary>現在の色</summary>
    public COLOR_TYPE type;

    /// <summary>色のタイプを渡すとその色の画像を返す</summary>
    /// <param name="colorType">色のタイプ</param>
    /// <returns></returns>
    public Sprite SelectColor(COLOR_TYPE colorType)
    {
        Sprite image = null;
        switch (colorType)
        {
            case COLOR_TYPE.Blank:
                image = blankImage;
                break;
            case COLOR_TYPE.Red :
                image = redImage;
                break;
            case COLOR_TYPE.Bule:
                image = buleImage;
                break;
            case COLOR_TYPE.Green:
                image = greenImage;
                break;
        }
        return image;
    }
}
