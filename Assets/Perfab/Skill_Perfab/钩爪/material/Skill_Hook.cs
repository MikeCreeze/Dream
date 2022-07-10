using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Hook : MonoBehaviour
{

    public int SkillPlace;

    public Vector2 Destiny;

    private float speed = 0.2F;
    private float Shrink_Speed = 0;
    //public float distance = 2;
    //public GameObject nodePerfab;
    public GameObject player;
    //public GameObject lastNode;
    LineRenderer lineRen;


    public bool done = false;
    public bool Faildone = false;
    public bool doneFirstPrepare = false;
    
    private GameObject LinkObj;
    

    private void Awake()
    {

    }

    void Start()
    {
        lineRen = gameObject.GetComponent<LineRenderer>();

        player = GameObject.FindGameObjectWithTag("Player");
        lineRen.startWidth = 0.8f;
        lineRen.endWidth = 0.8f;
        lineRen.positionCount = 2;
        transform.position = player.gameObject.transform.position;
        PlayerInfo.Ins.Set_Speed(-3.8f,false);


    }

    // Update is called once per frame
    private void FixedUpdate()
    {
    
        if (Shrink_Speed > 0 && done)
        {
            Shrink_Speed -= Time.deltaTime;
        }


        if (!done)
        {
            if (!Faildone)
            {
                transform.position = Vector2.MoveTowards(transform.position, Destiny, speed);
            }

            if (transform.position.x == Destiny.x && transform.position.y == Destiny.y)
            {
                Faildone = true;
                Shrink_Speed = 1;
                Debug.Log("到点未连接，收回");
            }
        }
        else
        {
            LinkObj.transform.position = Vector2.MoveTowards( LinkObj.transform.position, transform.position, Shrink_Speed/5);
        }

        if (!doneFirstPrepare && done)
        {
            doneFirstPrepare = true;

          
            Debug.Log("连接成攻");
        }
       

        
        Hook_Shrink();      //收缩

    }
    private void Update()
    {
        GetAngle(gameObject.transform, player.gameObject.transform);
        transform.Rotate(0, 0, -90f);
        if (done)
        {

            if (Input.GetMouseButtonDown(0))
            {
                if (Shrink_Speed <= 0.1 && done == true)
                {
                    FastCountDown();
                    //Debug.Log("收缩");
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                if (done == true)
                {
              
                     DistroyObj();
            }

            }

        }
        lineRen.SetPosition(0, transform.position);
        lineRen.SetPosition(1, player.transform.position);
    }
    public void Hook_Shrink()
    {
        if (Shrink_Speed > 0)
        {

            transform.position = Vector2.MoveTowards(transform.position, player.gameObject.transform.position, Shrink_Speed / 5f);
            Vector2 Each_Distance = transform.position - player.transform.position;
            if (Each_Distance.magnitude <= 0.3) 
            {
                DistroyObj();
            }
        }
    }
    private void DistroyObj()
    {
        Skill.Instance.ResetSkill(SkillPlace);        //出口
     
        if(LinkObj)
        {
            LinkObj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }
        PlayerInfo.Ins.Set_Speed(3.8f, false);
        Destroy(gameObject);
    }
    public void FastCountDown()
    {
        Shrink_Speed = 0.4f;

    }
    void GetAngle(Transform target1, Transform target2)     //设置爪头旋转角度
    {
        Vector3 dir = target1.position - target2.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        target1.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CanCatch_Obj")
        {
            done = true;
           
            LinkObj = collision.gameObject;
            LinkObj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            Debug.Log("抓取");
        }
        if (collision.gameObject.tag == "Barrier")
        {
            Faildone = true;
            Shrink_Speed = 1;
            Debug.Log("碰到障碍，收回");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == LinkObj)
        {
            done = false;

            LinkObj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

            Debug.Log("抓取");
        }
    }


}