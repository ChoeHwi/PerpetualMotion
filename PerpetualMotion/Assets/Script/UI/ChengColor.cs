using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChengColor : MonoBehaviour
{
    public int selectNumber;
    ColorSelection colorSelection;
    void Start()
    {
        colorSelection = FindObjectOfType<ColorSelection>();
    }
    public void ColorDown(int colorNumber)
    {
        colorSelection.selectNumber = colorNumber;
    }
}
