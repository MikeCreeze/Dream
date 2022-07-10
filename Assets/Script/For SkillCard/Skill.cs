using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public static Skill Instance;
    public int UsingSkill = 0;
    //private int[] UsingSkillState = new int[] {0,0,0,0,0,0,0};//0无用(0结束,1开始施法,2施法中)

    public GameObject Skill_Flash_Perfab;
    public GameObject Skill_Ice_Perfab;
    public GameObject Skill_Power_Perfab;
    public GameObject Skill_Hook_Perfab;

    GameObject curHook = null;
    Vector2 Des;
    int Us_temp;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        
    }


   
    void Update()
    {
        if (ConjureControl.Instance.IfConjure == true)
        {
            if (Input.GetMouseButton(0))
            {
                Accept_Conj();
                UseSkill(SkillBarManager.instance.Get_SkillID(UsingSkill));            //技能种类
            }
            if (Input.GetMouseButton(1))
            {
                Cancel_Conj();
            }
        }
    
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UsingSkill = 1;
            CheckSkill();
         
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UsingSkill = 2;
            CheckSkill();
          

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UsingSkill = 3;
              CheckSkill();
          

        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UsingSkill = 4;
           CheckSkill();
           

        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            UsingSkill = 5;
           CheckSkill();
         

        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            UsingSkill = 6;
           CheckSkill();
         

        }

    }
   private void CheckSkill()        //接收技能按键指令后执行操作
    {
        if (SkillBarManager.instance.GetSkillState(UsingSkill))
        {
            ConjureControl.Instance.Conjure_Type = SkillBarManager.instance.Get_Conjure(UsingSkill);/*BackPackManager.bpMinstance.playBackPack.skill[1].skill_Conjure*/;
            switch (ConjureControl.Instance.Conjure_Type)        //判断指示器是否需要用到变量
            {
                    //线性指示器
                case 1:
                    ConjureControl.Instance.VariableFloat = SkillBarManager.instance.Get_SkillLong(UsingSkill);
                    break;
                    //范围指示器
                case 2:
                    ConjureControl.Instance.VariableFloat = SkillBarManager.instance.Get_SkillLong(UsingSkill);
                    break;

            }
            ConjureControl.Instance.IfConjure = true;
        }
        else
        {
            Debug.Log("冷却或不存在技能！");
        }
    }
   
    public void UseSkill(int i)
    {
        switch(i)
        {
            case 1:
                Skill_Flash_01();
                break;
            case 2:
                Skill_Ice_01();   
                break;
            case 3:
                Skill_Power_01();
                break;
            case 4:
                Skill_Hook_01();
                break;

        }
    }
    //=============闪现===============
    private void Skill_Flash_01()
    {
     
        if(!ConjureControl.Instance.CheckCollsion())
        {

            PlayerMove.Ins.CanControl = false;
            Des = ConjureControl.Instance.Tar_Point.transform.position;
            Us_temp = UsingSkill;
            Invoke("Skill_Flash_02", 0.3f);
        }
        else
        {
         
            Debug.Log("落点在墙内，闪现失败！");
        }
    
    }
    private void Skill_Flash_02()
    {
       GameObject Temp  = Instantiate(Skill_Flash_Perfab, PlayerInfo.Ins.GetPosition());
       
        Temp.GetComponent<Skill_Flash>().Destiny = Des;
        Temp.GetComponent<Skill_Flash>().SkillPlace = Us_temp;
    }
    //================================

    private void Skill_Ice_01()
    {
        PlayerMove.Ins.CanControl = false;
        Invoke("Skill_Ice_02", 0.1f);
    }
    private void Skill_Ice_02()
    {
        GameObject Temp= Instantiate(Skill_Ice_Perfab ,PlayerInfo.Ins.GetPosition());
        Temp.transform.Find("碰撞体").GetComponent<Skill_Ice>().SkillPlace = UsingSkill;
    }
  
    private void Skill_Power_01()
    {
        GameObject Temp = Instantiate(Skill_Power_Perfab, PlayerInfo.Ins.GetPosition());
        Temp.GetComponent<Skill_Power>().SkillPlace = UsingSkill;
    }
       
    private void Skill_Hook_01()
    {
       
        if (!curHook)
        {
            curHook = Instantiate(Skill_Hook_Perfab, gameObject.transform.position, Quaternion.identity);
            curHook.GetComponent<Skill_Hook>().Destiny = ConjureControl.Instance.Tar_Point.transform.position;   //告知Rope脚本鼠标位置，让锚点到达
            curHook.GetComponent<Skill_Hook>().SkillPlace = UsingSkill;
        }
        else
        {
            Debug.Log("场内存在爪钩");
        }

    }
    private void Skill_Hook_02()
    {

    }


    public void Accept_Conj()//确定施法
    {
        ConjureControl.Instance.IfConjure = false;
    }
    public void Cancel_Conj()//取消施法
    {
        ConjureControl.Instance.IfConjure =false;
    }
    public void ResetSkill(int Place)
    {


        SkillBarManager.instance.Success_Skill(Place);
                

        Debug.Log(Place + "技能使用完毕");
       
    }
}
