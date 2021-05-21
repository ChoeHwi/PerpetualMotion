using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 敵を足止めするトラップオブジェクトのクラス </summary>
public class ElectricTrap : MonoBehaviour
{
    bool actuation = false;
    EnemyController enemyController;
    /// <summary> animatorの変数 </summary>
    Animator anim = null;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Actuation()
    {
        if(actuation)
        {
            anim.SetBool("Trap", false);
            actuation = false;
            if (enemyController)
            {
                enemyController.FreezeOff();
                enemyController = null;
            }
        }
        else
        {
            anim.SetBool("Trap", true);
            actuation = true;
            if (enemyController)
            {
                enemyController.Freeze();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.gameObject.tag == "Enemy")
        {
            enemyController = collision.gameObject.GetComponent<EnemyColliderController>().enemyController;
            if (actuation)
            {
                enemyController.Freeze();
            }
        }
    }
}
