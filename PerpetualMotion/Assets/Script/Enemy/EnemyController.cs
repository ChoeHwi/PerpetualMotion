using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//オブジェクトにNavMeshAgentコンポーネントを設置
[RequireComponent(typeof(NavMeshAgent))]

public class EnemyController : MonoBehaviour
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
    bool tracking = false;
    /// <summary>このオブジェクトの見た目の位置</summary>
    [SerializeField] GameObject enemyProjector;
    PlayerController playerController;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

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
        enemyProjector.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);

        //Playerとこのオブジェクトの距離を測る
        targetPos = target.transform.position;
        distance = Vector3.Distance(enemyProjector.transform.position, targetPos);

        if (tracking)
        {
            //追跡の時、quitRangeより距離が離れたら中止
            if (distance > quitRange || playerController.stealth)
            {
                tracking = false;
            }

            //Playerを目標とする
            agent.destination = targetPos;
        }
        else
        {
            //PlayerがtrackingRangeより近づいたら追跡開始
            if (distance < trackingRange)
            {
                if (!playerController.stealth)
                {
                    tracking = true;
                }
            }

            // エージェントが現目標地点に近づいてきたら、
            // 次の目標地点を選択します
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();
        }
    }

    void OnDrawGizmosSelected()
    {
        //trackingRangeの範囲を赤いワイヤーフレームで示す
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemyProjector.transform.position, trackingRange);

        //quitRangeの範囲を青いワイヤーフレームで示す
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(enemyProjector.transform.position, quitRange);
    }
}
