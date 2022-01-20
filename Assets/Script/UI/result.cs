using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class result : MonoBehaviour
{
    [Header("クリアタイム")]
    [SerializeField] GameObject timeObj;
    public int timeNumber;
    [Header("無力化した敵の数")]
    [SerializeField] GameObject NeutralizeObj;
    public int NeutralizeNumber;
    [Header("使用したギミックの数")]
    [SerializeField] GameObject GimmickObj;
    public int GimmickNumber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
