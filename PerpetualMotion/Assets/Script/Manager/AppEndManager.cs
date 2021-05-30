using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> アプリケーションを終了させるクラス </summary>
public class AppEndManager : SingletonMonoBehaviour<AppEndManager>
{

    /// <summary> ゲーム終了のダイアログのキャンバス </summary>
    [Header("ゲーム終了のダイアログのキャンバス")]
    [SerializeField] Canvas m_endCanvas = default(Canvas);

    void Start()
    {
        m_endCanvas.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            // ダイアログの表示
            m_endCanvas.enabled = true;
        }
    }

    /// <summary>
    /// 終了ボタン
    /// </summary>
    [System.Obsolete]
    private void OnApplicationQuit()
    {
        if (m_endCanvas.enabled == false)
        {
            // ダイアログが開いていなければ終了処理はキャンセル
            Application.CancelQuit();
        }
    }

    /// <summary>
    /// 終了
    /// </summary>
    public void OnCallExit()
    {
        Application.Quit();
    }

    /// <summary>
    /// キャンセル
    /// </summary>
    public void OnCallCancel()
    {
        m_endCanvas.enabled = false;
    }
}
