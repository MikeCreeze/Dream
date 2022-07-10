using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private GameObject Parent_Object;
    public int Door_type;//门的类型，1:黄,2:蓝

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    public void Player_Touch()
    {
        Debug.Log("按R交互");//提示语句
    }
    public void An_R()
    {
        switch (Door_type)//门，打开
        {
            case 1:
                if(PlayerInfo.Ins.YellowKey){Debug.Log("芝麻开黄门");Destroy(Parent_Object);}else{Debug.Log("没钥匙，爬");}
                break;

            case 2:
                if (PlayerInfo.Ins.BlueKey) { Debug.Log("芝麻开蓝门"); Destroy(Parent_Object); } else { Debug.Log("没钥匙，爬"); }
                break;
        }
    
    }

}
