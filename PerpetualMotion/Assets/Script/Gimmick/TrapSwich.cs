using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSwich : MonoBehaviour
{
    [Header("対応する電流トラップ")]
    [SerializeField] ElectricTrap[] electricTrap;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TrapActuation()
    {
        foreach(ElectricTrap script in electricTrap)
        {
            script.Actuation();
        }
    }
}
