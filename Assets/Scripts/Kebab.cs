using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kebab : MonoBehaviour
{
    public int level;
    public int capacity;
    public int currentDummyCount;
    public Transform chibyPos, chibyPos2, chibyPos3;
    public GameObject level1kebab, level2kebab, level3kebab;
    public float chibyRotX, chibyRotY, chibyRotZ;
    public bool rotating;
    public Transform rotatingObject,rotatingObject2,rotatingObject3;
    public int givenGhost;
    private void Start()
    {
        GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>().CheckKebab();
    }

    private void Update()
    {
        if (!chibyPos.GetComponent<Pos>().isEmpty || !chibyPos2.GetComponent<Pos>().isEmpty || !chibyPos3.GetComponent<Pos>().isEmpty)
        {
            if (!rotating)
            {
                rotating = true;
                rotatingObject.GetComponent<Animator>().enabled = true;
                rotatingObject2.GetComponent<Animator>().enabled = true;
                rotatingObject3.GetComponent<Animator>().enabled = true;

            }

        }
        else
        {
            if (rotating)
            {
                rotatingObject.GetComponent<Animator>().enabled = false;
                rotatingObject2.GetComponent<Animator>().enabled = false;
                rotatingObject3.GetComponent<Animator>().enabled = false;
                rotating = false;
            }

        }
    }
}
