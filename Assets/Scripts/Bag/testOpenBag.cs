using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testOpenBag : MonoBehaviour
{
    public GameObject Bag;
    public bool IsOpen = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ShowBag();
        }
    }

    public void ShowBag()
    {
        IsOpen = !IsOpen;
        Bag.SetActive(IsOpen);
    }
}
