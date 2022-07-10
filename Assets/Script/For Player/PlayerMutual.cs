using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMutual : MonoBehaviour
{
    public bool Trigger_On = false;    //是否碰到可交互物体
    public GameObject Selected_Object; //被选中的物体

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Trigger_Check();
    }
    private void Trigger_Check()//检查是否有交互
    {
        
        if (Input.GetKeyDown(KeyCode.R) && Trigger_On)//按下R时而且触碰到可交互物体
        {
            Debug.Log("已交互");
            Selected_Object.GetComponent<Door>().An_R();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Door")//如果是碰到的可交互物体
        {
            Trigger_On = true;
            Selected_Object = collision.gameObject;
            Selected_Object.GetComponent<Door>().Player_Touch();//调用Door提示语句

        }

    }
    private void OnTriggerExit2D(Collider2D collision)//离开可交互物体
    {
        if (collision.tag == "Door")
        {
            Trigger_On = false;
            Selected_Object = null;
        }

    }

}
