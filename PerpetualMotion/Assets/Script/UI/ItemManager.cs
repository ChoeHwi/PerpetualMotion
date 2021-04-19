using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    new public string name = "New Item";
    public Sprite icon = null;

    public int numberOfItem;
    public Text itemText;
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
