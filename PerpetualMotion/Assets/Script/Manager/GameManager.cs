using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public string SceneName;
    public int clearTime = 0;
    public int killedEnemy = 0;
    public int gimmickCount = 0;
    [SerializeField] GameObject clearResult;
    [SerializeField] GameObject gameOverResult;
    [SerializeField] Text[] textBox;
    [Header("このシーンのメビウスの台座")]
    [SerializeField] Mobius mobius;
    public Slot slot;
    [SerializeField] GameObject itemPrefabBase;
    NavMeshHit hit;

    public void GetItem(Item item)
    {
        slot.AddItem(item);
    }

    public void RemoveItem()
    {
        slot.clearSlot();
    }

    public void LostItem(Vector3 position)
    {
        var item = Instantiate(itemPrefabBase, new Vector3(position.x + Random.Range(-3, 3), position.y + Random.Range(-3, 3), 0), new Quaternion(0, 0, 0, 0));
        item.GetComponent<MobiusParts>().item = slot.item;
        slot.clearSlot();
        if (NavMesh.SamplePosition(item.transform.position, out hit, 2.0f, NavMesh.AllAreas))
        {
            item.transform.position = hit.position;
        }
    }

    public void FitPiece()
    {
        mobius.FitPiece();
    }

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
