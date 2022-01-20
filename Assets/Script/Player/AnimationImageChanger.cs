using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationImageChanger : MonoBehaviour
{
    [SerializeField] Sprite[] m_sprits;
    int m_index = 0;
    /// <summary>アニメーションのスピード</summary>
    [SerializeField] float m_count = 0.25f;
    /// <summary></summary>
    float m_counter = 0;
    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void SpriteChange()
    {
        sr.sprite = m_sprits[m_index];
        m_index = (m_index + 1) % m_sprits.Length;
    }

    void Update()
    {
        if (m_counter >= m_count)
        {
            SpriteChange();
            m_counter = 0;
        }
        else
        {
            m_counter += Time.deltaTime;
        }
    }
}
