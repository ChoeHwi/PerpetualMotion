using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> トラップを作動させるクラス </summary>
public class TrapSwich : MonoBehaviour
{
    [Header("対応する電流トラップ")]
    [SerializeField] ElectricTrap[] electricTrap;

    public void TrapActuation()
    {
        foreach(ElectricTrap script in electricTrap)
        {
            script.Actuation();
        }
    }
}
