using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Flash : MonoBehaviour
{
    public int SkillPlace;
    public Vector2 Destiny;
    // Start is called before the first frame update
    void Start()
    {
        PlayerMove.Ins.CanControl = true;
        PlayerInfo.Ins.SetPosition(Destiny);
        Skill.Instance.ResetSkill(SkillPlace);        //出口
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
