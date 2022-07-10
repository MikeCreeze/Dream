using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject PlayerObject;
    [Range(0, 10)]
    public float Deviation_Y;           //偏移量，在组件中测试
    [Range(0, 10)]
    public float Deviation_Z;
    public float Follow_Speed;          //镜头跟随速度
    // Update is called once per frame
    void Update()
    {
        Vector2 newPos = new Vector3(PlayerObject.transform.position.x, PlayerObject.transform.position.y- Deviation_Y);
        transform.position = Vector2.MoveTowards(transform.position, newPos, Follow_Speed);
        transform.position = new Vector3(transform.position.x, transform.position.y, -Deviation_Z);
    }
}
