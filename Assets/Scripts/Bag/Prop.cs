using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Prop",menuName = "Inventory/Prop")]
public class Prop : ScriptableObject
{
    public string skillsName;//技能名字
    public int skill_ID;//技能id（实际作用）
    public int skill_Conjure;//技能指示器类型
    public float skill_long;//技能长度
    public Sprite skillsImage;//技能图片
    public int skillsWeight;//技能重量

    public int skillsHeld;//技能持有量
    public int skillsHeldInWarehouse;//技能在仓库的持有量
    public int skillsHeldInBag;//技能在背包的持有量

    public float skillsCooling_time;//技能冷却时间
    public bool CanUse;//技能是否立即释放

    [TextArea]
    public string skillsInfo;//技能介绍

}
