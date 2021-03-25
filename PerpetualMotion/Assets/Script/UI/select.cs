using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class select : MonoBehaviour
{
    [SerializeField] private GameObject selectStage_1;
    [SerializeField] private GameObject selectStage_2;

    int StageNum;
    [SerializeField] int pageNum;
    [SerializeField] int maxPage;
    [SerializeField] Text nameText;
    
    [SerializeField] GameObject playLog;
    [SerializeField] GameObject clearStamp;
    bool clearBool = false;

    [SerializeField] Text infText;
    [SerializeField] int timeInt;
    [SerializeField] int pitInt;
    [SerializeField] int objInt;
    void Start()
    {
        playLog.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PlayLog();
        SceneSelect();
    }
    public void PlayLog()
    {
        if (clearBool == true)
        {
            clearStamp.SetActive(true);
        }
        else
        {
            clearStamp.SetActive(false);
        }
        switch (StageNum)
        {
            case 1:
                infText.text = "Time : " + timeInt + "\n落とし穴で\n敵を落とした数 : " + pitInt + "\nオブジェクトとの\n融合数 : " + objInt;
                break;
            case 2:
                infText.text = "Time : " + timeInt + "\n落とし穴で\n敵を落とした数 : " + pitInt + "\nオブジェクトとの\n融合数 : " + objInt;
                break;
            case 3:
                infText.text = "Time : " + timeInt + "\n落とし穴で\n敵を落とした数 : " + pitInt + "\nオブジェクトとの\n融合数 : " + objInt;
                break;
            case 4:
                infText.text = "Time : " + timeInt + "\n落とし穴で\n敵を落とした数 : " + pitInt + "\nオブジェクトとの\n融合数 : " + objInt;
                break;
            case 5:
                infText.text = "Time : " + timeInt + "\n落とし穴で\n敵を落とした数 : " + pitInt + "\nオブジェクトとの\n融合数 : " + objInt;
                break;
            case 6:
                infText.text = "Time : " + timeInt + "\n落とし穴で\n敵を落とした数 : " + pitInt + "\nオブジェクトとの\n融合数 : " + objInt;
                break;
            case 7:
                infText.text = "Time : " + timeInt + "\n落とし穴で\n敵を落とした数 : " + pitInt + "\nオブジェクトとの\n融合数 : " + objInt;
                break;
            case 8:
                infText.text = "Time : " + timeInt + "\n落とし穴で\n敵を落とした数 : " + pitInt + "\nオブジェクトとの\n融合数 : " + objInt;
                break;
            case 9:
                infText.text = "Time : " + timeInt + "\n落とし穴で\n敵を落とした数 : " + pitInt + "\nオブジェクトとの\n融合数 : " + objInt;
                break;
            case 10:
                infText.text = "Time : " + timeInt + "\n落とし穴で\n敵を落とした数 : " + pitInt + "\nオブジェクトとの\n融合数 : " + objInt;
                break;
            case 11:
                infText.text = "Time : " + timeInt + "\n落とし穴で\n敵を落とした数 : " + pitInt + "\nオブジェクトとの\n融合数 : " + objInt;
                break;
            case 12:
                infText.text = "Time : " + timeInt + "\n落とし穴で\n敵を落とした数 : " + pitInt + "\nオブジェクトとの\n融合数 : " + objInt;
                break;
            case 13:
                infText.text = "Time : " + timeInt + "\n落とし穴で\n敵を落とした数 : " + pitInt + "\nオブジェクトとの\n融合数 : " + objInt;
                break;
            case 14:
                infText.text = "Time : " + timeInt + "\n落とし穴で\n敵を落とした数 : " + pitInt + "\nオブジェクトとの\n融合数 : " + objInt;
                break;
            case 15:
                infText.text = "Time : " + timeInt + "\n落とし穴で\n敵を落とした数 : " + pitInt + "\nオブジェクトとの\n融合数 : " + objInt;
                break;
            case 16:
                infText.text = "Time : " + timeInt + "\n落とし穴で\n敵を落とした数 : " + pitInt + "\nオブジェクトとの\n融合数 : " + objInt;
                break;
            case 17:
                infText.text = "Time : " + timeInt + "\n落とし穴で\n敵を落とした数 : " + pitInt + "\nオブジェクトとの\n融合数 : " + objInt;
                break;
            case 18:
                infText.text = "Time : " + timeInt + "\n落とし穴で\n敵を落とした数 : " + pitInt + "\nオブジェクトとの\n融合数 : " + objInt;
                break;
            default:
                break;
        }
    }
    public void RoadScene()
    {
        SceneManager.LoadScene("PlotScene(otuki)");
    }
    public void SceneSelect()
    {
        switch (pageNum)
        {
            case 1:
                selectStage_1.SetActive(true);
                selectStage_2.SetActive(false);
                break;
            case 2:
                selectStage_1.SetActive(false);
                selectStage_2.SetActive(true);
                break;
            default:
                break;
        }
    }
    public void NextPage()
    {
        if (pageNum < maxPage)
        {
            pageNum++;
        }
    }
    public void BackPage()
    {
        if (pageNum > 1)
        {
            pageNum--;
        }
    }
    public void Push_Button(int number)
    {
        playLog.SetActive(true);
        StageNum = number;
        nameText.text = "Stage " + number;
    }
}
