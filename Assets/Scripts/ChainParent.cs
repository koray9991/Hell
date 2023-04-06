using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainParent : MonoBehaviour
{
    public Transform hand;
    public bool isEmpty = true;
    public GameObject lastChain;
    public GameObject myChibby;
    void Start()
    {
        lastChain = transform.GetChild(transform.childCount - 1).gameObject;
        ChainPassive();
    }

    
    void FixedUpdate()
    {
        transform.position = hand.position;
    }

    public void ChainActive()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<MeshRenderer>().enabled = true;
        }
    }
    public void ChainPassive()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
