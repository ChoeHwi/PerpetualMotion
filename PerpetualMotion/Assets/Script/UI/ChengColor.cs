using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChengColor : MonoBehaviour
{
    public int selectNumber;
    public Sprite[] UI_Image;
    ColorSelection colorSelection;
    void Start()
    {
        colorSelection = FindObjectOfType<ColorSelection>();
    }
    public void ColorDown()
    {
        colorSelection.selectNumber = selectNumber;
    }
    public void ColorEnter()
    {
        GetComponent<Image>().sprite = UI_Image[1];
    }
    public void Color_Exit()
    {
        GetComponent<Image>().sprite = UI_Image[0];
    }
}
