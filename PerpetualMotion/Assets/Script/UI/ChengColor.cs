using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChengColor : MonoBehaviour
{
    public int selectNumber = 0;
    ColorSelection colorSelection;
    [SerializeField] GameObject[] selecttingBacks;
    void Start()
    {
        colorSelection = FindObjectOfType<ColorSelection>();
    }

    private void Update()
    {
        if (InputChanger.InputInform(InputChanger.INPUTKEY_TYPE.UpSelect))
        {
            selecttingBacks[0].SetActive(true);
            selecttingBacks[1].SetActive(false);
            selecttingBacks[2].SetActive(false);
            selecttingBacks[3].SetActive(false);
            selectNumber = 0;
        }
        if (InputChanger.InputInform(InputChanger.INPUTKEY_TYPE.DownSelect))
        {
            selecttingBacks[0].SetActive(false);
            selecttingBacks[1].SetActive(false);
            selecttingBacks[2].SetActive(true);
            selecttingBacks[3].SetActive(false);
            selectNumber = 2;
        }
        if (InputChanger.InputInform(InputChanger.INPUTKEY_TYPE.LeftSelect))
        {
            selecttingBacks[0].SetActive(false);
            selecttingBacks[1].SetActive(false);
            selecttingBacks[2].SetActive(false);
            selecttingBacks[3].SetActive(true);
            selectNumber = 3;
        }
        if (InputChanger.InputInform(InputChanger.INPUTKEY_TYPE.RightSelect))
        {
            selecttingBacks[0].SetActive(false);
            selecttingBacks[1].SetActive(true);
            selecttingBacks[2].SetActive(false);
            selecttingBacks[3].SetActive(false);
            selectNumber = 1;
        }
    }

    public void ColorDown()
    {
        colorSelection.selectNumber = selectNumber;
    }
}
