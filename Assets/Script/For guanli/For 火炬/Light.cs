using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    // Start is called before the first frame update
    bool light = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Be_lighted()
    {
        if(light==false)
        {
            light = true;
            Debug.Log("被点亮");
        }
        else
        {
            light = false;
            Debug.Log("被熄灭");
        }
       
    }
}
