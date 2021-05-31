using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearedAnimController : MonoBehaviour
{
    SaveManager m_saveManager;
    [SerializeField] public int m_stageNumber;
    [SerializeField] GameObject m_clearedAnim;
    bool m_isClear;
    void Start()
    {
        if (GameObject.FindObjectOfType<SaveManager>())
        {
            m_saveManager = GameObject.FindObjectOfType<SaveManager>();
            m_isClear = m_saveManager.saveData.m_clearedStages[m_stageNumber - 1];
            if (m_isClear)
            {
                m_clearedAnim.SetActive(true);
            }
            else
            {
                m_clearedAnim.SetActive(false);
            }
        }
    }

    
    

}
