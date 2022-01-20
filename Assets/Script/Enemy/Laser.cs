using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DEleteThis", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeleteThis()
    {
        Destroy(this.gameObject);
    }

    public void Positioning(Quaternion direction)
    {
        transform.rotation = direction;
    }
}
