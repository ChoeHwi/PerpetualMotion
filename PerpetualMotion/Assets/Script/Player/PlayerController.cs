﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤーの制御
/// </summary>
public class PlayerController : ColorInfo
{
    [Header("プレイヤーの移動速度")]
    public float m_moveSpeed = 4.0f;
    public Rigidbody2D rb;
    float m_stopSpeed;
    /// <summary>無敵時間</summary>
    float invincibleTime = 3f;
    bool isDamaged = false;
    public SpriteRenderer playerImage;
    int imageIndex = 1;
    public int playerHp = 5;
    public COLOR_TYPE nowColor = COLOR_TYPE.Blank;
    /// <summary>ステルス状態かどうか</summary>
    public bool stealth = false;
    /// <summary>プレイヤーが操作可能か</summary>
    bool active = true;
    GameManager gameManager;
    CapsuleCollider2D capsuleCollider;
    [SerializeField] GameObject dialog;
    bool triggerStay;
    Transform stealthPosition;
    /// <summary>現在入ることのできるステルスオブジェクト</summary>
    public StealthObject stealthObject;
    //moveObj関連
    bool MoveObj;
    [Header("オブジェクトと融合したときのスピード")]
    public float ObjSpeed;
    public bool Movement = false;
    public moveObject moveObject;
    [Header("融合して移動するオブジェクトを入れる")]
    public Transform MoveObj_Red;
    public Transform MoveObj_Blue;
    public Transform MoveObj_Green;
    //アイテム関連
    public int itemCount;
    [SerializeField] GameObject[] itemType = new GameObject[1];
    //通気口関連
    Vent vent_S;
    ventManager vent_Mana;
    bool ventBool;
    public int VentNum;
    bool Vent_ch = false;
    bool tracking = false;
    [SerializeField] EnemyController enemyCon;
    public Canvas BeingTrackedOBJ;


    void Start()
    {
        enemyCon = FindObjectOfType<EnemyController>();
        vent_Mana = GameObject.FindObjectOfType<ventManager>();
        vent_S = GameObject.FindObjectOfType<Vent>();
        m_stopSpeed = m_moveSpeed;
        playerImage = GetComponent<SpriteRenderer>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        moveObject = GameObject.FindObjectOfType<moveObject>();
    }

    // Update is called once per frame

    void Update()
    {
        tracking = enemyCon.tracking;
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (active)
        {
            
            Vector2 dir = new Vector2(h, v).normalized;
            rb.velocity = dir * m_moveSpeed;
        }

        if (h > 0)
        {
            if (v > 0)
            {

            }
            else if (v < 0)
            {

            }
            else
            {
                imageIndex = 3;
                playerImage.sprite = SelectColor(nowColor)[imageIndex];
            }
        }
        else if (h < 0)
        {
            if (v > 0)
            {

            }
            else if (v < 0)
            {

            }
            else
            {
                imageIndex = 2;
                playerImage.sprite = SelectColor(nowColor)[imageIndex];
            }
        }
        else
        {
            if (v > 0)
            {
                imageIndex = 0;
                playerImage.sprite = SelectColor(nowColor)[imageIndex];
            }
            else if (v < 0)
            {
                imageIndex = 1;
                playerImage.sprite = SelectColor(nowColor)[imageIndex];
            }
        }

        if (playerHp <= 0 && active)
        {
            active = false;
            gameManager.OpenResult(false);
        }

        if (triggerStay)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (stealth)
                {
                    capsuleCollider.isTrigger = false;
                    stealth = false;
                    active = true;
                }
                else
                {
                    if (stealthObject.nowColor == nowColor)
                    {
                        stealth = true;
                        active = false;
                        capsuleCollider.isTrigger = true;
                        this.transform.position = new Vector3(stealthPosition.transform.position.x, stealthPosition.transform.position.y, this.transform.position.z);
                    }
                }
            }
        }
        if (MoveObj)//オブジェクト移動できるときの処理
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (Movement)
                {
                    transform.parent = null;
                    active = true;
                    capsuleCollider.isTrigger = false;
                    Movement = false;
                    moveObject.active = false;
                }
                else
                {
                    if (moveObject.nowColor == nowColor)
                    {
                        switch (nowColor)
                        {
                            
                            case COLOR_TYPE.Blank:
                                break;
                            case COLOR_TYPE.Red:
                                if (moveObject.nowColor == COLOR_TYPE.Red)
                                {
                                    transform.parent = MoveObj_Red;
                                    moveObject.active = true;
                                    Movement = true;
                                    active = false;
                                    capsuleCollider.isTrigger = true;
                                    this.transform.position = new Vector3(MoveObj_Red.transform.position.x, MoveObj_Red.transform.position.y, this.transform.position.z);
                                }
                                break;
                            case COLOR_TYPE.Bule:
                                if (moveObject.nowColor == COLOR_TYPE.Bule)
                                {
                                    transform.parent = MoveObj_Blue;
                                    moveObject.active = true;
                                    Movement = true;
                                    active = false;
                                    capsuleCollider.isTrigger = true;
                                    this.transform.position = new Vector3(MoveObj_Blue.transform.position.x, MoveObj_Blue.transform.position.y, this.transform.position.z);
                                }
                                break;
                            case COLOR_TYPE.Green:
                                if (moveObject.nowColor==COLOR_TYPE.Green)
                                {
                                    transform.parent = MoveObj_Green;
                                    moveObject.active = true;
                                    Movement = true;
                                    active = false;
                                    capsuleCollider.isTrigger = true;
                                    this.transform.position = new Vector3(MoveObj_Green.transform.position.x, MoveObj_Green.transform.position.y, this.transform.position.z);
                                }
                                break;
                            default:
                                break;
                        }
                        ;
                    }
                }
            }
        }
        if (ventBool && !tracking)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!Vent_ch)
                {
                    Vent_ch = true;
                    active = false;
                }
                else if (Vent_ch && vent_S)
                {
                    Vent_ch = false;
                    active = true;
                    transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                }
            }
            if (Vent_ch)
            {
                if (VentNum < 0)
                {
                    VentNum = vent_Mana.num;
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    if (VentNum >= 0)
                    {
                        VentNum -= 1;
                    }
                    if (VentNum == -1)
                    {
                        VentNum = vent_Mana.num - 1;
                    }
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    if (VentNum < vent_Mana.num)
                    {
                        VentNum += 1;
                    }
                    if (VentNum == vent_Mana.num)
                    {
                        VentNum = 0;
                    }
                }
                Vent_Pos();
            }
        }
    }

    void FixedUpdate()
    {
        //ダメージを受けた時の処理
        if (isDamaged)
        {
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Enemy" && !isDamaged)
        {
            playerHp -= 1;
            isDamaged = true;
            Invoke("flashEnd", invincibleTime);
            if (itemCount > 0)
            {
                itemCount -= 1;
                gameManager.LostItem(this.transform.position, itemType[0]);
            }
        }
        if (collision.gameObject.tag == "Item")
        {
            itemCount += 1;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Mobius")
        {
            if (itemCount > 0)
            {
                gameManager.FitStart(itemCount);
                itemCount = 0;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "stealth")
        {
            stealthObject = collision.GetComponent<StealthObject>();
            if (stealthObject.nowColor == nowColor)
            {
                dialog.SetActive(true);
                triggerStay = true;
                stealthPosition = collision.transform;
            }
        }
        if (collision.gameObject.tag == "Switch")
        {
            collision.gameObject.GetComponent<TrapSwich>().TrapActuation();
        }
        if (collision.gameObject.tag == "mov")
        {
            moveObject = collision.GetComponent<moveObject>();
            if (moveObject.nowColor == nowColor)
            {
                dialog.SetActive(true);
                MoveObj = true;
                stealthPosition = collision.transform;
            }
        }
        if (collision.gameObject.tag == "vent")
        {
            vent_S = collision.GetComponent<Vent>();
            VentNum = vent_S.ventNumber;
            ventBool = true;
            if (!tracking)
            {
                dialog.SetActive(true);
            }
            else
            {
                BeingTrackedOBJ.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "stealth")
        {
            dialog.SetActive(false);
            triggerStay = false;
            stealthObject = null;
        }
        if (collision.gameObject.tag == "mov")
        {
            dialog.SetActive(false);
            MoveObj = false;
            moveObject = null;
            transform.parent = null;
        }
        if (collision.gameObject.tag == "vent")
        {
            ventBool = false;
            dialog.SetActive(false);
            BeingTrackedOBJ.gameObject.SetActive(false);

        }
    }
    public void Vent_Pos()
    {
        transform.position = new Vector3(vent_Mana.vent_Pos[VentNum].position.x, vent_Mana.vent_Pos[VentNum].position.y, 1);
    }

    public void Form_Color(COLOR_TYPE color)
    {
        nowColor = color;
        playerImage.sprite = SelectColor(nowColor)[imageIndex];
        if (stealth)
        {
            if (stealthObject.nowColor != nowColor)
            {
                capsuleCollider.isTrigger = false;
                stealth = false;
                active = true;
                dialog.SetActive(false);
            }
        }
        else if (triggerStay)
        {
            if (stealthObject.nowColor != nowColor)
            {
                dialog.SetActive(false);
            }
            else if (stealthObject.nowColor == nowColor)
            {
                dialog.SetActive(true);
            }
        }
        if (Movement)
        {
            if (moveObject.nowColor != nowColor)
            {
                capsuleCollider.isTrigger = false;
                Movement = false;
                active = true;
                transform.parent = null;
                moveObject.active = false;
                dialog.SetActive(false);
            }
        }
        else if (MoveObj)
        {
            if (moveObject.nowColor != nowColor)
            {
                dialog.SetActive(false);
            }
            else if (moveObject.nowColor == nowColor)
            {
                dialog.SetActive(true);
            }
        }
    }

    void flashEnd()
    {
        isDamaged = false;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1);
    }

    public void mod_craft()
    {
        m_moveSpeed = 0;
    }
    public void mod_mov()
    {
        m_moveSpeed = m_stopSpeed;
    }
}
