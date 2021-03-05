﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName = "ScriptableObject/Create")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;

    public void Use()
    {
        Debug.Log(name + "を使用");
    }
}
