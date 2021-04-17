using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour
{
    PlayerController con;
    [SerializeField] GameObject LifeObj;
    [SerializeField] Text LifeText;
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
                LifeText.text = "×" + life;
                break;
            case 3:
                LifeText.text = "×" + life;
                break;
            case 2:
                LifeText.text = "×" + life;
                break;
            case 1:
                LifeText.text = "×" + life;
                break;
            case 0:
                LifeText.text = "×" + life;
                break;
            default:
                break;
        }
    }
}
