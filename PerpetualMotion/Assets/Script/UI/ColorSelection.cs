using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>色を変えるスクリプト</summary>
public class ColorSelection : MonoBehaviour
{
    /// <summary></summary>
    public int selectNumber = 0;
    [System.NonSerialized]
    [Header("赤色のバー")]
    [SerializeField] Image select_red;
    [System.NonSerialized]
    [Header("緑のバー")]
    [SerializeField] Image select_green;
    [System.NonSerialized]
    [Header("青色のバー")]
    [SerializeField] Image select_blue;
    /// <summary>3本のゲージの値</summary>
    float[] m_usageTimes = new float[3] { 1f, 1f, 1f, };
    /// <summary>ゲージの減少速度</summary>
    [Header("色の使用スピード(0に近いと遅く減る)")]
    public float m_speed;
    /// <summary>ゲージの回復速度</summary>
    [Header("色の回復スピード(0に近いと遅く回復する)")]
    public float m_heelSpeed;
    PlayerController playerController;
    /// <summary>プレイやの色変えのUIオブジェクト</summary>
    public ChengColor m_UICheng;

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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            m_UICheng.gameObject.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            m_UICheng.ColorDown();
            m_UICheng.gameObject.SetActive(false);
        }
        SelectColor();
    }

    void SelectColor()
    {
        switch (selectNumber)
        {
            case 0:
                if (playerController.nowColor != ColorInfo.COLOR_TYPE.Blank)
                {
                    playerController.Form_Color(ColorInfo.COLOR_TYPE.Blank);
                }
                if (m_usageTimes[0] < 1)
                {
                    m_usageTimes[0] += m_heelSpeed / 100 * Time.deltaTime;
                    select_red.fillAmount = m_usageTimes[0];
                }
                if (m_usageTimes[1] < 1)
                {
                    m_usageTimes[1] += m_heelSpeed / 100 * Time.deltaTime;
                    select_green.fillAmount = m_usageTimes[1];
                }
                if (m_usageTimes[2] < 1)
                {
                    m_usageTimes[2] += m_heelSpeed / 100 * Time.deltaTime;
                    select_blue.fillAmount = m_usageTimes[2];
                }
                break;
            case 1:
                if (playerController.nowColor != ColorInfo.COLOR_TYPE.Red)
                {
                    playerController.Form_Color(ColorInfo.COLOR_TYPE.Red);
                }
                if (m_usageTimes[0] > 0)
                {
                    m_usageTimes[0] -= m_speed / 100 * Time.deltaTime;
                    select_red.fillAmount = m_usageTimes[0];
                    if (m_usageTimes[1] < 1)
                    {
                        m_usageTimes[1] += m_heelSpeed / 100 * Time.deltaTime;
                        select_green.fillAmount = m_usageTimes[1];
                    }
                    if (m_usageTimes[2] < 1)
                    {
                        m_usageTimes[2] += m_heelSpeed / 100 * Time.deltaTime;
                        select_blue.fillAmount = m_usageTimes[2];
                    }
                }
                else//ゲージがなくなったら合図をだす
                {
                    selectNumber = 0;
                }
                break;
            case 2:
                if (playerController.nowColor != ColorInfo.COLOR_TYPE.Green)
                {
                    playerController.Form_Color(ColorInfo.COLOR_TYPE.Green);
                }
                if (m_usageTimes[1] > 0)
                {
                    m_usageTimes[1] -= m_speed / 100 * Time.deltaTime;
                    select_green.fillAmount = m_usageTimes[1];
                    if (m_usageTimes[0] < 1)
                    {
                        m_usageTimes[0] += m_heelSpeed / 100 * Time.deltaTime;
                        select_red.fillAmount = m_usageTimes[0];
                    }
                    if (m_usageTimes[2] < 1)
                    {
                        m_usageTimes[2] += m_heelSpeed / 100 * Time.deltaTime;
                        select_blue.fillAmount = m_usageTimes[2];
                    }
                }
                else//ゲージがなくなったら合図をだす
                {
                    selectNumber = 0;
                }
                break;
            case 3:
                if (playerController.nowColor != ColorInfo.COLOR_TYPE.Bule)
                {
                    playerController.Form_Color(ColorInfo.COLOR_TYPE.Bule);
                }
                if (m_usageTimes[2] > 0)
                {
                    m_usageTimes[2] -= m_speed /100 * Time.deltaTime;
                    select_blue.fillAmount = m_usageTimes[2];
                    if (m_usageTimes[0] < 1)
                    {
                        m_usageTimes[0] += m_heelSpeed / 100 * Time.deltaTime;
                        select_red.fillAmount = m_usageTimes[0];
                    }
                    if (m_usageTimes[1] < 1)
                    {
                        m_usageTimes[1] += m_heelSpeed / 100 * Time.deltaTime;
                        select_green.fillAmount = m_usageTimes[1];
                    }
                }
                else//ゲージがなくなったら合図をだす
                {
                    selectNumber = 0;
                }
                break;
            default:
                if (playerController.nowColor != ColorInfo.COLOR_TYPE.Blank)
                {
                    playerController.Form_Color(ColorInfo.COLOR_TYPE.Blank);
                }
                if (m_usageTimes[0] < 1)
                {
                    m_usageTimes[0] += m_heelSpeed / 100 * Time.deltaTime;
                    select_red.fillAmount = m_usageTimes[0];
                }
                if (m_usageTimes[1] < 1)
                {
                    m_usageTimes[1] += m_heelSpeed / 100 * Time.deltaTime;
                    select_green.fillAmount = m_usageTimes[1];
                }
                if (m_usageTimes[2] < 1)
                {
                    m_usageTimes[2] += m_heelSpeed / 100 * Time.deltaTime;
                    select_blue.fillAmount = m_usageTimes[2];
                }
                break;
        }
    }

    /// <summary>外部から色を変える</summary>
    /// <param name="colorNumber"></param>
    public void ColorChange(int colorNumber)
    {
        selectNumber = colorNumber;
    }
}
