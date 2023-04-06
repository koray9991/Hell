using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public int level;
    public int capacity;
    public int currentDummyCount;
    public Transform chibyPos, chibyPos2, chibyPos3;
    public GameObject level1lava, level2lava, level3lava;
    public float chibyRotX, chibyRotY, chibyRotZ;
    public Animator devil;
    public bool rotating;
    public int givenGhost;
    public ParticleSystem puf;
    private void Start()
    {
        GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>().CheckLava();
    }
    private void Update()
    {
        if (!chibyPos.GetComponent<Pos>().isEmpty || !chibyPos2.GetComponent<Pos>().isEmpty || !chibyPos3.GetComponent<Pos>().isEmpty)
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
