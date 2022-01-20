﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

/// <summary>
/// プレイヤーの制御
/// </summary>
public class PlayerController : MonoBehaviour
{
    ///プランナーが設定するパラメーター
    #region
    [Header("プレイヤーの移動速度")]
    public float m_moveSpeed = 4.0f;
    [Header("無敵時間")]
    [SerializeField] float m_invincibleTime = 3f;
    [Header("オブジェクトと融合したときのスピード")]
    [SerializeField] float ObjSpeed;
    #endregion

    //コンポーネント系
    #region
    /// <summary>リジッドボディ</summary>
    Rigidbody2D m_rb;
    /// <summary>スプライトレンダラー</summary>
    [SerializeField] SpriteRenderer m_spriteRenderer;
    /// <summary>カプセルコライダー</summary>
    CapsuleCollider2D capsuleCollider;
    #endregion
    /// <summary>無色の画像</summary>
    [SerializeField] AnimationImages blankImages;
    /// <summary>赤の画像</summary>
    [SerializeField] AnimationImages redImages;
    /// <summary>青の画像</summary>
    [SerializeField] AnimationImages buleImages;
    /// <summary>緑の画像</summary>
    [SerializeField] AnimationImages greenImages;

    /// <summary>現在のカラーイメージ</summary>
    AnimationImages m_colorSprites;
    
    /// <summary>現在アニメーションしているイメージの配列</summary>
    Sprite[] m_animationSprites;
    /// <summary>ダメージアニメーションの表示中</summary>
    bool m_isDamaged = false;
    /// <summary>アニメーションのスピード</summary>
    [SerializeField] float m_count = 0.25f;
    /// <summary>時間のカウンター</summary>
    float m_counter = 0;
    /// <summary>アニメーション画像の番号</summary>
    int m_imageIndex = 0;
    /// <summary>プレイヤーのライフ</summary>
    public int m_playerHp = 5;
    /// <summary>プレイヤーの現在の色</summary>
    public ColorInfo.COLOR_TYPE nowColor = ColorInfo.COLOR_TYPE.Blank;
    /// <summary>ステルス可能な状態か</summary>
    bool m_canStealth;
    /// <summary>ステルス状態か</summary>
    public bool m_stealth = false;
    /// <summary>プレイヤーが操作可能か</summary>
    public bool m_active = false;
    /// <summary>ゲームマネージャー</summary>
    GameManager gameManager;
    /// <summary>今できることを表示するUI</summary>
    [SerializeField] GameObject dialog;
    /// <summary>現在のアニメーションタイプ</summary>
    AnimationImages.ANIMATION_TYPE animationType  = AnimationImages.ANIMATION_TYPE.Front;
    /// <summary>現在入ることのできるステルスオブジェクト</summary>
    public StealthObject stealthObject;
    /// <summary>ステルスオブジェクトに追加したRigidbody2D</summary>
    Rigidbody2D addedRigidbody;
    //アイテム関連
    public int itemCount;
    [SerializeField] GameObject[] itemType = new GameObject[1];
    //通気口関連
    /// <summary>Ventスクリプトが入る</summary>
    Vent vent_S;
    /// <summary>VentManagerスクリプトが入る</summary>
    ventManager vent_Mana;
    /// <summary>Ventに触れているかどうか</summary>
    bool ventBool;
    /// <summary>触れたVentの番号</summary>
    public int VentNum;
    /// <summary>Ventに入っているかどうか</summary>
    bool Vent_ch = false;
    /// <summary>現在追いかけられてる敵</summary>
    public List<EnemyController> enemyCon = new List<EnemyController>(0);
    public Canvas BeingTrackedOBJ;
    Vector2 enterPosition;
    /// <summary>eyeのプレハブ</summary>
    [Header("eyeのプレハブを入れる")]
    [SerializeField] GameObject eye;
    /// <summary>eyeの生成場所</summary>
    GameObject instancedEye;
    /// <summary> AudioManagerを参照する変数 </summary>
    AudioManager audioManager;

    void Start()
    {
        vent_Mana = GameObject.FindObjectOfType<ventManager>();
        vent_S = GameObject.FindObjectOfType<Vent>();
        //m_spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        m_rb = GetComponent<Rigidbody2D>();
        if (GameObject.FindObjectOfType<AudioManager>())
        {
            audioManager = GameObject.FindObjectOfType<AudioManager>();
        }
    }

    void Update()
    {
        //入力方向を向く
        #region
        if (InputChanger.InputInform(InputChanger.INPUTKEY_TYPE.Right))//Input.GetKeyDown(KeyCode.D))
        {
            animationType = AnimationImages.ANIMATION_TYPE.Side;
            m_animationSprites = m_colorSprites.GetAnimImages(animationType);
            if (transform.localScale.x > 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = transform.localScale.x * -1;
                transform.localScale = scale;
            }
            PlayerAnimation();
        }
        else if (InputChanger.InputInform(InputChanger.INPUTKEY_TYPE.Left))//Input.GetKeyDown(KeyCode.A))
        {
            animationType = AnimationImages.ANIMATION_TYPE.Side;
            m_animationSprites = m_colorSprites.GetAnimImages(animationType);
            if (transform.localScale.x < 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Abs(transform.localScale.x);
                transform.localScale = scale;
            }
            PlayerAnimation();
        }
        else if(InputChanger.InputInform(InputChanger.INPUTKEY_TYPE.Down))//Input.GetKeyDown(KeyCode.S))
        {
            animationType = AnimationImages.ANIMATION_TYPE.Front;
            m_animationSprites = m_colorSprites.GetAnimImages(animationType);
            if (transform.localScale.x < 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Abs(transform.localScale.x);
                transform.localScale = scale;
            }
            PlayerAnimation();
        }
        else if (InputChanger.InputInform(InputChanger.INPUTKEY_TYPE.Up))//Input.GetKeyDown(KeyCode.W))
        {
            animationType = AnimationImages.ANIMATION_TYPE.Back;
            m_animationSprites = m_colorSprites.GetAnimImages(animationType);
            if (transform.localScale.x < 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Abs(transform.localScale.x);
                transform.localScale = scale;
            }
            PlayerAnimation();
        }
        #endregion
        if (ventBool && enemyCon.Count == 0)//ベントオブジェクトに触れており、追いかけられてないなら
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!Vent_ch)
                {
                    Vent_ch = true;
                    m_active = false;
                }
                else if (Vent_ch && vent_S)
                {
                    Vent_ch = false;
                    m_active = true;
                    transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                }
            }
            if (Vent_ch)
            {
                if (VentNum < 0)
                {
                    VentNum = vent_Mana.num;
                }
                if (InputChanger.InputInform(InputChanger.INPUTKEY_TYPE.Left))//Input.GetKeyDown(KeyCode.A))
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
                if (InputChanger.InputInform(InputChanger.INPUTKEY_TYPE.Right))//Input.GetKeyDown(KeyCode.D))
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
        if (Input.GetKeyDown(KeyCode.Space))//スペースを押したとき
        {
            //隠れる
            if (audioManager)
            {
                audioManager.PlaySE(audioManager.audioClips[6]);
            }

            if (stealthObject)//ステルスオブジェクトに触れているなら
            {
                if (m_stealth)//ステルス状態なら
                {
                    //離れるSE
                    if (audioManager)
                    {
                        audioManager.PlaySE(audioManager.audioClips[7]);
                    }
                    StealthOff();
                }
                else
                {
                    if (m_canStealth)//ステルスできる状態なら
                    {
                        StealthOn();
                    }
                }
            }
        }
        else
        {
            float h = InputChanger.InputAxisRaw(true);
            float v = InputChanger.InputAxisRaw(false);
            Vector2 dir = new Vector2(h, v).normalized;
            //アクティブ状態なら移動とアニメーションをする。
            if (m_active)
            {
                m_rb.velocity = dir * m_moveSpeed;
                if (m_counter >= m_count)
                {
                    if (h != 0 || v != 0)
                    {
                        PlayerAnimation();
                        //移動
                        if (audioManager)
                        {
                            audioManager.PlaySE(audioManager.audioClips[5]);
                        }
                    }
                    m_counter = 0;
                }
                else
                {
                    m_counter += Time.deltaTime;
                }
            }
            else if (m_stealth)
            {
                if (stealthObject.canMove)
                {
                    addedRigidbody.velocity = dir * m_moveSpeed;
                    this.transform.position = stealthObject.transform.position;
                }
            }
        }
        if (m_playerHp <= 0 && m_active)
        {
            GameOverCH();
        }
    }

    /// <summary>無敵時間中の処理</summary>
    void FixedUpdate()
    {
        if (m_isDamaged)
        {
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level);
        }
    }

    /// <summary>無敵時間の終了時の処理</summary>
    void FlashEnd()
    {
        if (enemyCon.Count > 0)
        {
            if (GetAttackBool())
            {
                m_playerHp -= 1;
                if (audioManager)
                {
                    audioManager.PlaySE(audioManager.audioClips[8]);
                }
                if (m_playerHp > 0)
                {
                    Invoke("FlashEnd", m_invincibleTime);
                    if (itemCount > 0)
                    {
                        itemCount -= 1;
                        gameManager.LostItem(this.transform.position, itemType[0]);
                    }
                }
            }
            else
            {
                m_isDamaged = false;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1);
                if (enemyCon.Count > 0)
                {
                    foreach (EnemyController enemy in enemyCon)
                    {
                        enemy.m_enemyProjector.GetComponent<CapsuleCollider2D>().isTrigger = false;
                    }
                }
            }
        }
        else
        {
            m_isDamaged = false;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1);
            if (enemyCon.Count > 0)
            {
                foreach (EnemyController enemy in enemyCon)
                {
                    enemy.m_enemyProjector.GetComponent<CapsuleCollider2D>().isTrigger = false;
                }
            }
        }
    }

    /// <summary>
    /// 現在攻撃されているかどうかを取得する
    /// </summary>
    /// <returns></returns>
    private bool GetAttackBool()
    {
        foreach (EnemyController enemy in enemyCon)
        {
            if (enemy.m_isAttack)
            {
                return true;
            }
        }
        return false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !m_isDamaged && !m_stealth)
        {
            if (!collision.gameObject.GetComponent<EnemyColliderController>().enemyController.m_isMalfunction)
            {
                m_isDamaged = true;
                m_playerHp -= 1;
                //ダメージを受けた時
                if (audioManager)
                {
                    audioManager.PlaySE(audioManager.audioClips[8]);
                }
                if (enemyCon.Count > 0)
                {
                    foreach (EnemyController enemy in enemyCon)
                    {
                        Debug.Log(enemy.m_enemyProjector.GetComponent<CapsuleCollider2D>());
                        enemy.m_enemyProjector.GetComponent<CapsuleCollider2D>().isTrigger = true;
                    }
                }
                Invoke("FlashEnd", m_invincibleTime);
                if (itemCount > 0)
                {
                    itemCount -= 1;
                    gameManager.LostItem(this.transform.position, itemType[0]);
                }
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "stealth")
        {
            stealthObject = collision.GetComponent<StealthObject>();
            if (nowColor != ColorInfo.COLOR_TYPE.Blank)
            {
                if (stealthObject.nowColor == nowColor)
                {
                    dialog.SetActive(true);
                    m_canStealth = true;
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
            m_canStealth = false;
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
        m_active = false;
        gameManager.OpenResult(false);
    }
    public void Vent_Pos()
    {
        transform.position = new Vector3(vent_Mana.vent_Pos[VentNum].position.x, vent_Mana.vent_Pos[VentNum].position.y, 1);
    }

    /// <summary>色変更</summary>
    /// <param name="color">色のタイプ</param>
    public void Form_Color(ColorInfo.COLOR_TYPE color)
    {
        nowColor = color;
        m_colorSprites = SelectColor(nowColor);
        if (stealthObject)
        {
            if (m_stealth)
            {
                if (stealthObject.nowColor != nowColor)
                {
                    StealthOff();
                }
            }
            else
            {
                if (stealthObject.nowColor != nowColor)
                {
                    dialog.SetActive(false);
                    m_canStealth = false;
                }
                else if (stealthObject.nowColor == nowColor)
                {
                    m_spriteRenderer.enabled = true;
                    dialog.SetActive(true);
                    m_canStealth = true;
                }
            }
        }
        m_animationSprites = m_colorSprites.GetAnimImages(animationType);
        PlayerAnimation();
    }
    
    /// <summary>ステルスオン</summary>
    void StealthOn()
    {
        instancedEye = Instantiate(eye, stealthObject.transform);
        //instancedEye.transform.localScale = stealthObject.transform.localScale;
        m_rb.velocity = new Vector2(0, 0);
        enterPosition = this.transform.position;
        m_stealth = true;
        capsuleCollider.isTrigger = true;
        //this.transform.position = new Vector3(stealthObject.transform.position.x, stealthObject.transform.position.y, this.transform.position.z);
        transform.SetParent(stealthObject.transform);
        this.transform.localPosition = new Vector2(0, 0);
        m_spriteRenderer.enabled = false;
        if (stealthObject.canMove)
        {
            addedRigidbody = stealthObject.gameObject.AddComponent<Rigidbody2D>();
        }
        else
        {
            m_active = false;
        }
    }

    /// <summary>ステルス解除</summary>
    void StealthOff()
    {
        Destroy(instancedEye);
        transform.SetParent(null);
        m_spriteRenderer.enabled = true;
        this.transform.position = enterPosition;
        capsuleCollider.isTrigger = false;
        m_stealth = false;
        m_active = true;
        if (stealthObject.canMove)
        {
            Destroy(addedRigidbody);
        }
    }

    /// <summary>スクリプトアニメーション</summary>
    void PlayerAnimation()
    {
        m_spriteRenderer.sprite = m_animationSprites[m_imageIndex];
        m_imageIndex = (m_imageIndex + 1) % m_animationSprites.Length;
    }

    /// <summary>色のタイプを渡すとその色の画像を返す</summary>
    /// <param name="colorType">色のタイプ</param>
    /// <returns></returns>
    public AnimationImages SelectColor(ColorInfo.COLOR_TYPE colorType)
    {
        AnimationImages image = null;
        switch (colorType)
        {
            case ColorInfo.COLOR_TYPE.Blank:
                image = blankImages;
                break;
            case ColorInfo.COLOR_TYPE.Red:
                image = redImages;
                break;
            case ColorInfo.COLOR_TYPE.Bule:
                image = buleImages;
                break;
            case ColorInfo.COLOR_TYPE.Green:
                image = greenImages;
                break;
        }
        return image;
    }

    /// <summary>プレイヤーのアニメーション一覧</summary>
    [System.Serializable]
    public class AnimationImages
    {
        [SerializeField] public Sprite[] m_idleImages;
        [SerializeField] public Sprite[] m_backImages;
        [SerializeField] public Sprite[] m_frontImages;
        [SerializeField] public Sprite[] m_sideImages;

        public enum ANIMATION_TYPE
        {
            Idle,
            Back,
            Side,
            Front,
        }

        public Sprite[] GetAnimImages(ANIMATION_TYPE type)
        {
            Sprite[] sprites;
            switch (type)
            {
                case ANIMATION_TYPE.Idle:
                    sprites = m_idleImages;
                    break;
                case ANIMATION_TYPE.Back:
                    sprites = m_backImages;
                    break;
                case ANIMATION_TYPE.Side:
                    sprites = m_sideImages;
                    break;
                case ANIMATION_TYPE.Front:
                    sprites = m_frontImages;
                    break;
                default:
                    sprites = null;
                    break;
            }
            return sprites;
        }

    }
}
