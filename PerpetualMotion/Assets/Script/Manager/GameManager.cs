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
    /// <summary>台座のUI</summary>
    [SerializeField] GameObject mobius;
    Mobius mobiusScript;
    /// <summary> </summary>
    public Slot slot;
    /// <summary> </summary>
    [SerializeField] GameObject itemPrefabBase = null;
    /// <summary> </summary>
    NavMeshHit hit;
    /// <summary> 参照先のクラスの変数 </summary>
    [SerializeField] GameObject director;

    private void Start()
    {
        mobiusScript = mobius.GetComponentInChildren<Mobius>();
        mobiusScript.gameManager = this;
    }

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

    public void FitStart(int item)
    {
        StartCoroutine(FitPiece(item));
    }

    IEnumerator FitPiece(int item)
    {
        mobius.SetActive(true);
        yield return new WaitForSeconds(1f);
        if (mobiusScript.FitPiece(item))
        {
            
        }
        else
        {
            yield return new WaitForSeconds(1f);
            mobius.SetActive(false);
        }
        
    }

    public void OpenResult(bool isClear)
    {
        if(isClear)
        {
            StartCoroutine(ClearResult());
        }
        else
        {
            gameOverResult.SetActive(true);
        }
    }

    IEnumerator ClearResult()
    {
        director.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        clearResult.SetActive(true);
        /*textBox[0].text = clearTime.ToString();
        textBox[1].text = killedEnemy.ToString();
        textBox[2].text = gimmickCount.ToString();*/
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
