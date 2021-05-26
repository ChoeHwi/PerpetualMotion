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
        if(actuation)
        {   
            actuation = false;
            if (enemyController)
            {
                enemyController.FreezeOff();
                enemyController = null;
            }
        }
        else
        {
            StartCoroutine(ElecAnim());
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

    /// <summary>
    /// トラップのアニメーションのコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator ElecAnim() 
    {
        anim.SetBool("Trap", true);
        yield return new WaitForSeconds(3f);
        anim.SetBool("Trap", false);
        //スイッチOFFのSE
        audioManager.PlaySE(audioManager.audioClips[1]);
        yield break;
    }
}
