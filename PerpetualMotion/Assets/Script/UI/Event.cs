using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Event : MonoBehaviour
{
    public Sprite[] Sprit_UI;
    [Header("呼び込みたいシーンを追加する")]
    public string[] LoadSceneName;
    public GameObject i;
    public bool ch = false;


    void Start()
    {

    }
    public void EnterButton()
    {
        GetComponent<Image>().sprite = Sprit_UI[0];
        if (ch)
        {
            i.gameObject.SetActive(true);
        }
    }
    public void ExitButton()
    {
        GetComponent<Image>().sprite = Sprit_UI[1];
        if (ch)
        {
            i.gameObject.SetActive(false);
        }
    }
    public void DownButton_main()
    {
        SceneManager.LoadScene(LoadSceneName[0]);
    }
    public void DownButton_sub()
    {
        SceneManager.LoadScene(LoadSceneName[1]);
    }
}
