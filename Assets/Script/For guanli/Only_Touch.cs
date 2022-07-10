using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Only_Touch : MonoBehaviour
{
    // Start is called before the first frame update
    public int type;//可交互物品的类型

    bool If_Player=false;//是否接触到玩家

     GameObject character;//接触的人(要做数组，或者链表)

 

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Touched()//被碰到
    {
    
        if(type==4)//压力板
        {
            Debug.Log("踩到压力板");//好像触发两次了
            this.GetComponent<Yaliban>().Be_ya();
        }
    }
    public void Out()//离开时调用的函数
    {
       if(type==4)
        {
            Debug.Log("离开压力板");//好像触发两次了
            this.GetComponent<Yaliban>().Taiqi();//抬起压力板
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)//出去
    {
        if(collision.tag=="Player"||collision.tag=="enemy")
        {
            If_Player = true;
            character = collision.gameObject;
            Touched();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            If_Player = false;
            Out();
        }
    }
}
