using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearedAnimController : MonoBehaviour
{
    SaveManager m_saveManager;
    [SerializeField] GameObject[] m_clearedAnim;
    void Start()
    {
        if (GameObject.FindObjectOfType<SaveManager>())
        {
            m_saveManager = GameObject.FindObjectOfType<SaveManager>();
            
            for(int i = 0; i < m_clearedAnim.Length; i++)
            {
                m_clearedAnim[i].SetActive(m_clearedAnim[i]);
            }
        }
    }

    
    

}
