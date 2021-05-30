using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//オブジェクトにNavMeshAgentコンポーネントを設置
[RequireComponent(typeof(NavMeshAgent))]

public class EnemyController : MonoBehaviour
{
    //プランナーの設定項目
    [Header("移動速度")]
    /// <summary>移動速度</summary>
    [SerializeField] float m_speed;
    [Header("巡回地点")]
    /// <summary>巡回地点</summary>
    public Transform[] m_points;
    [Header("追いかける対象の名前")]
    /// <summary>追いかける対象の名前</summary>
    public string m_targetName = "Player";
    [Header("追跡を開始する範囲の半径")]
    /// <summary>この範囲に入ったら追跡</summary>
    [SerializeField] float m_trackingRange = 3f;
    [Header("追跡を停止する範囲の半径")]
    /// <summary>この範囲から出たら追跡をやめる</summary>
    [SerializeField] float m_quitRange = 5f;
    [Header("攻撃する範囲の半径")]
    [SerializeField] float m_attackRange = 1f;
    [Header("この敵の色")]
    public ColorInfo.COLOR_TYPE m_nowColor = ColorInfo.COLOR_TYPE.Red;
    [Header("敵のタイプ")]
    public ENEMY_TYPE m_enemyType = ENEMY_TYPE.RargeType;


    /// <summary>アニメーションのスピード</summary>
    [SerializeField] float m_count = 0.25f;
    /// <summary>時間のカウンター</summary>
    float m_counter = 0;
    /// <summary>アニメーションするかどうか</summary>
    bool m_isAnimation = true;
    /// <summary>追跡状態かどうか</summary>
    public bool m_tracking = false;
    /// <summary>攻撃状態かどうか</summary>
    public bool m_isAttack = false;
    /// <summary>現在の巡回地のインデックス</summary>
    int m_destPoint = 0;
    /// <summary>このオブジェクトのAI</summary>
    NavMeshAgent m_agent;
    /// <summary>ターゲットの位置</summary>
    Vector3 m_targetPos;
    /// <summary>追いかける対象</summary>
    GameObject m_target;
    /// <summary>ターゲットからの距離</summary>
    float m_distance;
    /// <summary>敵のソースプレハブ</summary>
    [SerializeField] GameObject[] enemyEntitys;
    /// <summary>このオブジェクトの実体</summary>
    public GameObject m_enemyProjector;
    /// <summary>実体のスクリプト</summary>
    EnemyColliderController m_enemyScipt;
    /// <summary>プレイヤーコントローラー</summary>
    PlayerController m_playerController;
    bool onPlayer;
    /// <summary>このオブジェクトの角度</summary>
    int m_angle;
    /// <summary>トランスフォームのキャッシュ</summary>
    Transform m_transform;
    /// <summary>ここから指令を出すエフェクト</summary>
    public Effect effect;
    /// <summary>故障中</summary>
    public bool m_isMalfunction;

    public enum ENEMY_TYPE
    { 
        RargeType,
        HumanoidType,
        DroneType,
        LowType
    }

    public bool alarm = false;
    void Start()
    {
        alarm = false;
        m_transform = transform;
        m_agent = GetComponent<NavMeshAgent>();
        m_enemyProjector = Instantiate(enemyEntitys[(int)m_enemyType], new Vector3(this.transform.position.x, this.transform.position.y, 0), new Quaternion(0, 0, 0, 0));
        m_enemyScipt =  m_enemyProjector.GetComponent<EnemyColliderController>();
        m_enemyScipt.enemyController = this;
        if (GameObject.FindObjectOfType<Effect>())
        {
            effect = GameObject.FindObjectOfType<Effect>().GetComponent<Effect>();
        }
        Form_Color(m_nowColor);

        // autoBraking を無効にすると、目標地点の間を継続的に移動します
        //(つまり、エージェントは目標地点に近づいても
        // 速度をおとしません)
        m_agent.autoBraking = false;
        m_agent.speed = m_speed;

        GotoNextPoint();

        //追跡したいオブジェクトの名前を入れる
        m_target = GameObject.Find(m_targetName);

        if (m_target.GetComponent<PlayerController>())
        {
            m_playerController = m_target.GetComponent<PlayerController>();
        }
    }


    void GotoNextPoint()
    {
        // 地点がなにも設定されていないときに返します
        if (m_points.Length == 0)
            return;

        // エージェントが現在設定された目標地点に行くように設定します
        m_agent.destination = m_points[m_destPoint].position;

        // 配列内の次の位置を目標地点に設定し、
        // 必要ならば出発地点にもどります
        m_destPoint = (m_destPoint + 1) % m_points.Length;
    }


    void Update()
    {
        m_enemyProjector.transform.position = new Vector3(m_transform.position.x, m_transform.position.y, 0);
        //Playerオブジェクトの位置取得
        m_targetPos = m_target.transform.position;
        //Playerとこのオブジェクトの距離を測る
        m_distance = Vector3.Distance(m_enemyProjector.transform.position, m_targetPos);
        //このオブジェクトの角度取得
        m_angle = (int)(Mathf.Asin(m_transform.forward.x) * Mathf.Rad2Deg);
        //プレイヤーが攻撃範囲に入り、ステルス状態じゃないなら攻撃状態にする
        if (m_attackRange > m_distance && !m_playerController.m_stealth)
        {
            m_isAttack = true;
        }
        else if (m_attackRange < m_distance && m_isAttack)
        {
            m_isAttack = false;
        }
        if (m_isAttack)
        {
            Freeze();
        }
        else
        {
            FreezeOff();
        }
        //Debug.Log(m_angle);
        if (m_angle < 0)
        {
            m_angle += 360;
        }
        if (m_angle <= 22.5 || m_angle >= 337.5)
        {
            if (m_transform.forward.y < 0)//下　
            {
                //Debug.Log("下1");
                if (m_isAttack)
                {
                    m_enemyScipt.AnimChange(AnimationImages.ANIMATION_TYPE.AttackFront);
                }
                else
                {
                    m_enemyScipt.AnimChange(AnimationImages.ANIMATION_TYPE.Front);
                }
            }
            else//上
            {
                //Debug.Log("上1");
                if (m_isAttack)
                {
                    m_enemyScipt.AnimChange(AnimationImages.ANIMATION_TYPE.AttackBack);
                }
                else
                {
                    m_enemyScipt.AnimChange(AnimationImages.ANIMATION_TYPE.Back);
                }
            }
        }
        else if (m_angle > 22.5 && m_angle < 67.5)
        {
            if (m_transform.forward.y < 0)//右下
            {
                //Debug.Log("右下");
                if (m_isAttack)
                {
                    m_enemyScipt.AnimChange(AnimationImages.ANIMATION_TYPE.AttackRight);
                }
                else
                {
                    m_enemyScipt.AnimChange(AnimationImages.ANIMATION_TYPE.Right);
                }
            }
            else//右上
            {
                //Debug.Log("右上");
                if (m_isAttack)
                {
                    m_enemyScipt.AnimChange(AnimationImages.ANIMATION_TYPE.AttackRight);
                }
                else
                {
                    m_enemyScipt.AnimChange(AnimationImages.ANIMATION_TYPE.Right);
                }
            }
        }
        else if (m_angle >= 67.5 && m_angle < 112.5)//右
        {
            //Debug.Log("右");
            if (m_isAttack)
            {
                m_enemyScipt.AnimChange(AnimationImages.ANIMATION_TYPE.AttackRight);
            }
            else
            {
                m_enemyScipt.AnimChange(AnimationImages.ANIMATION_TYPE.Right);
            }
        }
        else if (m_angle >= 157.5 && m_angle < 202.5)//下
        {
            //Debug.Log("下2");
            if (m_isAttack)
            {
                m_enemyScipt.AnimChange(AnimationImages.ANIMATION_TYPE.AttackFront);
            }
            else
            {
                m_enemyScipt.AnimChange(AnimationImages.ANIMATION_TYPE.Front);
            }
        }
        else if (m_angle > 202.5 && m_angle <= 247.5)//左下
        {
            //Debug.Log("左下");
            if (m_isAttack)
            {
                m_enemyScipt.AnimChange(AnimationImages.ANIMATION_TYPE.AttackLeft);
            }
            else
            {
                m_enemyScipt.AnimChange(AnimationImages.ANIMATION_TYPE.Left);
            }
        }
        else if (m_angle >= 247.5 && m_angle < 292.5)//左
        {
            //Debug.Log("左");
            if (m_isAttack)
            {
                m_enemyScipt.AnimChange(AnimationImages.ANIMATION_TYPE.AttackLeft);
            }
            else
            {
                m_enemyScipt.AnimChange(AnimationImages.ANIMATION_TYPE.Left);
            }
        }
        else if (m_angle >= 292 && m_angle < 337)
        {
            if (m_transform.forward.y < 0)//左下
            {
                //Debug.Log("左下");
                if (m_isAttack)
                {
                    m_enemyScipt.AnimChange(AnimationImages.ANIMATION_TYPE.AttackLeft);
                }
                else
                {
                    m_enemyScipt.AnimChange(AnimationImages.ANIMATION_TYPE.Left);
                }
            }
            else//左上
            {
                //Debug.Log("左上");
                if (m_isAttack)
                {
                    m_enemyScipt.AnimChange(AnimationImages.ANIMATION_TYPE.AttackLeft);
                }
                else
                {
                    m_enemyScipt.AnimChange(AnimationImages.ANIMATION_TYPE.Left);
                }
            }
        }

        //アクティブ状態ならアニメーションをする。
        if (m_isAnimation)
        {
            if (!m_isMalfunction)
            {
                if (m_counter >= m_count)
                {
                    m_enemyScipt.EnemyAnimation();
                    m_counter = 0;
                }
                else
                {
                    m_counter += Time.deltaTime;
                }
            }
        }

        //追跡の時、quitRangeより距離が離れたら中止
        if (m_distance > m_quitRange || m_playerController.m_stealth)
        {
            if (m_tracking == true)
            {
                if (effect)
                {
                    effect.EffectStop();
                }
                m_tracking = false;
                m_playerController.enemyCon.Remove(this);
                onPlayer = false;
                m_enemyProjector.GetComponent<CapsuleCollider2D>().isTrigger = false;
            }   
        }

        if (m_tracking || alarm == true)
        {
            //Playerを目標とする
            m_agent.destination = m_targetPos;
        }
        else
        {
            //PlayerがtrackingRangeより近づいたら追跡開始
            if (m_distance < m_trackingRange)
            {
                if (!m_playerController.m_stealth)
                {
                    if (m_tracking == false)
                    {
                        if (effect)
                        {
                            effect.EffectStart();
                        }
                        m_tracking = true;
                        foreach (EnemyController enemyController in m_playerController.enemyCon)
                        {
                            if (enemyController == this)
                            {
                                onPlayer = true;
                            }
                        }
                        if (!onPlayer)
                        {
                            m_playerController.enemyCon.Add(this);
                        }
                    }
                }
            }

            // エージェントが現目標地点に近づいてきたら、
            // 次の目標地点を選択します
            if (!m_agent.pathPending && m_agent.remainingDistance < 0.5f)
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

    /// <summary>
    /// 色変更
    /// </summary>
    /// <param name="color"></param>
    public void Form_Color(ColorInfo.COLOR_TYPE color)
    {
        m_nowColor = color;
        m_enemyScipt.ChangeColorImage(color);
    }

    /// <summary>故障</summary>
    public void Malfunction(bool isMalfunction)
    {
        m_isMalfunction = isMalfunction;
        if (isMalfunction)
        {
            Freeze();
            m_enemyScipt.Malfunction();
        }
        else
        {
            FreezeOff();
        }
    }

    /// <summary>
    /// 動きを停止
    /// </summary>
    public void Freeze()
    {
        if (m_agent.isStopped == true)
        {
            return;
        }
        m_isAnimation = false;
        m_agent.isStopped = true;
    }

    /// <summary>
    /// 停止を解除
    /// </summary>
    public void FreezeOff()
    {
        if (m_agent.isStopped == false)
        {
            return;
        }
        m_isAnimation = true;
        m_agent.isStopped = false;
    }
}
