using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    /// <summary>シーンの名前 </summary>
    public string SceneName;
    /// <summary>クリアの時間 </summary>
    public int clearTime = 0;
    /// <summary> </summary>
    public int killedEnemy = 0;
    /// <summary> </summary>
    public int gimmickCount = 0;
    /// <summary> </summary>
    [SerializeField] GameObject clearResult = null;
    /// <summary> </summary>
    [SerializeField] GameObject gameOverResult = null;
    /// <summary> </summary>
    [SerializeField] Text[] textBox;
    [Header("このシーンのメビウスの台座")]
    [SerializeField] Mobius mobius;
    /// <summary> </summary>
    public Item item;
    public Slot slot;
    /// <summary> </summary>
    [SerializeField] GameObject itemPrefabBase = null;
    /// <summary> </summary>
    NavMeshHit hit;
    /// <summary> 参照先のクラスの変数 </summary>
    TimeLineManager timeLineManager;
    [SerializeField] GameObject director;

    private void Start()
    {
        mobius.gameManager = this;
        timeLineManager = director.GetComponent<TimeLineManager>();
    }

    public void GetItem()
    {
    }

    public void RemoveItem()
    {
        //slot.clearSlot();
    }

    public void LostItem(Vector3 position)
    {
        var item = Instantiate(itemPrefabBase, new Vector3(position.x + Random.Range(-3, 3), position.y + Random.Range(-3, 3), 0), new Quaternion(0, 0, 0, 0));
        //item.GetComponent<MobiusParts>().item = slot.item;
        //slot.clearSlot();
        if (NavMesh.SamplePosition(item.transform.position, out hit, 2.0f, NavMesh.AllAreas))
        {
            item.transform.position = hit.position;
        }
    }

    public void FitPiece(int item)
    {
        mobius.FitPiece(item);
    }

    public void OpenResult(bool isClear)
    {
        if(isClear)
        {
            director.SetActive(true);
            
            timeLineManager.StartTimeLine();

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
