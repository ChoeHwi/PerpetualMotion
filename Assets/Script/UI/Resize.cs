using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Resize : MonoBehaviour
{
    public enum UI_TYPE
    {
        Life,
        Gauge,
        Time,
        Item
    }
    public UI_TYPE ui_Type;
    float screenHeight;
    float screenWidth;
    RectTransform rtf;

    // Start is called before the first frame update
    void Start()
    {
        rtf = GetComponent<RectTransform>();
        screenHeight = (float)Screen.height;
        screenWidth = (float)Screen.width;
        switch (ui_Type)
        {
            case UI_TYPE.Life:
                rtf.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (float)Screen.height * 0.2f);
                rtf.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)Screen.height * 0.2f);
                break;
            case UI_TYPE.Gauge:
                rtf.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)Screen.height * 0.16f / 3f);
                rtf.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (float)Screen.height * 16f / 9f / 5f);
                break;
            case UI_TYPE.Time:
                rtf.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (float)Screen.height * 0.16f);
                rtf.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)Screen.height * 16f / 9f * 0.1f);
                break;
            case UI_TYPE.Item:
                rtf.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (float)Screen.height * 0.41f);
                rtf.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)Screen.height * 16f / 9f / 5f);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (screenWidth != (float)Screen.height || screenHeight != (float)Screen.width)
        {
            switch (ui_Type)
            {
                case UI_TYPE.Life:
                    rtf.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (float)Screen.height * 0.2f);
                    rtf.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)Screen.height * 0.2f);
                    break;
                case UI_TYPE.Gauge:
                    rtf.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)Screen.height * 0.16f / 3f);
                    rtf.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (float)Screen.height * 16f / 9f / 5f);
                    break;
                case UI_TYPE.Time:
                    rtf.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (float)Screen.height * 0.16f);
                    rtf.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)Screen.height * 16f / 9f * 0.08f);
                    break;
                case UI_TYPE.Item:
                    rtf.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (float)Screen.height * 0.41f);
                    rtf.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)Screen.height * 16f / 9f / 5f);
                    break;
            }
            screenHeight = (float)Screen.height;
            screenWidth = (float)Screen.width;
        }
    }
}