using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacked_Obj : MonoBehaviour
{
    private bool IfEnter=false;
    private float runtime=1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
      
        if(IfEnter)
        {

            runtime += Time.deltaTime;


            if(runtime>1)
            {
                runtime = 0;
                PlayerInfo.Ins.Hp--;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
      
        if(!IfEnter&&collision.tag=="Player")
        {
            IfEnter = true;
            PlayerInfo.Ins.Set_Speed(1.2f, true);
            Debug.Log("进入上海区");

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IfEnter = false;
        PlayerInfo.Ins.Set_Speed(1f, true);
        runtime = 1;
    }
}
