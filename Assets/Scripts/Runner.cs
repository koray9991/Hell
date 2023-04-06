using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    public int capacity;
    public int currentDummyCount;
    public Transform chibyPos;
    public Transform lookPos;
    public Animator devil;
    public bool rotating;
    private void Update()
    {
        if (!chibyPos.GetComponent<Pos>().isEmpty)
        {
            if (!rotating)
            {
                rotating = true;
                devil.SetBool("attack", true);
                devil.SetBool("fly", false);
            }

        }
        else
        {
            if (rotating)
            {
                devil.SetBool("fly", true);
                devil.SetBool("attack", false);
                rotating = false;
            }

        }
    }

}
