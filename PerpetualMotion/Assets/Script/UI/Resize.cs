using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Resize : MonoBehaviour
{
    RectTransform rtf;
    public int num;
    public bool shieldZone;
    public bool anchorRight;
    // Start is called before the first frame update
    void Start()
    {
        rtf = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {


        if (shieldZone)
        {
            rtf.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (float)Screen.width - (float)Screen.height / 8f * 0.7f * 4);
        }
        else
        {
            rtf.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (float)Screen.height / 8f * 0.7f);
        }
        if (anchorRight)
        {
            if (shieldZone)
            {
                transform.position = new Vector3((float)Screen.width, transform.position.y, 0);
            }
            else
            {
                transform.position = new Vector3((float)Screen.width - ((float)Screen.height / 8f * 0.7f * num + (float)Screen.height / 8f * 0.7f * 0.5f), transform.position.y, 0f);
            }
        }
        else
        {
            if (shieldZone)
            {
                transform.position = new Vector3(0, transform.position.y, 0);
            }
            else
            {
                transform.position = new Vector3((float)Screen.height / 8f * 0.7f * num + (float)Screen.height / 8f * 0.7f * 0.5f, transform.position.y, 0f);
            }
        }
    }
}