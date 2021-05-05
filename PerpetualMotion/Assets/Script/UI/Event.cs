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


    void Start()
    {

    }
    public void EnterButton()
    {
        GetComponent<Image>().sprite = Sprit_UI[0];
    }
    public void ExitButton()
    {
        GetComponent<Image>().sprite = Sprit_UI[1];
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
