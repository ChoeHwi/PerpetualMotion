using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//オブジェクトにNavMeshAgentコンポーネントを設置
[RequireComponent(typeof(NavMeshAgent))]

public class EnemyController : MonoBehaviour
{
    /// <summary>巡回地点</summary>
    public Transform[] points;
    /// <summary>現在の巡回地のインデックス</summary>
    [SerializeField] int destPoint = 0;
    /// <summary>このオブジェクトのAI</summary>
    private NavMeshAgent agent;
    /// <summary>ターゲットの位置</summary>
    Vector3 targetPos;
    /// <summary>追いかける対象</summary>
    GameObject target;
    /// <summary>ターゲットからの距離</summary>
    float distance;
    /// <summary>この範囲に入ったら追跡</summary>
    [SerializeField] float trackingRange = 3f;
    /// <summary>この範囲から出たら追跡をやめる</summary>
    [SerializeField] float quitRange = 5f;
    /// <summary>追跡状態かどうか</summary>
    [SerializeField] bool tracking = false;

    void Start()
    {

        agent = GetComponent<NavMeshAgent>();

        // autoBraking を無効にすると、目標地点の間を継続的に移動します
        //(つまり、エージェントは目標地点に近づいても
        // 速度をおとしません)
        agent.autoBraking = false;

        GotoNextPoint();

        //追跡したいオブジェクトの名前を入れる
        target = GameObject.Find("Player");
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
        //Playerとこのオブジェクトの距離を測る
        targetPos = target.transform.position;
        distance = Vector3.Distance(this.transform.position, targetPos);


        if (tracking)
        {
            //追跡の時、quitRangeより距離が離れたら中止
            if (distance > quitRange)
                tracking = false;

            //Playerを目標とする
            agent.destination = targetPos;
        }
        else
        {
            //PlayerがtrackingRangeより近づいたら追跡開始
            if (distance < trackingRange)
                tracking = true;

            // エージェントが現目標地点に近づいてきたら、
            // 次の目標地点を選択します
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();
        }
    }

    //void OnDrawGizmosSelected()
    //{
    //    //trackingRangeの範囲を赤いワイヤーフレームで示す
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, trackingRange);

    //    //quitRangeの範囲を青いワイヤーフレームで示す
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(transform.position, quitRange);
    //}
}
