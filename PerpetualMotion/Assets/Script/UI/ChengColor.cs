using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChengColor : MonoBehaviour
{
    public int selectNumber = 0;
    ColorSelection colorSelection;
    void Start()
    {
        colorSelection = FindObjectOfType<ColorSelection>();
    }

    private void Update()
    {
        if (InputChanger.InputInform(InputChanger.INPUTKEY_TYPE.UpSelect))
        {
            selectNumber = 0;
        }
        if (InputChanger.InputInform(InputChanger.INPUTKEY_TYPE.DownSelect))
        {
            selectNumber = 2;
        }
        if (InputChanger.InputInform(InputChanger.INPUTKEY_TYPE.LeftSelect))
        {
            selectNumber = 3;
        }
        if (InputChanger.InputInform(InputChanger.INPUTKEY_TYPE.RightSelect))
        {
            selectNumber = 1;
        }
    }

    public void ColorDown()
    {
        colorSelection.selectNumber = selectNumber;
    }
}
