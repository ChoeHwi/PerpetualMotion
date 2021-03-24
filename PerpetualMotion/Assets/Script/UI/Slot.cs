using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image icon;
    public GameObject removeButton;
    public Item item;

    public void AddItem(Item newitem)
    {
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
