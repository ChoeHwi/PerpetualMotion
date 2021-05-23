using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ColorInfo : MonoBehaviour
{
    [SerializeField] private Sprite[] blankImages;
    [SerializeField] private Sprite[] redImages;
    [SerializeField] private Sprite[] buleImages;
    [SerializeField] private Sprite[] greenImages;

    /// <summary>色の種類</summary>
    public enum COLOR_TYPE 
    {
        Blank,
        Red,
        Bule,
        Green,
    }

    /// <summary>現在の色</summary>
    COLOR_TYPE type;

    /// <summary>色のタイプを渡すとその色の画像を返す</summary>
    /// <param name="colorType">色のタイプ</param>
    /// <returns></returns>
    public Sprite[] SelectColor(COLOR_TYPE colorType)
    {
        Sprite[] image = null;
        switch (colorType)
        {
            case COLOR_TYPE.Blank:
                image = blankImages;
                break;
            case COLOR_TYPE.Red :
                image = redImages;
                break;
            case COLOR_TYPE.Bule:
                image = buleImages;
                break;
            case COLOR_TYPE.Green:
                image = greenImages;
                break;
        }
        return image;
    }
}
