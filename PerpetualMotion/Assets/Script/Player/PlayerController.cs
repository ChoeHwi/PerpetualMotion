using System.Collections;
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
    /// <summary>無敵時間</summary>
    [SerializeField] float invincibleTime = 3f;
    /// <summary>ダメージアニメーションの表示中</summary>
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
    /// <summary>ステルス可能な状態</summary>
    bool enterStayStealthObj;
    /// <summary>現在入ることのできるステルスオブジェクト</summary>
    public StealthObject stealthObject;
    [Header("オブジェクトと融合したときのスピード")]
    [SerializeField] float ObjSpeed;
    /// <summary>ステルスオブジェクトに追加したRigidbody2D</summary>
    Rigidbody2D addedRigidbody;
    //アイテム関連
    public int itemCount;
    [SerializeField] GameObject[] itemType = new GameObject[1];
    //通気口関連
    Vent vent_S;
    ventManager vent_Mana;
    bool ventBool;
    public int VentNum;
    bool Vent_ch = false;
    /// <summary>現在追いかけられてる敵</summary>
    public List<EnemyController> enemyCon;
    public Canvas BeingTrackedOBJ;

    void Start()
    {
        vent_Mana = GameObject.FindObjectOfType<ventManager>();
        vent_S = GameObject.FindObjectOfType<Vent>();
        playerImage = GetComponent<SpriteRenderer>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (ventBool && enemyCon.Count == 0)//ベントオブジェクトに触れており、追いかけられてないなら
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
        else if (enterStayStealthObj)//ステルスオブジェクトに触れているなら
        {
            if (Input.GetKeyDown(KeyCode.Space))//スペースを押したとき
            {
                if (stealth)//ステルス状態なら
                {
                    capsuleCollider.isTrigger = false;
                    stealth = false;
                    active = true;
                    playerImage.enabled = true;
                    stealthObject.EyeController_Fa();
                    if (stealthObject.canMove)
                    {
                        Destroy(addedRigidbody);
                    }
                }
                else
                {
                    if (stealthObject.nowColor == nowColor)//ステルスできる状態なら
                    {
                        stealthObject.EyeController_Tr();
                        playerImage.enabled = false;
                        stealth = true;
                        active = false;
                        capsuleCollider.isTrigger = true;
                        this.transform.position = new Vector3(stealthObject.transform.position.x, stealthObject.transform.position.y, this.transform.position.z);
                        if (stealthObject.canMove)
                        {
                            addedRigidbody = stealthObject.gameObject.AddComponent<Rigidbody2D>();
                        }
                    }
                }
            }
        }
        else
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            Vector2 dir = new Vector2(h, v).normalized;

            if (active)
            {
                rb.velocity = dir * m_moveSpeed;
            }
            else if (stealth)
            {
                if (stealthObject.canMove)
                {
                    addedRigidbody.velocity = dir * m_moveSpeed;
                    this.transform.position = stealthObject.transform.position;
                }
            }
        }

        //方向によって画像変更
        #region
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
        #endregion

        if (playerHp <= 0 && active)
        {
            GameOverCH();
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
        if (collision.gameObject.tag == "Enemy" && !isDamaged)
        {
            playerHp -= 1;
            isDamaged = true;
            foreach (EnemyController enemy in enemyCon)
            {
                enemy.enemyProjector.GetComponent<CapsuleCollider2D>().isTrigger = true;
            }
            Invoke("FlashEnd", invincibleTime);
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
            if (nowColor != COLOR_TYPE.Blank)
            {
                if (stealthObject.nowColor == nowColor)
                {
                    dialog.SetActive(true);
                    enterStayStealthObj = true;
                }
            }
        }
        if (collision.gameObject.tag == "Switch")
        {
            collision.gameObject.GetComponent<TrapSwich>().TrapActuation();
        }
        if (collision.gameObject.tag == "vent")
        {
            vent_S = collision.GetComponent<Vent>();
            VentNum = vent_S.ventNumber;
            ventBool = true;
            if (enemyCon.Count == 0)
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
            enterStayStealthObj = false;
            stealthObject = null;
        }
        if (collision.gameObject.tag == "vent")
        {
            ventBool = false;
            dialog.SetActive(false);
            BeingTrackedOBJ.gameObject.SetActive(false);
        }
    }
    public void GameOverCH()
    {
        active = false;
        gameManager.OpenResult(false);
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
                playerImage.enabled = true;
                stealthObject.EyeController_Fa();
                capsuleCollider.isTrigger = false;
                stealth = false;
                active = true;
                dialog.SetActive(false);
            }
        }
        else if (enterStayStealthObj)
        {
            if (stealthObject.nowColor != nowColor)
            {
                dialog.SetActive(false);
                enterStayStealthObj = false;
            }
            else if (stealthObject.nowColor == nowColor)
            {
                playerImage.enabled = true;
                stealthObject.EyeController_Fa();
                dialog.SetActive(true);
                enterStayStealthObj = true;
            }
        }
    }

    void FlashEnd()
    {
        isDamaged = false;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1);
        foreach (EnemyController enemy in enemyCon)
        {
            enemy.enemyProjector.GetComponent<CapsuleCollider2D>().isTrigger = true;
        }
    }
}
