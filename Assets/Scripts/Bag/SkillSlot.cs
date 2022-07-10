using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public Prop SlotProp;
    public Image SlotImage;//背包UI里技能的图片
    public Text SkillHeld;
    public GameObject PropInSlot;//
    public int SlotPropID;
    public GameObject CD_BG;
    public Text CD;
 

    public void SetupSlot(Prop prop)
    {
        if (prop == null)
        {
            PropInSlot.SetActive(false);//如果槽位没有物品，则隐藏预制体Slot下的Skill
            return;
        }
        SlotImage.sprite = prop.skillsImage;

        if (SlotPropID < 16)
        {
            SkillHeld.text = prop.skillsHeldInWarehouse.ToString();
        }
        else       
        {
            SkillHeld.text = prop.skillsHeldInBag.ToString();

        }


        SlotProp = prop;
    }



}
