using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ここは実際に見えるEnemyの実体です。Enemy側で衝突判定を取る場合はここに書いてください
/// </summary>
public class EnemyColliderController : MonoBehaviour
{
    [SerializeField] EnemyController.ENEMY_TYPE m_enemyType;
    /// <summary>生成時に渡されるコントローラー</summary>
    public EnemyController enemyController;
    [SerializeField] AnimationImages redImages;
    [SerializeField] AnimationImages buleImages;
    /// <summary>現在の色のイメージ</summary>
    AnimationImages m_colorSprites;
    /// <summary>現在アニメーションしているイメージの配列</summary>
    Sprite[] m_animationSprites;
    public SpriteRenderer enemyImage;
    /// <summary>アニメーション画像の番号</summary>
    int m_imageIndex = 0;
    /// <summary>ドローンが発射する弾</summary>
    [SerializeField] GameObject laser;

    // Start is called before the first frame update
    void Start()
    {
        enemyImage = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 色の画像を変更
    /// </summary>
    public void ChangeColorImage(ColorInfo.COLOR_TYPE color)
    {
        m_colorSprites = SelectColor(color);
    }

    /// <summary>
    /// アニメーションする画像を変更
    /// </summary>
    public void AnimChange(AnimationImages.ANIMATION_TYPE type)
    {
        if (m_animationSprites != m_colorSprites.GetAnimImages(type))
        {
            m_imageIndex = 0;
        }
        m_animationSprites = m_colorSprites.GetAnimImages(type);
        if (m_enemyType == EnemyController.ENEMY_TYPE.DroneType)
        {
            if (type == AnimationImages.ANIMATION_TYPE.AttackRight || type == AnimationImages.ANIMATION_TYPE.Right)
            {
                if (transform.localScale.x < 0)
                {
                    Vector3 scale = transform.localScale;
                    scale.x = Mathf.Abs(transform.localScale.x);
                    transform.localScale = scale;
                }
            }
            else
            {
                if (transform.localScale.x > 0)
                {
                    Vector3 scale = transform.localScale;
                    scale.x = transform.localScale.x * -1;
                    transform.localScale = scale;
                }
            }
        }
        else
        {
            if (type == AnimationImages.ANIMATION_TYPE.AttackRight || type == AnimationImages.ANIMATION_TYPE.Right)
            {
                if (transform.localScale.x > 0)
                {
                    Vector3 scale = transform.localScale;
                    scale.x = transform.localScale.x * -1;
                    transform.localScale = scale;
                }
            }
            else
            {
                if (transform.localScale.x < 0)
                {
                    Vector3 scale = transform.localScale;
                    scale.x = Mathf.Abs(transform.localScale.x);
                    transform.localScale = scale;
                }
            }
        }
    }

    /// <summary>スクリプトアニメーション</summary>
    public void EnemyAnimation()
    {
        enemyImage.sprite = m_animationSprites[m_imageIndex];
        m_imageIndex = (m_imageIndex + 1) % m_animationSprites.Length;
        if (m_enemyType == EnemyController.ENEMY_TYPE.DroneType)
        {
            if (m_imageIndex == m_animationSprites.Length)
            {
                Instantiate(laser, this.transform.position, Quaternion.identity).GetComponent<Laser>().Positioning(enemyController.transform.rotation);
            }
        }
    }

    /// <summary>故障中</summary>
    public void Malfunction()
    {
        enemyImage.sprite = m_colorSprites.m_malfunctionImage;
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
                //image = blankImages;
                break;
            case ColorInfo.COLOR_TYPE.Red:
                image = redImages;
                break;
            case ColorInfo.COLOR_TYPE.Bule:
                image = buleImages;
                break;
            case ColorInfo.COLOR_TYPE.Green:
                //image = greenImages;
                break;
        }
        return image;
    }
}

/// <summary>エネミーのアニメーション一覧</summary>
[System.Serializable]
public class AnimationImages
{
    [SerializeField] public Sprite[] m_idleImages;
    [SerializeField] public Sprite[] m_backImages;
    [SerializeField] public Sprite[] m_frontImages;
    [SerializeField] public Sprite[] m_sideImages;
    [SerializeField] public Sprite[] m_attackFrontImages;
    [SerializeField] public Sprite[] m_attackBackImages;
    [SerializeField] public Sprite[] m_attackSideImages;
    [SerializeField] public Sprite m_malfunctionImage;

    public enum ANIMATION_TYPE
    {
        Idle,
        Back,
        Left,
        Right,
        Front,
        AttackBack,
        AttackLeft,
        AttackRight,
        AttackFront
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
            case ANIMATION_TYPE.Left:
                sprites = m_sideImages;
                break;
            case ANIMATION_TYPE.Right:
                sprites = m_sideImages;
                break;
            case ANIMATION_TYPE.Front:
                sprites = m_frontImages;
                break;
            case ANIMATION_TYPE.AttackBack:
                sprites = m_attackBackImages;
                break;
            case ANIMATION_TYPE.AttackLeft:
                sprites = m_attackSideImages;
                break;
            case ANIMATION_TYPE.AttackRight:
                sprites = m_attackSideImages;
                break;
            case ANIMATION_TYPE.AttackFront:
                sprites = m_attackFrontImages;
                break;
            default:
                sprites = null;
                break;
        }
        return sprites;
    }
}


