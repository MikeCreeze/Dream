using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackPackManager : MonoBehaviour
{
    public static BackPackManager bpMinstance;//背包管理单例

    //左边仓库
    public BackPack WareHouse;//仓库(左)
    public GameObject mallgridParent;//仓库父物体
    public GameObject EmptySlot;//预制体
    public List<GameObject> Mallslots = new List<GameObject>();
    public GameObject SkillInfo;
    public Text SkillInfoText;
   

    //右边背包
    public BackPack playBackPack;//背包(右)
    public GameObject baggridParent;//背包父物体
    public List<GameObject> Bagslots = new List<GameObject>();
    public int BagActualWeight = 0 ;//背包实际重量
    public Text BagActualWeightText;

    //下边技能栏
    public GameObject skillsBar;//技能栏父物体
    public List<GameObject> skillsBarslots = new List<GameObject>();
    public Prop[] playerprops;//技能栏的技能


    private void Awake()
    {
        if (bpMinstance != null)
        {
            Destroy(this);
        }
        bpMinstance = this;
    }
    private void OnEnable()
    {
       
        RefreshGrids();
    }
    // Start is called before the first frame update

    private void Start()
    {
        
    }

    private void Update()
    {

            if (SkillInfo.activeSelf)
        {
            SkillInfo.transform.position = new Vector2(Input.mousePosition.x+95, Input.mousePosition.y-115);
        }
           
        //}
        
    }

    public static void RefreshGrids()//刷新槽位
    {
        for (int i = 0; i < bpMinstance.mallgridParent.transform.childCount; i++)//清空仓库物品
        {
            if (bpMinstance.mallgridParent.transform.childCount == 0)
                break;
            Destroy(bpMinstance.mallgridParent.transform.GetChild(i).gameObject);
            bpMinstance.Mallslots.Clear();//清空slots的列表
        }

        for (int i = 0; i < bpMinstance.WareHouse.skill.Count; i++)//从仓库列表重新生成物品
        {
            bpMinstance.Mallslots.Add(Instantiate(bpMinstance.EmptySlot));
            bpMinstance.Mallslots[i].transform.SetParent(bpMinstance.mallgridParent.transform);
            bpMinstance.Mallslots[i].GetComponent<SkillSlot>().SlotPropID = i;
            bpMinstance.Mallslots[i].GetComponent<SkillSlot>().SetupSlot(bpMinstance.WareHouse.skill[i]);
        }

        for (int i = 0; i < bpMinstance.baggridParent.transform.childCount; i++)//清空背包物品
        {
            if (bpMinstance.baggridParent.transform.childCount == 0)
                break;
            Destroy(bpMinstance.baggridParent.transform.GetChild(i).gameObject);
            bpMinstance.Bagslots.Clear();
        }

        for (int i = 0; i < bpMinstance.playBackPack.skill.Count; i++)//从背包列表重新生成
        {
            bpMinstance.Bagslots.Add(Instantiate(bpMinstance.EmptySlot));
            bpMinstance.Bagslots[i].transform.SetParent(bpMinstance.baggridParent.transform);
            bpMinstance.Bagslots[i].GetComponent<SkillSlot>().SlotPropID = bpMinstance.WareHouse.skill.Count + i;
            bpMinstance.Bagslots[i].GetComponent<SkillSlot>().SetupSlot(bpMinstance.playBackPack.skill[i]);
           
        }

        for (int i = 0; i < bpMinstance.skillsBar.transform.childCount; i++)//清空技能栏
        {
            if (bpMinstance.skillsBar.transform.childCount == 0)
                break;
            Destroy(bpMinstance.skillsBar.transform.GetChild(i).gameObject);
            bpMinstance.skillsBarslots.Clear();
                
        }
        for (int i = 0; i < bpMinstance.playBackPack.skill.Count; i++)//从背包列表重新生成技能栏
        {
            bpMinstance.skillsBarslots.Add(Instantiate(bpMinstance.EmptySlot));
            bpMinstance.skillsBarslots[i].transform.SetParent(bpMinstance.skillsBar.transform);
            bpMinstance.skillsBarslots[i].GetComponent<SkillSlot>().SlotPropID = bpMinstance.WareHouse.skill.Count + bpMinstance.playBackPack.skill.Count + i;
            bpMinstance.skillsBarslots[i].GetComponent<SkillSlot>().SetupSlot(bpMinstance.playBackPack.skill[i]) ;
        }
        bpMinstance.BagActualWeight = 0;
        for (int i = 0; i < bpMinstance.playBackPack.skill.Count; i++)
        {
            bpMinstance.playerprops[i] = bpMinstance.playBackPack.skill[i];
            if (bpMinstance.playBackPack.skill[i] != null)
            {
                //计算背包的实际重量
                bpMinstance.BagActualWeight += (bpMinstance.playBackPack.skill[i].skillsHeldInBag * bpMinstance.playBackPack.skill[i].skillsWeight);
            }
        }
        bpMinstance.BagActualWeightText.text = "背包承重:"+bpMinstance.BagActualWeight.ToString()+"/"+bpMinstance.playBackPack.BagBearing.ToString();
    }

 

    public static void ShowSkillInfo(Prop thisprop)
    {
        
        bpMinstance.SkillInfoText.text = thisprop.skillsInfo;
        bpMinstance.SkillInfo.SetActive(true);
        
    }
    public static void HiddenSkillInfo(Prop thisprop)
    {
        bpMinstance.SkillInfoText.text = "";
        bpMinstance.SkillInfo.SetActive(false);
        //bpMinstance.SkillInfoSwitch = true;
    }

    public static void SortBackPack(BackPack thisBackPack)//给背包里的list排序
    {
        
    }

    public static void JoinPropInWorld(Prop thisProp)//从世界场景中入库
    {
        
        if (!bpMinstance.WareHouse.skill.Contains(thisProp))//如果仓库里没有这件物品
        {
            for (int i = 0; i < bpMinstance.WareHouse.skill.Count; i++)
            {
                if (bpMinstance.WareHouse.skill[i] == null)//将仓库第一个空槽给这个物品
                {
                    bpMinstance.WareHouse.skill[i] = thisProp;
                    thisProp.skillsHeldInWarehouse = 1;
                    thisProp.skillsHeld = thisProp.skillsHeldInWarehouse + thisProp.skillsHeldInBag;
                    RefreshGrids();//刷新
                    break;
                }
                else
                {
                    Debug.Log("槽位已满");
                }
            }
        }
        else    //仓库已有这件物品
        {
            thisProp.skillsHeldInWarehouse += 1;//技能在仓库量+1
            thisProp.skillsHeld = thisProp.skillsHeldInWarehouse + thisProp.skillsHeldInBag;
            
        }
        

        RefreshGrids();
    }

    public static void PutOutPropToBag(Prop thisProp,int slotID)//出库到背包
    {
        
        if ((bpMinstance.BagActualWeight + thisProp.skillsWeight) <= bpMinstance.playBackPack.BagBearing)//如果加到背包里没有超重
        {
            if (bpMinstance.playBackPack.skill.Contains(thisProp))//如果背包里有这件物品
            {
                thisProp.skillsHeldInWarehouse -= 1;
                thisProp.skillsHeldInBag += 1;

            }
            else
            {
                for (int i = 0; i < bpMinstance.playBackPack.skill.Count; i++)
                {
                    if (bpMinstance.playBackPack.skill[i] == null)//表示有空槽
                    {
                        thisProp.skillsHeldInWarehouse -= 1;
                        thisProp.skillsHeldInBag = 1;
                        bpMinstance.playBackPack.skill[i] = thisProp;
                        break;
                    }
                }
                
            }
            if (thisProp.skillsHeldInWarehouse <= 0)//表示本来也只有一张，报备后仓库没有了
            {
                thisProp.skillsHeldInWarehouse = 0;
                bpMinstance.WareHouse.skill[slotID] = null;
            }
        }
        else  //加入超重
        {
            
            Debug.Log("超重");
        }

       
        RefreshGrids();
    }

    public static void RecyclingPropInBag(Prop thisProp,int slotID)//从背包回收回仓库
    {
        
        if (bpMinstance.WareHouse.skill.Contains(thisProp))//如果仓库里有这件物品
        {
            thisProp.skillsHeldInWarehouse += 1;
            thisProp.skillsHeldInBag -= 1;
        }
        else  //没有
        {
            for (int i = 0; i < bpMinstance.WareHouse.skill.Count; i++)
            {
                if (bpMinstance.WareHouse.skill[i] == null)//仓库有空槽
                {
                    thisProp.skillsHeldInWarehouse = 1;
                    thisProp.skillsHeldInBag -= 1;
                    bpMinstance.WareHouse.skill[i] = thisProp;
                    break;
                }
            }
        }

        if (thisProp.skillsHeldInBag <= 0)//表示背包现在没有该物品
        {
            thisProp.skillsHeldInBag = 0;
            bpMinstance.playBackPack.skill[slotID - bpMinstance.WareHouse.skill.Count] = null; 
        }
    
        RefreshGrids();
    }

    //public float Get_SkillLong(int i)              //获取技能长度
    //{
    //    return bpMinstance.playerprops[i - 1].skill_long;
    //}
    //public bool GetSkillState(int i)//获取技能栏i的技能状态
    //{
    //    if (!bpMinstance.playerprops[i - 1])
    //    {
    //        return false;
    //    }
    //    else
    //    {
    //        return bpMinstance.playerprops[i - 1].CanUse;
    //    }
    //}
    //public int Get_SkillID(int i)//获取技能栏i的技能id
    //{

    //    return bpMinstance.playerprops[i - 1].skill_ID;
    //}

    //public int Get_Conjure(int i)//获取技能栏i的技能指示器
    //{

    //    return bpMinstance.playerprops[i - 1].skill_Conjure;
    //}

    //public void Success_Skill(int i)//成功使用技能栏i的技能
    //{
    //    bpMinstance.playerprops[i - 1].skillsHeldInBag--;
    //    bpMinstance.playerprops[i - 1].skillsHeld--;
    //    if (bpMinstance.playerprops[i - 1].skillsHeldInBag == 0)
    //    {
    //        bpMinstance.playerprops[i - 1] = null;
    //        bpMinstance.playBackPack.skill[i - 1] = null;
    //    }
    //    else 
    //    {
    //        bpMinstance.playerprops[i - 1].CanUse = false;
    //        StartCoroutine(CoolingCD(i));
    //    }
    //    RefreshGrids();
    //}
    //public void Start_CD(int i)//
    //{
       
    //    //Debug.Log("开始协程");
    //}

    //IEnumerator CoolingCD(int i)
    //{
    //    float temp = 0;
    //   GameObject CD_BG = skillsBarslots[i - 1].GetComponent<SkillSlot>().CD_BG;
    //    Debug.Log(skillsBarslots[i - 1].GetComponent<SkillSlot>().CD+"11");
    //    Text CD = skillsBarslots[i - 1].GetComponent<SkillSlot>().CD;
        
    //    Debug.Log(CD_BG.activeSelf);
    //    do
    //    {
    //        CD_BG = skillsBarslots[i - 1].GetComponent<SkillSlot>().CD_BG;
    //        CD = skillsBarslots[i - 1].GetComponent<SkillSlot>().CD;
    //        CD_BG.SetActive(true);
    //        Debug.Log(bpMinstance.playerprops[i - 1].skillsCooling_time);
    //        temp+=Time.deltaTime;
    //        CD.text = ((int)bpMinstance.playerprops[i - 1].skillsCooling_time-(int)temp).ToString();
    //        yield return null;
    //    }
    //    while (temp < bpMinstance.playerprops[i - 1].skillsCooling_time);
    
    //    bpMinstance.playerprops[i - 1].CanUse = true;

    //    CD_BG.SetActive(false);
    //    Debug.Log(CD_BG.activeSelf);
    //}

}
