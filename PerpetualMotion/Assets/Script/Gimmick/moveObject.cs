using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveObject : ColorInfo
{
    [Header("このオブジェクトの色")]
    public COLOR_TYPE nowColor = COLOR_TYPE.Blank;
    SpriteRenderer objImage;
    PlayerController pc;
    public GameObject RootObject;
    public bool active;
    public float speed;
    private void Start()
    {
        active = false;
        pc = GameObject.FindObjectOfType<PlayerController>();
        //speed = pc.ObjSpeed;
        objImage = GetComponent<SpriteRenderer>();
        Form_Color(nowColor);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (active)
            {
                transform.Translate(-speed, 0, 0);
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (active)
            {
                transform.Translate(speed, 0, 0);
            }
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (active)
            {
                transform.Translate(0, speed, 0);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (active)
            {
                transform.Translate(0, -speed, 0);
            }
        }
    }
    public void objSet()
    {
        this.gameObject.transform.parent = RootObject.gameObject.transform;
    }
    public void objOut()
    {
        this.gameObject.transform.parent = null;
    }

    public void Form_Color(COLOR_TYPE color)
    {
        nowColor = color;
        //objImage.sprite = SelectColor(nowColor)[imageIndex];
        switch (color)
        {
            case COLOR_TYPE.Blank:
                objImage.color = new Color(0, 0, 0);
                break;
            case COLOR_TYPE.Red:
                objImage.color = new Color(255, 0, 0);
                break;
            case COLOR_TYPE.Bule:
                objImage.color = new Color(0, 0, 255);
                break;
            case COLOR_TYPE.Green:
                objImage.color = new Color(0, 255, 0);
                break;
        }

    }
}
