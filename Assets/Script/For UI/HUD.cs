using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public GameObject HpObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0;i<= PlayerInfo.Ins.Hp;i++)
        {
            HpObj.GetComponent<RectTransform>().sizeDelta = new Vector2(i * 34, 34);
        }
    }
}
