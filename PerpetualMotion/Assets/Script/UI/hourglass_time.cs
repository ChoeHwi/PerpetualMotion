using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hourglass_time : MonoBehaviour
{
    public Image coolDown;
    /// <summary>どっちからおちるかの判定</summary>
    [Header("オンが上から下/オフが下から上")]
    public bool hourglassUpDwon = false;
    /// <summary>制限時間</summary>
    [Header("0に近いと早く減る")]
    public float waitTime = 30.0f;
    /// <summary>PlayerControllerを呼び出す</summary>
    PlayerController pc;
    /// <summary> 砂時計を止めるためのフラグ </summary>
    [Header("砂時計を止めるためのフラグ")]
    public bool m_isClear = false;
    void Start()
    {
        pc = FindObjectOfType<PlayerController>();
    }
    
    void Update()
    {
        if (m_isClear)
        {
            return;
        }
        if (hourglassUpDwon == true)
        {
            coolDown.fillAmount -= 1.0f / waitTime * Time.deltaTime;
        }
        if (hourglassUpDwon == false)
        {
            coolDown.fillAmount += 1.0f / waitTime * Time.deltaTime;
            if (hourglassUpDwon == false && coolDown.fillAmount == 1)
            {
                pc.GameOverCH();
            }
        }
    }
}
