using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string SceneName;
    public int clearTime;
    public int killedEnemy;
    public int gimmickCount;
    [SerializeField] GameObject clearResult;
    [SerializeField] GameObject gameOverResult;
    [SerializeField] Text[] textBox;
    public void OpenResult(bool isClear)
    {
        if(isClear)
        {
            clearResult.SetActive(true);
        }
        else
        {
            gameOverResult.SetActive(true);
        }
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
