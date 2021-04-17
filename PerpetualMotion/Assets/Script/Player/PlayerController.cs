using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : ColorInfo
{
    [Header("プレイヤーの移動速度")]
    public float m_moveSpeed = 4.0f;
    public Rigidbody2D rb;
    float m_stopSpeed;
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

    // Start is called before the first frame update
    void Start()
    {
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
        //if (/*Input.GetKey(KeyCode.LeftArrow) || */Input.GetKey(KeyCode.A))
        //{
        //    imageIndex = 2;
        //    playerImage.sprite = SelectColor(nowColor)[imageIndex];
        //    if (active)
        //    {
        //        //transform.Translate(-m_moveSpeed, 0, 0);
        //        //rb.velocity = new Vector2(-m_moveSpeed, 0);
        //    }
        //}
        //if (/*Input.GetKey(KeyCode.RightArrow) || */Input.GetKey(KeyCode.D))
        //{
        //    imageIndex = 3;
        //    playerImage.sprite = SelectColor(nowColor)[imageIndex];
        //    if (active)
        //    {
        //        //transform.Translate(m_moveSpeed, 0, 0);
        //        //rb.velocity = new Vector2(m_moveSpeed, 0);
        //    }
        //}
        //if (/*Input.GetKey(KeyCode.UpArrow) || */Input.GetKey(KeyCode.W))
        //{
        //    imageIndex = 0;
        //    playerImage.sprite = SelectColor(nowColor)[imageIndex];
        //    if (active)
        //    {
        //        //transform.Translate(0, m_moveSpeed, 0);
        //        //rb.velocity = new Vector2(0, m_moveSpeed);
        //    }
        //}
        //if (/*Input.GetKey(KeyCode.DownArrow) || */Input.GetKey(KeyCode.S))
        //{
        //    imageIndex = 1;
        //    playerImage.sprite = SelectColor(nowColor)[imageIndex];
        //    if (active)
        //    {
        //        //transform.Translate(0, -m_moveSpeed, 0);
        //        //rb.velocity = new Vector2(0, -m_moveSpeed);
        //    }
        //}

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
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Enemy")
        {
            playerHp -= 1;
            if (gameManager.slot.item != null)
            {
                gameManager.LostItem(this.transform.position);
            }
        }
        if (collision.gameObject.tag == "Item")
        {
            gameManager.GetItem(collision.gameObject.GetComponent<MobiusParts>().item);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Mobius")
        {
            if (gameManager.slot.item != null)
            {
                gameManager.RemoveItem();
                gameManager.FitPiece();
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

    public void mod_craft()
    {
        m_moveSpeed = 0;
    }
    public void mod_mov()
    {
        m_moveSpeed = m_stopSpeed;
    }
}
