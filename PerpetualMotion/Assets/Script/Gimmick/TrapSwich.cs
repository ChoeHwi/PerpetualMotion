using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> トラップを作動させるクラス </summary>
public class TrapSwich : MonoBehaviour
{
    [Header("対応する電流トラップ")]
    [SerializeField] ElectricTrap[] electricTrap;
    /// <summary> AudioManagerを参照する変数 </summary>
    AudioManager audioManager;

    private void Start()
    {
        if (GameObject.FindObjectOfType<AudioManager>())
        {
            audioManager = GameObject.FindObjectOfType<AudioManager>();
        }
    }

    public void TrapActuation()
    {
        foreach(ElectricTrap script in electricTrap)
        {
            script.Actuation();
        }
    }

    /// <summary>
    /// プレイヤーがスイッチを踏んだ時鳴る
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (audioManager)
            {
                //スイッチONのSE
                audioManager.PlaySE(audioManager.audioClips[0]);
            }   
        }
    }
}
