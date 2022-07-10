using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo Ins;
    public Character _Player = new Character(3, 4, 0);  //血量，速度，攻击
   
    
    
    //============Buff==========
    public float BuffSpeed = 0;         //buff带来的移速
    public float BuffMultSpeed = 1;     //buff带来的指数移速
    public bool PowerOn = false;        //力量buff开关
    public int Hp = 5;
    //==========================   


    //============道具==========
    public bool YellowKey = false;
    public bool BlueKey = false;
    //==========================   
    void Awake()
    {
        Ins = this;
    }

    public float GetSpeed()
    {
        return (_Player.Speed+BuffSpeed)*BuffMultSpeed;
    }
    public Transform GetPosition()
    {
        return gameObject.transform;
    }
    public void SetPosition(Vector2 I)
    {
        gameObject.transform.position=I;
    }
    public void ClearVelocity()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
    public void ChangeColor(Vector4 _color)
    {
        gameObject.GetComponent<SpriteRenderer>().color = _color;
    }


    public void Set_Speed(float speed, bool IfMultiple=true)         //值，是否指数翻倍
    {

        if (IfMultiple)
        {
            
            BuffMultSpeed = speed;
            Debug.Log("玩家翻了" + speed + "倍移速，:目前:"+ (_Player.Speed + BuffSpeed) * BuffMultSpeed);


        }
        else
        {
            BuffSpeed += speed;
            Debug.Log("玩家添加了" + speed + "移速,目前:"+ (_Player.Speed + BuffSpeed) * BuffMultSpeed);
        }
    }

  
}
