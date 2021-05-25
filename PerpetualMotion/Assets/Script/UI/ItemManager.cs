using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>アイテムをテキストで表示させるスクリプト</summary>
public class ItemManager : MonoBehaviour
{
    new public string name = "New Item";
    public Sprite icon = null;
    /// <summary>メビウスの持っている数を保管している</summary>
    public int numberOfItem;
    /// <summary>textを入れる場所</summary>
    public Text itemText;
    /// <summary>PlayerControllerを呼び出し</summary>
    PlayerController playcon;
    void Start()
    {
        playcon = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    void Update()
    {
        numberOfItem = playcon.itemCount;
        itemText.text = "×" + numberOfItem;
    }
    public void getItem()
    {
        numberOfItem += 1;
        itemText.text = "×" + numberOfItem;

    }
    public void clearItem()
    {
        if (numberOfItem > 0)
        {
            numberOfItem -= 1;
            itemText.text = "×" + numberOfItem;
        }
    }
}
