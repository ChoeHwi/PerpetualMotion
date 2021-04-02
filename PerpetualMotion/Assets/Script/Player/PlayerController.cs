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

    // Start is called before the first frame update
    void Start()
    {
        m_stopSpeed = m_moveSpeed;
        playerImage = GetComponent<SpriteRenderer>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
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
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "stealth")
        {
            dialog.SetActive(false);
            triggerStay = false;
            stealthObject = null;
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
