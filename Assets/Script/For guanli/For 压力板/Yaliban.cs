using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yaliban : MonoBehaviour
{
    // Start is called before the first frame update
    Transform transform;
    void Start()
    {
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Be_ya()
    {
        Debug.Log("压力板按下");

    }
    public void Taiqi()
    {
        Debug.Log("压力板抬起");
    }
}
