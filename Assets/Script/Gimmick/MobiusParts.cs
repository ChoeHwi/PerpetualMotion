using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobiusParts : MonoBehaviour
{
    public Item item;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.icon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
