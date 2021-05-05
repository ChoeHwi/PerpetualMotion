using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Event : MonoBehaviour
{
    GameManager gm;
    public Sprite[] Sprit_UI;
    public string LoadScene_name;


    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }
    public void EnterButton()
    {
        GetComponent<Image>().sprite = Sprit_UI[0];
    }
    public void ExitButton()
    {
        GetComponent<Image>().sprite = Sprit_UI[1];
    }
    public void DownButton()
    {
        gm.LoadScene(LoadScene_name);
    }
}
