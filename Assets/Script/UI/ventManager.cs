using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>Ventを管理するスクリプト</summary>
public class ventManager : MonoBehaviour
{
    /// <summary>Ventの最大数</summary>
    public int GenerateNum;
    [SerializeField] GameObject ventOBJ;
    /// <summary>生成しているVentの数</summary>
    public int num;
    /// <summary>Ventのオブジェクトを格納</summary>
    public GameObject[] vent_OBJ = new GameObject[6];
    /// <summary>Ventのポジションを格納</summary>
    public Transform[] vent_Pos = new Transform[6];
}
