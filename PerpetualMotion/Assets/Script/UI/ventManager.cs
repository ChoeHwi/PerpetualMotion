using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ventManager : MonoBehaviour
{
    public int GenerateNum;
    [SerializeField] GameObject ventOBJ;
    public int num;
    public GameObject[] vent_OBJ = new GameObject[6];
    public Transform[] vent_Pos = new Transform[6];
}
