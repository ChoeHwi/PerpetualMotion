using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float m_moveSpeed = 0.02f;
    float m_stopSpeed;
    public Image playerImage;

    // Start is called before the first frame update
    void Start()
    {
        m_stopSpeed = m_moveSpeed;
    }

    // Update is called once per frame

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(-m_moveSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(m_moveSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, m_moveSpeed, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, -m_moveSpeed, 0);
        }

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {

    }
    public void Form_red()
    {
        playerImage.color = Color.red;
    }
    public void Form_green()
    {
        playerImage.color = Color.green;
    }
    public void Form_blue()
    {
        playerImage.color = Color.blue;
    }
    public void mod_craft()
    {
        m_moveSpeed = 0;
    }
    public void mod_mov()
    {
        m_moveSpeed = m_stopSpeed;
    }
}
