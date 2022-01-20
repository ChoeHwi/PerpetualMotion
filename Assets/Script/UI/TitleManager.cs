using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour//SingletonMonoBehaviour<TitleManager>
{
    [SerializeField] Text clickText;
    [SerializeField] GameObject TitlePanel;
    //static bool isTitleClicked;
    private void Start()
    {
        if (false)//isTitleClicked)
        {
            //TitlePanel.SetActive(false);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            TitleClick();
        }
    }

    void FixedUpdate()
    {
        float level = Mathf.Abs(Mathf.Sin(Time.time * 1.5f));
        clickText.color = new Color(clickText.color.r, clickText.color.g, clickText.color.b, level);
    }

    public void TitleClick()
    {
        TitlePanel.SetActive(false);
        //isTitleClicked = true;
    }
}
