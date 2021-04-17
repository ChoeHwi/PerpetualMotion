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

    public void FitPiece()
    {
        processNum++;
        image.sprite = images[processNum];
        if (images.Length == processNum + 1)
        {
            gameManager.OpenResult(true);
        }
    }
    
    
}
