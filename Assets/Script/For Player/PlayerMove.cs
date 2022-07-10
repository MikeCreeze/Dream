using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public static PlayerMove Ins;
    public bool CanControl;         //是否可控  
    private float Max_Speed = 8f;            //玩家最大速度限制(限制玩家因为各种情况导致速度过快)
    private Rigidbody2D Pla_Rigidbody;//自身刚体

    private SpriteRenderer SelfColor;

    private Animator Pla_Ani;

    [Header("位置")]
    public float inputX, inputY;

   
    bool IfCantSee = false;               //是否隐形
    
    void Awake()
    {
        Ins = this;
        Pla_Rigidbody = GetComponent<Rigidbody2D>();
        SelfColor = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanControl)
        {
            GetXY_Input();
        }
        else
        {
            inputX = 0;
            inputY = 0;
        }
        PlayerState();          //玩家渲染状态
    }
    private void FixedUpdate()
    {
        PlayerSpeedFix();
        Player_Move();
    }
    private void GetXY_Input()      //获取移动
    {
        if(Input.GetKey(KeyCode.W))
        {
            inputY = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputY = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputX = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputX = 1;
        }

        if (Input.GetKeyUp(KeyCode.W)|| Input.GetKeyUp(KeyCode.S))
        {
            inputY = 0;
        }
      
        if (Input.GetKeyUp(KeyCode.A)|| Input.GetKeyUp(KeyCode.D))
        {
            inputX = 0;
        }
       

    }
    private void PlayerState()      //状态侦测，动画判断，仅作渲染
    {
        if (inputX > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;

        }
        else if (inputX < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;

        }

    }
    private void Player_Move()      //玩家移动
    {
 
        if (inputX != 0 || inputY != 0)
        {
            Vector3 input = new Vector3(inputX, inputY).normalized;//向量标准化
            Pla_Rigidbody.velocity = input * PlayerInfo.Ins.GetSpeed();


        }
     
        //是否松开秒刹=====================================
        //if (inputX == 0)
        //{
        //    Pla_Rigidbody.velocity = new Vector2(0, Pla_Rigidbody.velocity.y);

        //}
        //if (inputY == 0)
        //{
        //     Pla_Rigidbody.velocity = new Vector2( Pla_Rigidbody.velocity.x, 0);
      
        //}
     
    }
    private void PlayerSpeedFix()       //最大受力限制
    {

        if (Pla_Rigidbody.velocity.x != 0 && Pla_Rigidbody.velocity.x > Max_Speed)
        {
            Pla_Rigidbody.velocity = new Vector2(Max_Speed, Pla_Rigidbody.velocity.y);
        }
        if (Pla_Rigidbody.velocity.x != 0 && Pla_Rigidbody.velocity.x < -Max_Speed)
        {
            Pla_Rigidbody.velocity = new Vector2(-Max_Speed, Pla_Rigidbody.velocity.y);
        }
        if (Pla_Rigidbody.velocity.y != 0 && Pla_Rigidbody.velocity.y > Max_Speed)
        {
            Pla_Rigidbody.velocity = new Vector2(Pla_Rigidbody.velocity.x, Max_Speed);
        }
        if (Pla_Rigidbody.velocity.y != 0 && Pla_Rigidbody.velocity.y < -Max_Speed)
        {
            Pla_Rigidbody.velocity = new Vector2(Pla_Rigidbody.velocity.x, -Max_Speed);
        }

    }
    
    
    public void yinxin()//隐形，调整透明度为0.5
    {
        IfCantSee = true;
        Color A = new Color();//颜色变化
        A = SelfColor.color;
        A.a = 0.5f;
        SelfColor.color = A;
    }
    public void Show()//显形，调整透明度为1
    {
        IfCantSee = false;
        Color A = new Color();//颜色变化
        A = SelfColor.color;
        A.a = 1.0f;
        SelfColor.color = A;
    }
    public void Be_hurt(float i)//玩家受伤
    {
        Debug.Log("玩家扣了" + i + "滴血");
    }


   
}
