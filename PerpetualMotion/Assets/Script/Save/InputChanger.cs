using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputChanger
{
    public static bool m_inputPattern = false;
    public enum INPUTKEY_TYPE
    {
        Up,
        Down,
        Left,
        Right,
        UpSelect,
        DownSelect,
        LeftSelect,
        RightSelect,
    }

    public static bool InputInform(INPUTKEY_TYPE inputType)
    {
        bool result = false;
        switch (inputType)
        {
            case INPUTKEY_TYPE.Up:
                if (!m_inputPattern)
                {
                    result = Input.GetButtonDown("w");
                }
                else
                {
                    result = Input.GetButtonDown("UpArrowkey");
                }
                break;
            case INPUTKEY_TYPE.Down:
                if (!m_inputPattern)
                {
                    result = Input.GetButtonDown("s");
                }
                else
                {
                    result = Input.GetButtonDown("DownArrowkey");
                }
                break;
            case INPUTKEY_TYPE.Left:
                if (!m_inputPattern)
                {
                    result = Input.GetButtonDown("a");
                }
                else
                {
                    result = Input.GetButtonDown("LeftArrowkey");
                }
                break;
            case INPUTKEY_TYPE.Right:
                if (!m_inputPattern)
                {
                    result = Input.GetButtonDown("d");
                }
                else
                {
                    result = Input.GetButtonDown("RightArrowkey");
                }
                break;
            case INPUTKEY_TYPE.UpSelect:
                if (!m_inputPattern)
                {
                    result = Input.GetButtonDown("UpArrowkey");
                }
                else
                {
                    result = Input.GetButtonDown("w");
                }
                break;
            case INPUTKEY_TYPE.DownSelect:
                if (!m_inputPattern)
                {
                    result = Input.GetButtonDown("DownArrowkey");
                }
                else
                {
                    result = Input.GetButtonDown("s");
                }
                break;
            case INPUTKEY_TYPE.LeftSelect:
                if (!m_inputPattern)
                {
                    result = Input.GetButtonDown("LeftArrowkey");
                }
                else
                {
                    result = Input.GetButtonDown("a");
                }
                break;
            case INPUTKEY_TYPE.RightSelect:
                if (!m_inputPattern)
                {
                    result = Input.GetButtonDown("RightArrowkey");
                }
                else
                {
                    result = Input.GetButtonDown("d");
                }
                break;
        }
        return result;
    }

    public static float InputAxisRaw(bool isHorizontal)
    {
        float result = 0;
        if (isHorizontal)
        {
            if (!m_inputPattern)
            {
                result = Input.GetAxisRaw("Horizontal");
            }
            else
            {
                result = Input.GetAxisRaw("Horizontal2");
            }
        }
        else
        {
            if (!m_inputPattern)
            {
                result = Input.GetAxisRaw("Vertical");
            }
            else
            {
                result = Input.GetAxisRaw("Vertical2");
            }
        }

        return result;
    }
}
