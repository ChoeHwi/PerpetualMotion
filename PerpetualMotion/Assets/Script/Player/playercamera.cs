using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercamera : MonoBehaviour
{
    public GameObject Target = null;
    public Vector3 offset = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = Target.transform.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null) return;

        transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y, Target.transform.position.z) + offset;

    }
}
