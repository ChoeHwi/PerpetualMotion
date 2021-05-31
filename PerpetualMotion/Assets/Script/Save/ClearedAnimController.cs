using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearedAnimController : MonoBehaviour
{
    [SerializeField] GameObject[] m_clearedAnim;
    void Start()
    {
        for (int i = 0; i < m_clearedAnim.Length; i++)
        {
            m_clearedAnim[i].SetActive(SaveManager.saveData.m_clearedStages[i]);
        }
    }
}
