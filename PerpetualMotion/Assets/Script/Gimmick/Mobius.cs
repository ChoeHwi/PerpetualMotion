using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobius : MonoBehaviour
{
    [SerializeField] Sprite[] image = new Sprite[3];
    int processNum = 0;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FitPiece()
    {
        processNum++;
        spriteRenderer.sprite = image[processNum];
        if (processNum + 1 = image.Length)
        {
            
        }
    }
    
    
}
