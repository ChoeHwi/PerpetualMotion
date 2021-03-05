using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercamera : MonoBehaviour
{
    public GameObject Target = null;
    public Vector3 offset = Vector3.zero;
    /// <summary>カメラと画面の距離</summary>
    public float cameraDistance = -10f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y, cameraDistance) + offset;
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null) return;

        transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y, cameraDistance) + offset;

    }
}
