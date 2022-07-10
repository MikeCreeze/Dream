using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Player_Touch()
    {
        Debug.Log("按F破坏");
    }
    public void Open()
    {
        Debug.Log("被破坏");
    }
}
