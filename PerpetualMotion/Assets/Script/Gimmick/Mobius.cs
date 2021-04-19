using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mobius : MonoBehaviour
{
    [SerializeField] Sprite[] images = new Sprite[7];
    public GameManager gameManager;
    int processNum = 0;
    Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool FitPiece(int item)
    {
        processNum += item;
        image.sprite = images[processNum];
        Debug.Log(processNum);
        if (images.Length == processNum + 1)
        {
            gameManager.OpenResult(true);
            return true;
        }
        return false;
    }
    
    
}
