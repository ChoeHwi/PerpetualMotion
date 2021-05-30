using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 敵を足止めするトラップオブジェクトのクラス </summary>
public class ElectricTrap : MonoBehaviour
{
    bool actuation = false;
    List<EnemyController> enemyControllers = new List<EnemyController>(0);
    /// <summary> animatorの変数 </summary>
    Animator anim = null;
    /// <summary> AudioManagerを参照する変数 </summary>
    AudioManager audioManager;

    private void Start()
    {
        anim = GetComponent<Animator>();

        if (GameObject.FindObjectOfType<AudioManager>())
        {
            audioManager = GameObject.FindObjectOfType<AudioManager>();
        }
    }

    public void Actuation()
    {
        if (actuation)
        {
            actuation = false;
            if (enemyControllers.Count > 0)
            {
                foreach (EnemyController enemyController in enemyControllers)
                {
                    enemyController.Malfunction(false);
                }
            }
        }
        else
        {
            actuation = true;
            StartCoroutine(ElecAnim());
            if (enemyControllers.Count > 0)
            {
                foreach (EnemyController enemyController in enemyControllers)
                {
                    enemyController.Malfunction(true);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.gameObject.tag == "Enemy")
        {
            enemyControllers.Add(collision.gameObject.GetComponent<EnemyColliderController>().enemyController);
            if (actuation)
            {
                collision.gameObject.GetComponent<EnemyColliderController>().enemyController.Malfunction(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (enemyControllers.Count > 0)
            {
                for (int i = 0; i < enemyControllers.Count; i++)
                {
                    if (enemyControllers[i] == collision.gameObject.GetComponent<EnemyColliderController>().enemyController)
                    {
                        enemyControllers.RemoveAt(i);
                    }
                }
            }
        }
    }

    /// <summary>
    /// トラップのアニメーションのコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator ElecAnim()
    {
        anim.SetBool("Trap", true);
        //電流が流れている時
        if (audioManager)
        {
            audioManager.PlaySE(audioManager.audioClips[3]);
        }
        yield return new WaitForSeconds(4f);
        anim.SetBool("Trap", false);
        //スイッチOFFのSE
        if (audioManager)
        {
            audioManager.PlaySE(audioManager.audioClips[2]);
        }
        Actuation();
    }
}
