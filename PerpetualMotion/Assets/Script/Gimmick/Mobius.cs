using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mobius : MonoBehaviour
{
    [Header("メビウス完成に必要なパーツの数")]
    public PART_TYPE part_type;

    public enum PART_TYPE
    { 
        Part6,
        Part9,
        Part13
    }
    
    [SerializeField] Sprite[] images6 = new Sprite[6];
    [SerializeField] Sprite[] images9 = new Sprite[9];
    [SerializeField] Sprite[] images13 = new Sprite[13];

    public GameManager gameManager;
    int processNum = 0;
    Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }

    public bool FitPiece(int item)
    {
        processNum += item;
        switch (part_type)
        {
            case PART_TYPE.Part6:
                image.sprite = images6[processNum];
                if (images6.Length == processNum + 1)
                {
                    gameManager.OpenResult(true);
                    return true;
                }
                break;
            case PART_TYPE.Part9:
                image.sprite = images9[processNum];
                if (images9.Length == processNum + 1)
                {
                    gameManager.OpenResult(true);
                    return true;
                }
                break;
            case PART_TYPE.Part13:
                image.sprite = images13[processNum];
                if (images13.Length == processNum + 1)
                {
                    gameManager.OpenResult(true);
                    return true;
                }
                break;
        }
        return false;
    }
}
