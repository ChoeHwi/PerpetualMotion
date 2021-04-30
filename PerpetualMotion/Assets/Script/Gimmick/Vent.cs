using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MonoBehaviour
{
    [Header("通気口の番号を入れる(0から6までで)")]
    public int ventNumber;
    ventManager v_Manger;
    public GameObject vent_obj;
    void Start()
    {
        v_Manger = FindObjectOfType<ventManager>();
        v_Manger.vent_Pos[ventNumber] = transform;
        v_Manger.vent_OBJ[ventNumber] = vent_obj;
        v_Manger.num += 1;
    }
}
