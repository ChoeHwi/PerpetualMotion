using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hourglass_time : MonoBehaviour
{
    public Image cooldown;
    [Header("オンが上から下/オフが下から上")]
    public bool hourglassUpDwon;
    [Header("0に近いと早く減る")]
    public float waitTime = 30.0f;
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
