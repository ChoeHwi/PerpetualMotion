using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hourglass_time : MonoBehaviour
{
    public Image cooldown;
    /// <summary>どっちからいちるかの判定</summary>
    [Header("オンが上から下/オフが下から上")]
    public bool hourglassUpDwon;
    /// <summary>制限時間</summary>
    [Header("0に近いと早く減る")]
    public float waitTime = 30.0f;
    /// <summary>PlayerControllerを呼び出す</summary>
    PlayerController pc;
    void Start()
    {
        pc = FindObjectOfType<PlayerController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (hourglassUpDwon == true)
        {
            cooldown.fillAmount -= 1.0f / waitTime * Time.deltaTime;
        }
        if (hourglassUpDwon == false)
        {
            cooldown.fillAmount += 1.0f / waitTime * Time.deltaTime;
            if (hourglassUpDwon == false && cooldown.fillAmount == 1)
            {
                pc.GameOverCH();
            }
        }
    }
}
