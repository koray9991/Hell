using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Boat : MonoBehaviour
{
    public GameManager gm;
    public Transform jumpChiby;
    public bool go1Bool, goBool2;
    public GameObject chiby;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        jumpChiby = GameObject.FindGameObjectWithTag("JumpChibyPos").transform;
    }

    void FixedUpdate()
    {
        if (!go1Bool)
        {
            go1Bool = true;
            transform.LookAt(gm.boatLocation1);
            transform.DOMove(gm.boatLocation1.position, 5).OnComplete(() => {
                chiby.GetComponent<Animator>().enabled = false;
                chiby.GetComponent<Collider>().enabled = false;
                chiby.transform.parent = null;
                for (int i = 0; i < gm.jumpChibyParent.childCount; i++)
                {
                    if (gm.jumpChibyParent.GetChild(i).gameObject.activeInHierarchy)
                    {
                        if (gm.jumpChibyParent.GetChild(i).GetComponent<ChibyJumpPos>().isEmpty)
                        {
                            chiby.GetComponent<Chiby>().jumpPos = gm.jumpChibyParent.GetChild(i);
                            gm.jumpChibyParent.GetChild(i).GetComponent<ChibyJumpPos>().isEmpty = false;
                            chiby.transform.DOJump(gm.jumpChibyParent.GetChild(i).position, 4, 1, 1).OnComplete(() => {
                                chiby.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, Random.Range(0,360), 0), 1);
                                chiby.GetComponent<Animator>().enabled = true;
                                chiby.GetComponent<Collider>().enabled = true;
                            });
                            goBool2 = true;
                            return;
                        }
                    }
                }
                goBool2 = true;
                chiby.transform.parent = transform;

            });
        }
        if (goBool2)
        {
            goBool2 = false;
            transform.LookAt(gm.boatLocation2);
            transform.DOMove(gm.boatLocation2.position, 5).OnComplete(() => {
                Destroy(gameObject);
            });
        }
    }
}
