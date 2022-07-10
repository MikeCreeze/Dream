using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Button : MonoBehaviour
{
    // Start is called before the first frame update
    bool IfAnxia=false;//按钮是否被按下

    public int type;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Down()
    {
        if(IfAnxia==false)
        {
            IfAnxia = true;
            Debug.Log("按钮被按下");
        }
        if(type==1)//可以再次按下的按钮
        {
            Debug.Log("按钮在3秒中后重新可以按");
            //延迟函数
            IfAnxia = false;
        }
        else if(type==2)//不可以再次按下的按钮
        {

        }
        
    }
}
