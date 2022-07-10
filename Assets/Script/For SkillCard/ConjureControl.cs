using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConjureControl : MonoBehaviour
{
    public static ConjureControl Instance;  
    public bool IfConjure = false;  //是否开始施放(选中了技能)
    public int Conjure_Type = 0;    //施放方式是哪一种

    public float VariableFloat;     //整型变量（供外部赋值用）

    public GameObject Tar_Point;         //引导头
    LineRenderer LineRen;   //引导线

    public GameObject Tar_Circle;        //引导圈

    public GameObject Player_Object;
    Vector2 Mouse_Destiny;
    // Start is called before the first frame update
    
    void Awake()
    {
        Instance = this;
      
        LineRen =Player_Object.GetComponent<LineRenderer>();  //声明引导线
        LineRen.startWidth = 0.05f;      //引导线起点与终点线宽
        LineRen.endWidth = 0.1f;
        LineRen.positionCount = 2;      //引导线端点数
    }

    // Update is called once per frame
    void Update()
    {
        Mouse_Destiny = Camera.main.ScreenToWorldPoint(Input.mousePosition);
     

        if (IfConjure)
        {
            switch (Conjure_Type)
            {
                case 0:
                   
                    break;
                case 1:
                    Guide_Line();         //线性指示器
                    break;

                case 2:
                    Guide_Circle(VariableFloat);         //线性指示器
                    break;
            }

        }
        else
        {
            ResetAll_UI();
        }
    }
    private void Guide_Line()
    {
        //引导线
        if (Tar_Point.GetComponent<Image>().enabled==true && LineRen.enabled == true)
        {
         
           

                Tar_Point.transform.position = Vector3.MoveTowards(Player_Object.transform.position, Mouse_Destiny, VariableFloat);
            
            LineRen.SetPosition(0, Player_Object.transform.position);
            LineRen.SetPosition(1, Tar_Point.transform.position);

        }
        else
        {
            Tar_Point.GetComponent<Image>().enabled = true;
            LineRen.enabled = true;
        }
    }
    private void Guide_Circle(float Radius)
   {
        Tar_Circle.transform.localScale = new Vector2(Radius, Radius / 2f);
        Debug.Log(Radius);
        Tar_Circle.GetComponent<Image>().enabled = true;
       
    }



    private void ResetAll_UI()
    {
        Tar_Point.GetComponent<Image>().enabled = false;
        Player_Object.GetComponent<LineRenderer>().enabled = false;
        Tar_Circle.GetComponent<Image>().enabled = false;
    }
    public bool CheckCollsion()     //判断鼠标点击位置是否存在碰撞体
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            if(hit.collider.gameObject.tag == "Only_Touch")
            {
                Debug.Log("仅侦测器,无东西");
                return false;
            }
            Debug.Log(hit.collider.name);
            return true;
           
        }
        else
        {
            Debug.Log("无东西");
            return false;
        }
    }
}
