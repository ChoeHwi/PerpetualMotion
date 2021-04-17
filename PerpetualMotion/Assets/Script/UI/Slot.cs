using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image icon;
    public GameObject removeButton;
    public Item item;

    //仮テスト
    public int ItemNum;
    public Text ItemNumText;
    public void TestAddItem()
    {
        ItemNum += 1;
        ItemNumText.text = "×" + ItemNum;
    }
    public void TestclearSlot()
    {
        ItemNum -= 1;
        if (ItemNum >= 0)
        {
            ItemNumText.text = "×" + ItemNum;
        }
    }


    public void AddItem(Item newitem)
    {
        Debug.Log("atariamotu");
        item = newitem;
        icon.sprite = newitem.icon;
        if (removeButton)
        {
            removeButton.SetActive(true);
        }
    }
    public void clearSlot()
    {
        item = null;
        icon.sprite = null;
        if (removeButton)
        {
            removeButton.SetActive(false);
        }
    }
    public void onRemovedButton()
    {
        Inventry.instance.Remove(item);
    }

    public void UseItem()
    {
        if (item == null)
        {
            return;
        }
        item.Use();
    }
}
