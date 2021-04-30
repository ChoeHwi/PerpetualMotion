using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSelection : MonoBehaviour
{
    public int selectNumber = 0;
    [Header("赤色のバー")]
    [SerializeField] Image Select_red;
    [Header("緑のバー")]
    [SerializeField] Image Select_green;
    [Header("青色のバー")]
    [SerializeField] Image Select_blue;
    /// <summary>3本のゲージの値</summary>
    float[] UsageTimes = new float[3] { 1f, 1f, 1f, };
    /// <summary>ゲージの減少速度</summary>
    [Header("色の使用スピード(0に近いと早く減る)")]
    public float speed;
    /// <summary>ゲージの回復速度</summary>
    [Header("色の回復スピード(0に近いと早く回復する)")]
    public float HeelSpeed;
    PlayerController playerController;

    void Start()
    {
        Select_red = GameObject.Find("Red_Image").GetComponent<Image>();
        Select_green = GameObject.Find("Green_Image").GetComponent<Image>();
        Select_blue = GameObject.Find("Blue_Image").GetComponent<Image>();
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
                    if (UsageTimes[i] > 0)
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
                for (int i = selectNumber + 1; i < UsageTimes.Length; i++)
                {
                    if (UsageTimes[i] > 0)
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
                if (UsageTimes[0] > 0)
                {
                    UsageTimes[0] -= 1.0f / speed * Time.deltaTime;
                    Select_red.fillAmount = UsageTimes[0];
                    if (UsageTimes[1] < 1)
                    {
                        UsageTimes[1] += 1.0f / HeelSpeed * Time.deltaTime;
                        Select_green.fillAmount = UsageTimes[1];
                    }
                    if (UsageTimes[2] < 1)
                    {
                        UsageTimes[2] += 1.0f / HeelSpeed * Time.deltaTime;
                        Select_blue.fillAmount = UsageTimes[2];
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
                if (UsageTimes[1] > 0)
                {
                    UsageTimes[1] -= 1.0f / speed * Time.deltaTime;
                    Select_green.fillAmount = UsageTimes[1];
                    if (UsageTimes[0] < 1)
                    {
                        UsageTimes[0] += 1.0f / HeelSpeed * Time.deltaTime;
                        Select_red.fillAmount = UsageTimes[0];
                    }
                    if (UsageTimes[2] < 1)
                    {
                        UsageTimes[2] += 1.0f / HeelSpeed * Time.deltaTime;
                        Select_blue.fillAmount = UsageTimes[2];
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
                if (UsageTimes[2] > 0)
                {
                    UsageTimes[2] -= 1.0f / speed * Time.deltaTime;
                    Select_blue.fillAmount = UsageTimes[2];
                    if (UsageTimes[0] < 1)
                    {
                        UsageTimes[0] += 1.0f / HeelSpeed * Time.deltaTime;
                        Select_red.fillAmount = UsageTimes[0];
                    }
                    if (UsageTimes[1] < 1)
                    {
                        UsageTimes[1] += 1.0f / HeelSpeed * Time.deltaTime;
                        Select_green.fillAmount = UsageTimes[1];
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
                if (UsageTimes[0] < 1)
                {
                    UsageTimes[0] += 1.0f / HeelSpeed * Time.deltaTime;
                    Select_red.fillAmount = UsageTimes[0];
                }
                if (UsageTimes[1] < 1)
                {
                    UsageTimes[1] += 1.0f / HeelSpeed * Time.deltaTime;
                    Select_green.fillAmount = UsageTimes[1];
                }
                if (UsageTimes[2] < 1)
                {
                    UsageTimes[2] += 1.0f / HeelSpeed * Time.deltaTime;
                    Select_blue.fillAmount = UsageTimes[2];
                }
                break;
        }
    }

    /// <summary>残っているゲージを探し、切り替える</summary>
    void shiftColor()
    {
        for(int i = 0; i < UsageTimes.Length; i++)
        {
            if (UsageTimes[i] > 0)
            {
                selectNumber = i;
                break;
            }
        }
    }
}
