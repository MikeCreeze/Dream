using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BackPack", menuName = "Inventory/BackPack")]
public class BackPack : ScriptableObject
{
    public List<Prop> skill = new List<Prop>();//技能
    public int BagBearing;//背包承重
}
