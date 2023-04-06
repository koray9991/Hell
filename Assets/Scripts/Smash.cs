using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smash : MonoBehaviour
{

    public int capacity;
    public int currentDummyCount;
    public Transform chibyPos, chibyPos2, chibyPos3;
    public GameObject level1smash, level2smash, level3smash;

    public Transform bang;
    public Transform bangPos;
    public bool downing;
    [HideInInspector] public float defaultPosy;
    public float chibyRotX, chibyRotY, chibyRotZ;
    public Animator rot1, rot2, rot3;
    public bool rotating=true;
    public int givenGhost;
    private void Start()
    {
        defaultPosy = bang.transform.position.y;
        GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>().CheckSmash();
    }
    private void Update()
    {
        if(!chibyPos.GetComponent<Pos>().isEmpty || !chibyPos2.GetComponent<Pos>().isEmpty || !chibyPos3.GetComponent<Pos>().isEmpty )
        {
            if (!rotating)
            {
                rotating = true;
                bang.GetComponent<Animator>().enabled = true;
                rot1.enabled = true;
                rot2.enabled = true;
                rot3.enabled = true;
            }
           
        }
        else
        {
            if (rotating)
            {
                bang.GetComponent<Animator>().enabled = false;
                bang.transform.position = new Vector3(bang.transform.position.x, defaultPosy, bang.transform.position.z);
                rot1.enabled = false;
                rot2.enabled = false;
                rot3.enabled = false;
                rotating = false;
            }
           
        }

       
    }
   
}
