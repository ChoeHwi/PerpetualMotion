using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//オブジェクトにNavMeshAgentコンポーネントを設置
[RequireComponent(typeof(NavMeshAgent))]

public class EnemyController : MonoBehaviour
{
    [SerializeField] AnimationImages blankImages;
    [SerializeField] AnimationImages redImages;
    [SerializeField] AnimationImages buleImages;
    [SerializeField] AnimationImages greenImages;
    /// <summary>現在のカラーイメージ</summary>
    AnimationImages m_colorSprites;
    /// <summary>現在アニメーションしているイメージの配列</summary>
    Sprite[] m_animationSprites;
    /// <summary>アニメーションのスピード</summary>
    [SerializeField] float m_count = 0.25f;
    /// <summary>時間のカウンター</summary>
    float m_counter = 0;
    /// <summary>アニメーション画像の番号</summary>
    int m_imageIndex = 0;
    /// <summary>移動可能かどうか</summary>
    bool m_active;
    [Header("移動速度")]
    /// <summary>移動速度</summary>
    [SerializeField] float speed;
    [Header("巡回地点")]
    /// <summary>巡回地点</summary>
    public Transform[] points;
    /// <summary>現在の巡回地のインデックス</summary>
    int destPoint = 0;
    /// <summary>このオブジェクトのAI</summary>
    private NavMeshAgent agent;
    /// <summary>ターゲットの位置</summary>
    Vector3 targetPos;
    [Header("追いかける対象の名前")]
    /// <summary>追いかける対象の名前</summary>
    public string targetName = "Player";
    /// <summary>追いかける対象</summary>
    GameObject target;
    /// <summary>ターゲットからの距離</summary>
    float distance;
    [Header("追跡を開始する範囲の半径")]
    /// <summary>この範囲に入ったら追跡</summary>
    [SerializeField] float trackingRange = 3f;
    [Header("追跡を停止する範囲の半径")]
    /// <summary>この範囲から出たら追跡をやめる</summary>
    [SerializeField] float quitRange = 5f;
    /// <summary>追跡状態かどうか</summary>
    public bool tracking = false;
    [SerializeField] GameObject projectorObj;
    /// <summary>このオブジェクトの見た目のオブジェクト</summary>
    public GameObject enemyProjector;
    /// <summary>プレイヤーコントローラー</summary>
    PlayerController playerController;
    [Header("この敵の色")]
    public ColorInfo.COLOR_TYPE nowColor = ColorInfo.COLOR_TYPE.Red;
    public SpriteRenderer enemyImage;
    int imageIndex = 1;
    bool onPlayer;
    int angle;
    Transform m_transform;

    public bool alarm = false;
    void Start()
    {
        alarm = false;
        m_transform = transform;
        agent = GetComponent<NavMeshAgent>();
        enemyProjector = Instantiate(projectorObj, new Vector3(this.transform.position.x, this.transform.position.y, 0), new Quaternion(0, 0, 0, 0));
        enemyProjector.GetComponent<EnemyColliderController>().enemyController = this;
        enemyImage = enemyProjector.GetComponent<SpriteRenderer>();
        Form_Color(nowColor);

        // autoBraking を無効にすると、目標地点の間を継続的に移動します
        //(つまり、エージェントは目標地点に近づいても
        // 速度をおとしません)
        agent.autoBraking = false;
        agent.speed = speed;

        GotoNextPoint();

        //追跡したいオブジェクトの名前を入れる
        target = GameObject.Find(targetName);

        if (target.GetComponent<PlayerController>())
        {
            playerController = target.GetComponent<PlayerController>();
        }
    }


    void GotoNextPoint()
    {
        // 地点がなにも設定されていないときに返します
        if (points.Length == 0)
            return;

        // エージェントが現在設定された目標地点に行くように設定します
        agent.destination = points[destPoint].position;

        // 配列内の次の位置を目標地点に設定し、
        // 必要ならば出発地点にもどります
        destPoint = (destPoint + 1) % points.Length;
    }


    void Update()
    {
        enemyProjector.transform.position = new Vector3(m_transform.position.x, m_transform.position.y, 0);
        //Playerとこのオブジェクトの距離を測る
        targetPos = target.transform.position;

        angle = (int)(Mathf.Asin(m_transform.forward.x) * Mathf.Rad2Deg);
        if (angle < 0)
        {
            angle += 360;
        }
        if (angle <= 22.5 || angle >= 337.5)
        {
            if (transform.localScale.x < 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Abs(transform.localScale.x);
                transform.localScale = scale;
            }
            if (agent.destination.y < transform.position.y)//下
            {
                m_animationSprites = m_colorSprites.GetAnimImages(AnimationImages.ANIMATION_TYPE.Front);
            }
            else//上
            {
                m_animationSprites = m_colorSprites.GetAnimImages(AnimationImages.ANIMATION_TYPE.Back);
            }
        }
        else if (angle > 22.5 && angle < 67.5)
        {
            if (transform.localScale.x > 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = transform.localScale.x * -1;
                transform.localScale = scale;
            }
            if (agent.destination.y < transform.position.y)//右下
            {
                m_animationSprites = m_colorSprites.GetAnimImages(AnimationImages.ANIMATION_TYPE.Side);
            }
            else//右上
            {
                m_animationSprites = m_colorSprites.GetAnimImages(AnimationImages.ANIMATION_TYPE.Side);
            }
        }
        else if (angle >= 67.5 && angle < 112.5)//右
        {
            if (transform.localScale.x > 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = transform.localScale.x * -1;
                transform.localScale = scale;
            }
            m_animationSprites = m_colorSprites.GetAnimImages(AnimationImages.ANIMATION_TYPE.Side);
        }
        else if (angle >= 157.5 && angle < 202.5)//下
        {
            if (transform.localScale.x < 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Abs(transform.localScale.x);
                transform.localScale = scale;
            }
            m_animationSprites = m_colorSprites.GetAnimImages(AnimationImages.ANIMATION_TYPE.Front);
        }
        else if (angle > 202.5 && angle <= 247.5)//左下
        {
            if (transform.localScale.x < 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Abs(transform.localScale.x);
                transform.localScale = scale;
            }
            m_animationSprites = m_colorSprites.GetAnimImages(AnimationImages.ANIMATION_TYPE.Side);
        }
        else if (angle >= 247.5 && angle < 292.5)//左
        {
            if (transform.localScale.x < 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Abs(transform.localScale.x);
                transform.localScale = scale;
            }
            m_animationSprites = m_colorSprites.GetAnimImages(AnimationImages.ANIMATION_TYPE.Side);
        }
        else if (angle >= 292 && angle < 337)
        {
            if (transform.localScale.x < 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Abs(transform.localScale.x);
                transform.localScale = scale;
            }
            if (agent.destination.y < transform.position.y)//左下
            {
                m_animationSprites = m_colorSprites.GetAnimImages(AnimationImages.ANIMATION_TYPE.Side);
            }
            else//左上
            {
                m_animationSprites = m_colorSprites.GetAnimImages(AnimationImages.ANIMATION_TYPE.Side);
            }
        }

        //アクティブ状態なら移動とアニメーションをする。
        if (m_active)
        {
            if (m_counter >= m_count)
            {
                EnemyAnimation();
                m_counter = 0;
            }
            else
            {
                m_counter += Time.deltaTime;
            }
        }

        distance = Vector3.Distance(enemyProjector.transform.position, targetPos);

        //追跡の時、quitRangeより距離が離れたら中止
        if (distance > quitRange || playerController.m_stealth)
        {
            tracking = false;
            playerController.enemyCon.Remove(this);
            onPlayer = false;
            enemyProjector.GetComponent<CapsuleCollider2D>().isTrigger = false;
        }

        if (tracking || alarm == true)
        {
            //Playerを目標とする
            agent.destination = targetPos;
        }
        else
        {
            //PlayerがtrackingRangeより近づいたら追跡開始
            if (distance < trackingRange)
            {
                if (!playerController.m_stealth)
                {
                    tracking = true;
                    foreach (EnemyController enemyController in playerController.enemyCon)
                    {
                        if (enemyController == this)
                        {
                            onPlayer = true;
                        }
                    }
                    if (!onPlayer)
                    {
                        playerController.enemyCon.Add(this);
                    }
                }
            }

            // エージェントが現目標地点に近づいてきたら、
            // 次の目標地点を選択します
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();
        }
    }

    /*void OnDrawGizmosSelected()
    {
        //trackingRangeの範囲を赤いワイヤーフレームで示す
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemyProjector.transform.position, trackingRange);

        //quitRangeの範囲を青いワイヤーフレームで示す
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(enemyProjector.transform.position, quitRange);
    }*/

    public void Form_Color(ColorInfo.COLOR_TYPE color)
    {
        nowColor = color;
        m_colorSprites = SelectColor(nowColor);
    }

    public void Freeze()
    {
        agent.isStopped = true;
    }

    public void FreezeOff()
    {
        agent.isStopped = false;
    }

    /// <summary>スクリプトアニメーション</summary>
    void EnemyAnimation()
    {
        enemyImage.sprite = m_animationSprites[m_imageIndex];
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

    /// <summary>エネミーのアニメーション一覧</summary>
    [System.Serializable]
    public class AnimationImages
    {
        [SerializeField] public Sprite[] m_idleImages;
        [SerializeField] public Sprite[] m_backImages;
        [SerializeField] public Sprite[] m_frontImages;
        [SerializeField] public Sprite[] m_sideImages;
        [SerializeField] public Sprite[] m_attackImages;

        public enum ANIMATION_TYPE
        {
            Idle,
            Back,
            Side,
            Front,
            Attack
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
                case ANIMATION_TYPE.Attack:
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
