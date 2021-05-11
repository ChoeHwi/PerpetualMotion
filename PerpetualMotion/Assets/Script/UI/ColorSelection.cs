using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSelection : MonoBehaviour
{
    public int selectNumber = 0;
    [Header("赤色のバー")]
    [SerializeField] Image select_red;
    [Header("緑のバー")]
    [SerializeField] Image select_green;
    [Header("青色のバー")]
    [SerializeField] Image select_blue;
    /// <summary>3本のゲージの値</summary>
    float[] usageTimes = new float[3] { 1f, 1f, 1f, };
    /// <summary>ゲージの減少速度</summary>
    [Header("色の使用スピード(0に近いと遅く減る)")]
    public float speed;
    /// <summary>ゲージの回復速度</summary>
    [Header("色の回復スピード(0に近いと遅く回復する)")]
    public float heelSpeed;
    PlayerController playerController;

    void Start()
    {
        select_red = GameObject.Find("Red_Image").GetComponent<Image>();
        select_green = GameObject.Find("Green_Image").GetComponent<Image>();
        select_blue = GameObject.Find("Blue_Image").GetComponent<Image>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerController.Form_Color(ColorInfo.COLOR_TYPE.Red);
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (selectNumber == 0)
            {
                selectNumber = 3;
            }
            else
            {
                for (int i = selectNumber - 1; i >= 0; i--)
                {
                    if (usageTimes[i] > 0)
                    {
                        selectNumber = i;
                        break;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (selectNumber == 2)
            {
                selectNumber = 3;
            }
            else if (selectNumber != 3)
            {
                for (int i = selectNumber + 1; i < usageTimes.Length; i++)
                {
                    if (usageTimes[i] > 0)
                    {
                        selectNumber = i;
                        break;
                    }
                }
            }
            else
            {
                selectNumber = 0;
            }
        }

        selectColor();
    }
    void selectColor()
    {
        switch (selectNumber)
        {
            case 0:
                if (playerController.nowColor != ColorInfo.COLOR_TYPE.Red)
                {
                    playerController.Form_Color(ColorInfo.COLOR_TYPE.Red);
                }
                if (usageTimes[0] > 0)
                {
                    usageTimes[0] -= speed / 100 * Time.deltaTime;
                    select_red.fillAmount = usageTimes[0];
                    if (usageTimes[1] < 1)
                    {
                        usageTimes[1] += heelSpeed / 100 * Time.deltaTime;
                        select_green.fillAmount = usageTimes[1];
                    }
                    if (usageTimes[2] < 1)
                    {
                        usageTimes[2] += heelSpeed / 100 * Time.deltaTime;
                        select_blue.fillAmount = usageTimes[2];
                    }
                }
                else//ゲージがなくなったら合図をだす
                {
                    shiftColor();
                    selectNumber = 3;
                }
                break;
            case 1:
                if (playerController.nowColor != ColorInfo.COLOR_TYPE.Green)
                {
                    playerController.Form_Color(ColorInfo.COLOR_TYPE.Green);
                }
                if (usageTimes[1] > 0)
                {
                    usageTimes[1] -= speed / 100 * Time.deltaTime;
                    select_green.fillAmount = usageTimes[1];
                    if (usageTimes[0] < 1)
                    {
                        usageTimes[0] += heelSpeed / 100 * Time.deltaTime;
                        select_red.fillAmount = usageTimes[0];
                    }
                    if (usageTimes[2] < 1)
                    {
                        usageTimes[2] += heelSpeed / 100 * Time.deltaTime;
                        select_blue.fillAmount = usageTimes[2];
                    }
                }
                else//ゲージがなくなったら合図をだす
                {
                    shiftColor();
                    selectNumber = 3;
                }
                break;
            case 2:
                if (playerController.nowColor != ColorInfo.COLOR_TYPE.Bule)
                {
                    playerController.Form_Color(ColorInfo.COLOR_TYPE.Bule);
                }
                if (usageTimes[2] > 0)
                {
                    usageTimes[2] -= speed /100 * Time.deltaTime;
                    select_blue.fillAmount = usageTimes[2];
                    if (usageTimes[0] < 1)
                    {
                        usageTimes[0] += heelSpeed / 100 * Time.deltaTime;
                        select_red.fillAmount = usageTimes[0];
                    }
                    if (usageTimes[1] < 1)
                    {
                        usageTimes[1] += heelSpeed / 100 * Time.deltaTime;
                        select_green.fillAmount = usageTimes[1];
                    }
                }
                else//ゲージがなくなったら合図をだす
                {
                    shiftColor();
                    selectNumber = 3;
                }
                break;
            default:
                if (playerController.nowColor != ColorInfo.COLOR_TYPE.Blank)
                {
                    playerController.Form_Color(ColorInfo.COLOR_TYPE.Blank);
                }
                if (usageTimes[0] < 1)
                {
                    usageTimes[0] += heelSpeed / 100 * Time.deltaTime;
                    select_red.fillAmount = usageTimes[0];
                }
                if (usageTimes[1] < 1)
                {
                    usageTimes[1] += heelSpeed / 100 * Time.deltaTime;
                    select_green.fillAmount = usageTimes[1];
                }
                if (usageTimes[2] < 1)
                {
                    usageTimes[2] += heelSpeed / 100 * Time.deltaTime;
                    select_blue.fillAmount = usageTimes[2];
                }
                break;
        }
    }

    /// <summary>残っているゲージを探し、切り替える</summary>
    void shiftColor()
    {
        for(int i = 0; i < usageTimes.Length; i++)
        {
            if (usageTimes[i] > 0)
            {
                selectNumber = i;
                break;
            }
        }
    }
}
