using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MonoBehaviour
{
    /// <summary>Ventの番号</summary>
    [Header("通気口の番号を入れる(0から6までで)")]
    public int ventNumber;
    /// <summary>VentManagerを呼び出し</summary>
    ventManager v_Manger;
    /// <summary>オブジェクトを格納する</summary>
    public GameObject vent_obj;
    void Start()
    {
        v_Manger = FindObjectOfType<ventManager>();
        v_Manger.vent_Pos[ventNumber] = transform;
        v_Manger.vent_OBJ[ventNumber] = vent_obj;
        v_Manger.num += 1;
    }
}
