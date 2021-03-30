using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfraredSensor : MonoBehaviour
{
    [SerializeField] GameObject enemyObj;
    EnemyController ec;
    void Start()
    {
        ec = enemyObj.GetComponent<EnemyController>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag=="Player")
        {
            ec.alarm = true;
        }
    }
}
