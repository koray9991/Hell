using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strech : MonoBehaviour
{
    public int capacity;
    public int currentDummyCount;
    public Transform chibyPos,chibyPos2,chibyPos3;
    public Transform lookPos;
    public GameObject level1strech, level2strech, level3strech;
    public Transform rotatingObject;
    public bool rotating;
    public int givenGhost;

    private void Start()
    {
        GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>().CheckStrech();
    }

    private void Update()
    {
        if (!chibyPos.GetComponent<Pos>().isEmpty || !chibyPos2.GetComponent<Pos>().isEmpty || !chibyPos3.GetComponent<Pos>().isEmpty)
        {
            if (!rotating)
            {
                rotating = true;
                rotatingObject.GetComponent<Animator>().enabled = true;
            }

        }
        else
        {
            if (rotating)
            {
                rotatingObject.GetComponent<Animator>().enabled = false;
                rotating = false;
            }

        }
    }
}
