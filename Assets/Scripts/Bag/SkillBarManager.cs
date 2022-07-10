using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBarManager : MonoBehaviour
{
    public static SkillBarManager instance;
    public BackPack playBackPack;
    public GameObject skillbarParent;
    public List<GameObject> skillsBarSlots = new List<GameObject>();
    public Prop[] skillProp;
    public GameObject EmptySlot;//预制体

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }
    private void OnEnable()
    {
        RefreshSkillBar();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void RefreshSkillBar()
    {
        for (int i = 0; i < instance.skillbarParent.transform.childCount; i++)//清空技能栏
        {
            if (instance.skillbarParent.transform.childCount == 0)
                break;
            Destroy(instance.skillbarParent.transform.GetChild(i).gameObject);
            instance.skillsBarSlots.Clear();

        }
        for (int i = 0; i < instance.playBackPack.skill.Count; i++)//从背包列表重新生成技能栏
        {
            instance.skillsBarSlots.Add(Instantiate(instance.EmptySlot));
            instance.skillsBarSlots[i].transform.SetParent(instance.skillbarParent.transform);
            instance.skillsBarSlots[i].GetComponent<SkillSlot>().SlotPropID = i;
            instance.skillsBarSlots[i].GetComponent<SkillSlot>().SetupSlot(instance.playBackPack.skill[i]);
            instance.skillProp[i] = instance.playBackPack.skill[i];
        }
    }

    public float Get_SkillLong(int i)              //获取技能长度
    {
        return instance.skillProp[i - 1].skill_long;
    }
    public bool GetSkillState(int i)//获取技能栏i的技能状态
    {
        if (!instance.skillProp[i - 1])
        {
            return false;
        }
        else
        {
            return instance.skillProp[i - 1].CanUse;
        }
    }
    public int Get_SkillID(int i)//获取技能栏i的技能id
    {

        return instance.skillProp[i - 1].skill_ID;
    }

    public int Get_Conjure(int i)//获取技能栏i的技能指示器
    {

        return instance.skillProp[i - 1].skill_Conjure;
    }

    public void Success_Skill(int i)//成功使用技能栏i的技能
    {
        instance.skillProp[i - 1].skillsHeldInBag--;
        instance.skillProp[i - 1].skillsHeld--;
        if (instance.skillProp[i - 1].skillsHeldInBag == 0)
        {
            instance.skillProp[i - 1] = null;
            instance.playBackPack.skill[i - 1] = null;
        }
        else
        {
            instance.skillProp[i - 1].CanUse = false;
            StartCoroutine(CoolingCD(i));
        }
        RefreshSkillBar();
    }
    public void Start_CD(int i)//
    {

        //Debug.Log("开始协程");
    }

    IEnumerator CoolingCD(int i)
    {
        float temp = 0;
        GameObject CD_BG = skillsBarSlots[i - 1].GetComponent<SkillSlot>().CD_BG;
        Debug.Log(skillsBarSlots[i - 1].GetComponent<SkillSlot>().CD + "11");
        Text CD = skillsBarSlots[i - 1].GetComponent<SkillSlot>().CD;

        Debug.Log(CD_BG.activeSelf);
        do
        {
            CD_BG = skillsBarSlots[i - 1].GetComponent<SkillSlot>().CD_BG;
            CD = skillsBarSlots[i - 1].GetComponent<SkillSlot>().CD;
            CD_BG.SetActive(true);
            Debug.Log(instance.skillProp[i - 1].skillsCooling_time);
            temp += Time.deltaTime;
            CD.text = ((int)instance.skillProp[i - 1].skillsCooling_time - (int)temp).ToString();
            yield return null;
        }
        while (temp < instance.skillProp[i - 1].skillsCooling_time);

        instance.skillProp[i - 1].CanUse = true;

        CD_BG.SetActive(false);
        Debug.Log(CD_BG.activeSelf);
    }
}
