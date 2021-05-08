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
    [Header("矢印が別であるときはtrueにする")]
    public bool ch = false;
    public GameObject arrow_Obj;

    //セレクトシーン用追加
    [Header("難易度の時は簡単な順に0～3" +
        "階層決めるときは最初の階層から順に0～19")]
    public int selectNum;
    select selection;

    void Start()
    {
        selection = FindObjectOfType<select>();
    }
    //全部共通
    public void EnterButton()
    {
        GetComponent<Image>().sprite = Sprit_UI[0];
        if (ch)
        {
            arrow_Obj.gameObject.SetActive(true);
        }
    }
    public void ExitButton()
    {
        GetComponent<Image>().sprite = Sprit_UI[1];
        if (ch)
        {
            arrow_Obj.gameObject.SetActive(false);
        }
    }
    //セレクトシーンの時
    //難易度決める時
    public void s_DownButton_difficulty()
    {
        selection.difficultyNum = selectNum;
    }
    //階層決める時
    public void s_DownButton_EachStage()
    {
        selection.StageNum = selectNum;
        selection.ch = true;
    }

    //セレクトシーン以外の時
    public void DownButton_main()
    {
        SceneManager.LoadScene(LoadSceneName[0]);
    }
    public void DownButton_sub()
    {
        SceneManager.LoadScene(LoadSceneName[1]);
    }
}
