using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ワープオブジェクトを管理するクラス </summary>
public class Vent : MonoBehaviour
{
    /// <summary>Ventの番号</summary>
    [Header("通気口の番号を入れる(0から6までで)")]
    public int ventNumber;
    /// <summary>VentManagerを呼び出し</summary>
    ventManager v_Manger;
    /// <summary>オブジェクトを格納する</summary>
    public GameObject vent_obj;
    /// <summary> プレイヤーが乗ったときアニメーション </summary>
    Animator[] vent_anim;
    void Start()
    {
        vent_anim = GetComponentsInChildren<Animator>();
        v_Manger = FindObjectOfType<ventManager>();
        v_Manger.vent_Pos[ventNumber] = transform;
        v_Manger.vent_OBJ[ventNumber] = vent_obj;
        v_Manger.num += 1;
    }

    /// <summary>
    /// プレイヤーがワープに乗ったとき
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            vent_anim[0].SetBool("Warp", true);
            vent_anim[1].SetBool("Warp", true);
        }
        else
        {
            vent_anim[0].SetBool("Warp", false);
            vent_anim[1].SetBool("Warp", false);
        }
    }

    /// <summary>
    /// プレイヤーがワープから離れた時
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            vent_anim[0].SetBool("Warp", false);
            vent_anim[1].SetBool("Warp", false);
        }
    }
}
