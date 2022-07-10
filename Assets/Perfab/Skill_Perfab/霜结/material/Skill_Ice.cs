using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Ice : MonoBehaviour
{

    public int SkillPlace;
    public GameObject ParentObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("霜冻:"+collision.name);
    }
    public void DeleteObject()
    {
        PlayerMove.Ins.CanControl = true;
        Skill.Instance.ResetSkill(SkillPlace);        //出口
        Destroy(ParentObj);
    }
}
