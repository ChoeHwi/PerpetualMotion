using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSelection : MonoBehaviour
{
    public int selectNumber;
    [SerializeField] Slider Select_red;
    [SerializeField] Slider Select_green;
    [SerializeField] Slider Select_blue;
    float UsageTime_red = 100;
    float UsageTime_green = 100;
    float UsageTime_blue = 100;
    // Start is called before the first frame update
    void Start()
    {
        Select_red = GameObject.Find("Red").GetComponent<Slider>();
        Select_green = GameObject.Find("Green").GetComponent<Slider>();
        Select_blue = GameObject.Find("Blue").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (selectNumber > 0)
            {
                selectNumber--;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (selectNumber < 2)
            {
                selectNumber++;
            }
        }
        selectColor();
    }
    void selectColor()
    {
        switch (selectNumber)
        {
            case 0:
                UsageTime_red -= Time.deltaTime;
                Select_red.value = UsageTime_red;
                break;
            case 1:
                UsageTime_green -= Time.deltaTime;
                Select_green.value = UsageTime_green;
                break;
            case 2:
                UsageTime_blue -= Time.deltaTime;
                Select_blue.value = UsageTime_blue;
                break;
            default:
                break;
        }
    }
}
