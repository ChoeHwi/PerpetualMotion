using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : ColorInfo
{
    public float m_moveSpeed = 0.02f;
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

    // Start is called before the first frame update
    void Start()
    {
        m_stopSpeed = m_moveSpeed;
        playerImage = GetComponent<SpriteRenderer>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame

    void Update()
    {
        if (/*Input.GetKey(KeyCode.LeftArrow) || */Input.GetKey(KeyCode.A) && active)
        {
            transform.Translate(-m_moveSpeed, 0, 0);
            imageIndex = 2;
            playerImage.sprite = SelectColor(nowColor)[imageIndex];
        }
        if (/*Input.GetKey(KeyCode.RightArrow) || */Input.GetKey(KeyCode.D) && active)
        {
            transform.Translate(m_moveSpeed, 0, 0);
            imageIndex = 3;
            playerImage.sprite = SelectColor(nowColor)[imageIndex];
        }
        if (/*Input.GetKey(KeyCode.UpArrow) || */Input.GetKey(KeyCode.W) && active)
        {
            transform.Translate(0, m_moveSpeed, 0);
            imageIndex = 0;
            playerImage.sprite = SelectColor(nowColor)[imageIndex];
        }
        if (/*Input.GetKey(KeyCode.DownArrow) || */Input.GetKey(KeyCode.S) && active)
        {
            transform.Translate(0, -m_moveSpeed, 0);
            imageIndex = 1;
            playerImage.sprite = SelectColor(nowColor)[imageIndex];
        }
        if (playerHp <= 0　&& active)
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
                    stealth = true;
                    active = false;
                    capsuleCollider.isTrigger = true;
                    this.transform.position = new Vector3(stealthPosition.transform.position.x, stealthPosition.transform.position.y, this.transform.position.z);
                }
            }
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "enemy")
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
            dialog.SetActive(true);
            triggerStay = true;
            stealthPosition = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "stealth")
        {
            dialog.SetActive(false);
            triggerStay = false;
        }
    }

    public void Form_Color(COLOR_TYPE color)
    {
        nowColor = color;
        playerImage.sprite = SelectColor(nowColor)[imageIndex];
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
