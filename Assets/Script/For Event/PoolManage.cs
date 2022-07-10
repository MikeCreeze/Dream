/****************************************************
	文件：test1.cs
	作者：空银子
	邮箱: 1184840945@qq.com
	日期：#CreateTime# 	
	功能：Nothing
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Pool : MonoBehaviour
{
    public Stack<object> items;

    public int Count
    {
        get { return items.Count; }
    }

}

public class PoolManage
{
    //将资源池管理器做成单例，以便使用
    static PoolManage instance;
    public static PoolManage Instance
    {
        get
        {
            if (instance==null)
            {
                instance = new PoolManage();
            }
            return instance;
        }   
    }
    PoolManage() { }


    public Dictionary<string, Pool> pools = new Dictionary<string, Pool>();
   
    // 获取游戏物体的函数
    public object GetItemByCreateFun(string sign,Func<object> createFun)
    {
        // 先判断是否存在这个池子
        if (pools.ContainsKey(sign))
        {         
            if (pools[sign].items.Count>0)
            {
                return pools[sign].items.Pop();
            }
            else
            {
                 return createFun();    
            }

        }
        else
        {
            Pool pool = new Pool();
            pool.items = new Stack<object>();              
            pools.Add(sign,pool);
            return createFun();       
        }
      
    }
 
    // 回收游戏物体的函数
    public void Recover(string sign,object item)
    {
        if (!pools.ContainsKey(sign))
        {
            Debug.LogError("并不存在标识为"+sign+"的资源池");
        }
        else
        {
            pools[sign].items.Push(item);
        }
    }
    
    // 获取对应池子的物体数量
    public int Count(string sign)
    {
        if (!pools.ContainsKey(sign))
        {
            Debug.LogError("并不存在标识为" + sign + "的资源池");
            return -1;
        }
        else
        {
            return pools[sign].Count;
        }
      
    }
}
