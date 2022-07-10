using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PropInDrag : MonoBehaviour , IBeginDragHandler, IDragHandler, IEndDragHandler,IPointerEnterHandler,IPointerExitHandler
{
    public BackPack warehouse;//仓库
    public BackPack playerBag;//背包
    public Prop currentClickProp;//当前点击的技能
    public int slotID;//当前点击的技能的槽位id


    //拖动
    public Transform originalParent;
    public Prop currentDradProp;
    private int currentPropId;

    //悬停
    public Prop HoverProp;


    private void Start()
    {
       
    }
    private void Update()
    {
    
    }


    public void BtnClick()
    {
        currentClickProp = transform.parent.GetComponent<SkillSlot>().SlotProp;//当前点击的物品
        slotID = transform.parent.GetComponent<SkillSlot>().SlotPropID;

        if (transform.parent.gameObject.GetComponent<SkillSlot>().SlotPropID < 16)//表示点击的块是左边仓库
        {
            BackPackManager.PutOutPropToBag(currentClickProp, slotID);
        }




        if (transform.parent.gameObject.GetComponent<SkillSlot>().SlotPropID >= 16 && transform.parent.gameObject.GetComponent<SkillSlot>().SlotPropID < 22)//表示点击的是右边背包
        {
            BackPackManager.RecyclingPropInBag(currentClickProp, slotID);
        }

        if (transform.parent.gameObject.GetComponent<SkillSlot>().SlotPropID >= 22)//表示点击的是技能栏
        {
            Debug.Log("点击的是技能栏"+ (slotID - 21));
        }
    }



    //开始拖动要做的事
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        currentPropId = originalParent.gameObject.GetComponent<SkillSlot>().SlotPropID;
        currentDradProp = originalParent.gameObject.GetComponent<SkillSlot>().SlotProp;
        transform.SetParent(transform.parent.parent.parent.parent);
        transform.localPosition = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    //按下并拖拽时 对象每帧响应一次此事件
    public void OnDrag(PointerEventData eventData)
    {
        transform.localPosition = eventData.position;
        Debug.Log(eventData.position);

    }

    //结束拖动应该做的事
    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.name != null)//表示在canvas层上
        {


            if (eventData.pointerCurrentRaycast.gameObject.name == "Slot(Clone)")////槽位里没有物品
            {
                var emptySlotID = eventData.pointerCurrentRaycast.gameObject.GetComponent<SkillSlot>().SlotPropID;
                if (currentPropId < 16)
                {
                    if (emptySlotID >= 16 && emptySlotID < 22)//出库
                    {
                        if (currentDradProp.skillsWeight + BackPackManager.bpMinstance.BagActualWeight <= BackPackManager.bpMinstance.playBackPack.BagBearing)
                        {
                            while (currentDradProp.skillsWeight + BackPackManager.bpMinstance.BagActualWeight <= BackPackManager.bpMinstance.playBackPack.BagBearing)
                            {
                                BackPackManager.PutOutPropToBag(currentDradProp, currentPropId);
                                if (currentDradProp.skillsHeldInWarehouse <= 0)
                                {
                                   
                                    break;
                                }
                            }

                        }
                        else
                        {
                            BackPackManager.RefreshGrids();
                        }

                    }
                    else
                    {
                        BackPackManager.RefreshGrids();
                    }
                }
               
                if (currentPropId >= 16 && currentPropId < 22)//入库
                {
                    if (emptySlotID < 16)
                    {
                        if (currentDradProp.skillsHeldInBag > 0)
                        {
                            while (currentDradProp.skillsHeldInBag > 0)
                            {
                                BackPackManager.RecyclingPropInBag(currentDradProp, currentPropId);
                            }
                        }
                        else
                        {
                            BackPackManager.RefreshGrids();
                        }
                        
                    }
                    else
                    {
                        BackPackManager.RefreshGrids();
                    }
                }
                

            }
            else if (eventData.pointerCurrentRaycast.gameObject.name == "skillImage")//槽位里有物品
            {

                var emptySlotID = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<SkillSlot>().SlotPropID;
                if (currentPropId < 16)
                {
                    if (emptySlotID >= 16 && emptySlotID < 22)
                    {
                        if (currentDradProp.skillsWeight + BackPackManager.bpMinstance.BagActualWeight <= BackPackManager.bpMinstance.playBackPack.BagBearing)
                        {
                            while (currentDradProp.skillsWeight + BackPackManager.bpMinstance.BagActualWeight <= BackPackManager.bpMinstance.playBackPack.BagBearing)
                            {
                                BackPackManager.PutOutPropToBag(currentDradProp, currentPropId);
                                if (currentDradProp.skillsHeldInWarehouse <= 0)
                                {
                                    
                                    break;
                                }
                            }
                        }
                        else
                        {
                            BackPackManager.RefreshGrids();
                        }
                    }
                    else
                    {
                      BackPackManager.RefreshGrids();
                    }
                }
               
                if (currentPropId >= 16 && currentPropId < 22)
                {
                    if (emptySlotID < 16)
                    {
                        if (currentDradProp.skillsHeldInBag > 0)
                        {
                            while (currentDradProp.skillsHeldInBag > 0)
                            {
                                BackPackManager.RecyclingPropInBag(currentDradProp, currentPropId);
                            }
                        }
                        else
                        {
                            BackPackManager.RefreshGrids();
                        }
                    }
                    else
                    {
                        BackPackManager.RefreshGrids();
                    }
                }
              
            }
            else
            {
                BackPackManager.RefreshGrids();
            }


        }

        GetComponent<CanvasGroup>().blocksRaycasts = true;
        Destroy(this.gameObject);
        
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.name == "skillImage" || eventData.pointerCurrentRaycast.gameObject.name == "Held")
        {
            HoverProp = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<SkillSlot>().SlotProp;
            var id = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<SkillSlot>().SlotPropID;
            
            if (id < 16)
            {
                BackPackManager.ShowSkillInfo(HoverProp);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        BackPackManager.HiddenSkillInfo(HoverProp);
    }
}
