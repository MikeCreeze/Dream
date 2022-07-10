using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Power : MonoBehaviour
{
    public int SkillPlace;
    private float PowerTime=3;
    private float runTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        Skill.Instance.ResetSkill(SkillPlace);        //出口
        PlayerInfo.Ins.PowerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        runTime += Time.deltaTime;
        PlayerInfo.Ins.ChangeColor(new Vector4(runTime%1f, 0.5f, 1, 1));
        if(runTime>=PowerTime)
        {
            PlayerInfo.Ins.ChangeColor(new Vector4(1, 1, 1, 1));
            PlayerInfo.Ins.PowerOn = false;
           
            Destroy(gameObject);
        }
    }
}
