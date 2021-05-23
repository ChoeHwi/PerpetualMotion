using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//オブジェクトにNavMeshAgentコンポーネントを設置
[RequireComponent(typeof(NavMeshAgent))]

public class EnemyController : ColorInfo
{
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
    PlayerController playerController;
    public COLOR_TYPE nowColor = COLOR_TYPE.Blank;
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
        //enemyProjector.transform.forward = transform.forward;
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
            if (agent.destination.y < transform.position.y)
            {
                enemyImage.sprite = SelectColor(nowColor)[4];
            }
            else
            {
                enemyImage.sprite = SelectColor(nowColor)[0];
            }
        }
        else if (angle > 22.5 && angle < 67.5)
        {   
            if (agent.destination.y < transform.position.y)
            {
                enemyImage.sprite = SelectColor(nowColor)[3];
            }
            else
            {
                enemyImage.sprite = SelectColor(nowColor)[1];
            }
        }
        else if (angle >= 67.5 && angle < 112.5)
        {
            enemyImage.sprite = SelectColor(nowColor)[2];
        }
        else if (angle >= 157.5 && angle < 202.5)
        {
            enemyImage.sprite = SelectColor(nowColor)[3];
        }
        else if (angle > 202.5 && angle <= 247.5)
        {
            enemyImage.sprite = SelectColor(nowColor)[5];
        }
        else if (angle >= 247.5 && angle < 292.5)
        {
            enemyImage.sprite = SelectColor(nowColor)[6];
        }
        else if (angle >= 292 && angle < 337)
        {
            if (agent.destination.y < transform.position.y)
            {
                enemyImage.sprite = SelectColor(nowColor)[5];
            }
            else
            {
                enemyImage.sprite = SelectColor(nowColor)[7];
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
                    foreach(EnemyController enemyController in playerController.enemyCon)
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

    public void Form_Color(COLOR_TYPE color)
    {
        nowColor = color;
        enemyImage.sprite = SelectColor(nowColor)[imageIndex];
    }

    public void Freeze()
    {
        agent.isStopped = true;
    }

    public void FreezeOff()
    {
        agent.isStopped = false;
    }
}
