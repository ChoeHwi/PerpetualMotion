using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour
{
    PlayerController con;
    [SerializeField] GameObject LifeObj_1;
    [SerializeField] GameObject LifeObj_2;
    [SerializeField] GameObject LifeObj_3;
    [SerializeField] GameObject LifeObj_4;
    [SerializeField] GameObject LifeObj_5;
    public int life;
    // Start is called before the first frame update
    void Start()
    {
        con = GameObject.Find("Player").GetComponent<PlayerController>();
        life= con.playerHp;
    }

    // Update is called once per frame
    void Update()
    {
        life = con.playerHp;
        CHANG_COLOR();
    }
    public void CHANG_COLOR()
    {
        switch (life)
        {
            case 4:
                LifeObj_5.GetComponent<Image>().color = Color.white;
                break;
            case 3:
                LifeObj_4.GetComponent<Image>().color = Color.white;
                break;
            case 2:
                LifeObj_3.GetComponent<Image>().color = Color.white;
                break;
            case 1:
                LifeObj_2.GetComponent<Image>().color = Color.white;
                break;
            case 0:
                LifeObj_1.GetComponent<Image>().color = Color.white;
                break;
            default:
                break;
        }
    }
}
