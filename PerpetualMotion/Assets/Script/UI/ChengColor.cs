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

    private void Update()
    {
        if (Input.)
        {

        }
        else if (Input.GetKeyDown(KeyCode.D))
        {

        }
    }

    public void ColorDown(int colorNumber)
    {
        colorSelection.selectNumber = colorNumber;
    }
}
