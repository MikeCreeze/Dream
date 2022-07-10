using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Key : MonoBehaviour
{
    [SerializeField]
    private GameObject Parent_Object;

    public int KeyType;
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
        if(collision.gameObject.tag=="Player")
        {
            switch(KeyType)
            {
                case 1:
                    PlayerInfo.Ins.YellowKey = true;
                    break;
                case 2:
                    PlayerInfo.Ins.BlueKey = true;
                    break;
            }
           
            Destroy(Parent_Object);
        }
    }
}
