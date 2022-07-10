using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPropInWorld : MonoBehaviour
{
    public Prop thisProp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))//这件物品如果碰到Tag是"Player"时触发
        {
            BackPackManager.JoinPropInWorld(thisProp);//加入仓库
            Destroy(this.gameObject);
        }
    }

}
