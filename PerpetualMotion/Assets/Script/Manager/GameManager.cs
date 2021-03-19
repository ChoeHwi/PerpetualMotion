using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string SceneName;
    public int clearTime = 0;
    public int killedEnemy = 0;
    public int gimmickCount = 0;
    [SerializeField] GameObject clearResult;
    [SerializeField] GameObject gameOverResult;
    [SerializeField] Text[] textBox;
    public void OpenResult(bool isClear)
    {
        if(isClear)
        {
            clearResult.SetActive(true);
            textBox[0].text = clearTime.ToString();
            textBox[1].text = killedEnemy.ToString();
            textBox[2].text = gimmickCount.ToString();
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
