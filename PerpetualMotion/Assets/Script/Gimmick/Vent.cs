using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MonoBehaviour
{
    [Header("通気口の番号")]
    [SerializeField] int ventNumber;
    ventManager v_Manger;
    PlayerController player_con;
    public GameObject vent_obj;
    public Transform vent_obj_tr;
    public bool ch;
    void Start()
    {
        v_Manger = FindObjectOfType<ventManager>();
        player_con = FindObjectOfType<PlayerController>();
        switch (ventNumber)
        {
            case 0:
                v_Manger.ventPos_1 = transform;
                break;
            case 1:
                v_Manger.ventPos_2 = transform;
                break;
            case 2:
                v_Manger.ventPos_3 = transform;
                break;
            case 3:
                v_Manger.ventPos_4 = transform;
                break;
            case 4:
                v_Manger.ventPos_5 = transform;
                break;
            case 5:
                v_Manger.ventPos_6 = transform;
                break;
            default:
                break;
        }
        v_Manger.Vent_trList.Add(vent_obj_tr);
        v_Manger.num += 1;
        v_Manger.Vent_trList.Sort((a, b) => string.Compare(a.name, b.name));
    }
}
