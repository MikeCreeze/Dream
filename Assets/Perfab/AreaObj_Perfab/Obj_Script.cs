using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*====================================================================
交互类型参考:
1:木箱(推动与否等)
2:泥潭(减速)
3:门-黄
4:草丛(隐藏)

====================================================================*/
public class Obj_Script : MonoBehaviour
{

    public int Obj_type;//可交互物品的类型

    private Entity _Obj = new Entity(false, false);
    void Start()
    {

    }
    private void OnCollisionStay2D(Collision2D collision)       //碰撞器保持
    {
        switch(Obj_type)
        {

            case 1:
            if (collision.gameObject.tag == "Player")
            {

                    if (PlayerInfo.Ins.PowerOn == true)
                    { gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic; }
                    else
                    {
                        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    }
       
            }
              
                break;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)             //侦测器进入
    {
        switch (Obj_type)
        {
        
               
               
            case 2:                                                     //泥潭，百分比减速
                if (collision.gameObject.tag == "Player")
                {

                    PlayerInfo.Ins.Set_Speed(0.4f,true);        

                }
                break;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (Obj_type)
        {
            case 2:                                   
                if (collision.gameObject.tag == "Player")
                {

                    PlayerInfo.Ins.Set_Speed(1f,true);

                }
                break;
        }
    }
}
