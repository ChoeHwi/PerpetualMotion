using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorInfo : MonoBehaviour
{
<<<<<<< HEAD
    /// <summary>オブジェクトの画像</summary>
    private Sprite image;

    [SerializeField] private Sprite blankImage;
    [SerializeField] private Sprite redImage;
    [SerializeField] private Sprite buluImage;
    [SerializeField] private Sprite greenImge;

=======
    public Color color;
>>>>>>> otsuki
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
<<<<<<< HEAD
=======
        color = new Color(255, 255, 255);
>>>>>>> otsuki
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
