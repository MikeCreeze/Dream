using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character          //角色类，除玩家外所有角色
{
    public float Health;
    public float Speed;
    public int Damage;

    public Character()
    {
        Health = 1;
        Speed = 0;
        Damage = 0;
    }
    public Character(float HP, float SP, int DA)
    {
        Health = HP;
        Speed = SP;
        Damage = DA;
    }

}
public class Entity
{
    public bool Ice_Frozen;     //冰封(控制)
    public bool Fire_Hitted;    //着火(伤害&减速)

     public Entity(bool _ice,bool _fire)
    {
        Ice_Frozen = _ice;
        Fire_Hitted = _fire;
    }
}

public class GrobalVariable : MonoBehaviour
{

}
public class GrobalClass : GrobalVariable
{


    public static bool FirstComing = true;

    public static void LoadToScene(int i)
    {
        SceneManager.LoadScene(i);
    }

}



