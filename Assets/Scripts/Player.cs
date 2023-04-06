using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public DynamicJoystick Joys;
    public GameManager gm;
    public bool moving;
    public float Speed;
    public float TurnSpeed;
    public Animator anim;
    public List<GameObject> chains;
    public int totalChainCount;
    public int emptyChainCount;

    bool timerBool;
    float timer;
    bool oneFrameBool;
    
    void Start()
    {
        emptyChainCount = totalChainCount;
    }

    private void Update()
    {
        if (timerBool)
        {
            timer += Time.deltaTime;
        }
    }
    void FixedUpdate()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        if (Input.GetMouseButton(0))
        {
            JoystickMovement();
            moving = true;
            anim.SetBool("Walk", true);
            anim.SetBool("Idle", false);
            GetComponent<Rigidbody>().isKinematic = false;
        }
        else
        {
            moving = false;
            anim.SetBool("Walk", false);
            anim.SetBool("Idle", true);
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public void JoystickMovement()
    {
        float horizontal = Joys.Horizontal;
        float vertical = Joys.Vertical;
        Vector3 addedPos = new Vector3(horizontal * Speed * Time.deltaTime, 0, vertical * Speed * Time.deltaTime);
        transform.position += addedPos;

        Vector3 direction = Vector3.forward * vertical + Vector3.right * horizontal;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), TurnSpeed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Chiby")
        {
            if (emptyChainCount > 0)
            {
                for (int i = 0; i < totalChainCount; i++)
                {
                    var myChainParent = chains[i].GetComponent<ChainParent>();
                    if (myChainParent.isEmpty)
                    {
                        other.transform.position = myChainParent.lastChain.transform.position;
                        other.GetComponent<Animator>().enabled = false;
                        myChainParent.isEmpty = false;
                        myChainParent.ChainActive();
                        myChainParent.myChibby = other.gameObject;
                        var Hip = other.GetComponent<Chiby>().hip;
                        Hip.AddComponent<HingeJoint>();
                        Hip.GetComponent<HingeJoint>().connectedBody = myChainParent.lastChain.GetComponent<Rigidbody>();
                        other.GetComponent<Chiby>().jumpPos.GetComponent<ChibyJumpPos>().isEmpty = true;
                        other.GetComponent<Chiby>().jumpPos = null;
                        //other.GetComponent<Collider>().enabled = false;
                        other.gameObject.tag = "Untagged";
                        emptyChainCount -= 1;
                        return;
                    }
                }
            }
           
        }
        if (other.tag == "Manager" && gm.managerPanelTimer>5)
        {
            gm.managerPanelClosed = false;
        }
       

    }
   
    
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Buyable" && !moving && gm.ghostCount >0)
        {
            timerBool = true;
            if (timer > 0.01f)
            {
                var buyObject = other.GetComponent<Buyable>();
                if (buyObject.cost > 50 && gm.ghostCount>50)
                {
                    var value = 50;
                    gm.ghostCount -= value;
                    buyObject.cost -= value;
                }
                else
                {
                    var value = 1;
                    gm.ghostCount -= value;
                    buyObject.cost -= value;
                }
               
                timer = 0;
                gm.ghostCountText.text = (gm.ghostCount).ToString();
                buyObject.costText.text = (buyObject.cost).ToString();
                buyObject.amount.fillAmount = (buyObject.startCost - buyObject.cost) / buyObject.startCost;
                if (buyObject.isNextLevelArea)
                {
                    PlayerPrefs.SetFloat("nextLevelCost", buyObject.cost);
                    PlayerPrefs.SetFloat("fillAmount", buyObject.amount.fillAmount);
                }
              
                if (buyObject.cost <= 0)
                {
                    if (buyObject.openableObject != null)
                    {
                        buyObject.openableObject.transform.GetComponent<Memory>().isEnable = 1;
                        PlayerPrefs.SetInt(buyObject.openableObject.name, buyObject.openableObject.transform.GetComponent<Memory>().isEnable);
                        buyObject.tag = "Untagged";
                        buyObject.canvas.SetActive(false);
                        buyObject.openableObject.SetActive(true);
                    }
                    else
                    {
                        if (!oneFrameBool)
                        {
                            oneFrameBool = true;
                            buyObject.canvas.SetActive(false);
                            gm.winPanel.SetActive(true);
                            DOVirtual.DelayedCall(2, () => {
                                PlayerPrefs.DeleteAll();
                                gm.level += 1;
                                if (gm.level == 3) { gm.level = 0; }
                                PlayerPrefs.SetInt("level", gm.level);
                                SceneManager.LoadScene(gm.level);
                            });
                        }
                       
                       
                    }
                   

                }

            }
        }
        if (other.tag == "Manager")
        {
            if (!moving && !gm.managerPanelClosed)
            {
                gm.managerPanel.SetActive(true);
            }

        }

        if (other.tag == "Boiler")
        {
            timerBool = true;
            if (timer > 0.5f)
            {
                var myBoil = other.GetComponent<Boiler>();
                if (myBoil.currentDummyCount < myBoil.capacity)
                {
                    timer = 0;
                    for (int i = 0; i < totalChainCount; i++)
                    {
                        var myChainParent = chains[i].GetComponent<ChainParent>();
                        if (!myChainParent.isEmpty)
                        {
                            var Hip = myChainParent.myChibby.GetComponent<Chiby>().hip;
                            myChainParent.myChibby.GetComponent<Animator>().enabled = true;
                            myChainParent.myChibby.GetComponent<Animator>().SetBool("Boiler", true);
                            Destroy(Hip.GetComponent<HingeJoint>());
                            Destroy(Hip.GetComponent<Rigidbody>());


                            if (other.GetComponent<Boiler>().chibyPos.GetComponent<Pos>().isEmpty && myChainParent.myChibby.GetComponent<Chiby>().pos==null)
                            {
                                myChainParent.myChibby.transform.DOJump(other.GetComponent<Boiler>().chibyPos.position, 4, 1, 1);
                                myChainParent.myChibby.GetComponent<Chiby>().pos = other.GetComponent<Boiler>().chibyPos.gameObject;
                            }
                            if (other.GetComponent<Boiler>().chibyPos2.GetComponent<Pos>().isEmpty && myChainParent.myChibby.GetComponent<Chiby>().pos == null)
                            {
                                myChainParent.myChibby.transform.DOJump(other.GetComponent<Boiler>().chibyPos2.position, 4, 1, 1);
                                myChainParent.myChibby.GetComponent<Chiby>().pos = other.GetComponent<Boiler>().chibyPos2.gameObject;
                            }
                            if (other.GetComponent<Boiler>().chibyPos3.GetComponent<Pos>().isEmpty && myChainParent.myChibby.GetComponent<Chiby>().pos == null)
                            {
                                myChainParent.myChibby.transform.DOJump(other.GetComponent<Boiler>().chibyPos3.position, 4, 1, 1);
                                myChainParent.myChibby.GetComponent<Chiby>().pos = other.GetComponent<Boiler>().chibyPos3.gameObject;
                            }
                            myChainParent.myChibby.GetComponent<Chiby>().pos.GetComponent<Pos>().isEmpty = false;


                            myChainParent.myChibby.transform.position = transform.position;
                            Hip.transform.localPosition = Vector3.zero;
                            myChainParent.isEmpty = true;
                            myChainParent.ChainPassive();
                            myBoil.currentDummyCount += 1;
                            myChainParent.myChibby.GetComponent<Chiby>().myBoiler = other.gameObject;
                            myChainParent.myChibby.GetComponent<Chiby>().isBoiling = true;
                            myChainParent.myChibby = null;
                            emptyChainCount += 1;
                            return;
                        }
                    }
                }
            }
        }
        if (other.tag == "Strech")
        {
            timerBool = true;
            if (timer > 0.5f)
            {
                var myStrech = other.GetComponent<Strech>();
                if (myStrech.currentDummyCount < myStrech.capacity)
                {
                    timer = 0;
                    for (int i = 0; i < totalChainCount; i++)
                    {
                        var myChainParent = chains[i].GetComponent<ChainParent>();
                        if (!myChainParent.isEmpty)
                        {
                            var Hip = myChainParent.myChibby.GetComponent<Chiby>().hip;
                            myChainParent.myChibby.GetComponent<Animator>().enabled = true;
                            myChainParent.myChibby.GetComponent<Animator>().SetBool("Strech", true);
                            Destroy(Hip.GetComponent<HingeJoint>());
                            Destroy(Hip.GetComponent<Rigidbody>());


                            if (other.GetComponent<Strech>().chibyPos.GetComponent<Pos>().isEmpty && myChainParent.myChibby.GetComponent<Chiby>().pos == null)
                            {
                                myChainParent.myChibby.transform.DOMove(myStrech.chibyPos.position, 1);
                                myChainParent.myChibby.GetComponent<Chiby>().pos = other.GetComponent<Strech>().chibyPos.gameObject;
                            }
                            if (other.GetComponent<Strech>().chibyPos2.GetComponent<Pos>().isEmpty && myChainParent.myChibby.GetComponent<Chiby>().pos == null)
                            {
                                myChainParent.myChibby.transform.DOMove(myStrech.chibyPos2.position, 1);
                                myChainParent.myChibby.GetComponent<Chiby>().pos = other.GetComponent<Strech>().chibyPos2.gameObject;
                            }
                            if (other.GetComponent<Strech>().chibyPos3.GetComponent<Pos>().isEmpty && myChainParent.myChibby.GetComponent<Chiby>().pos == null)
                            {
                                myChainParent.myChibby.transform.DOMove(myStrech.chibyPos3.position, 1);
                                myChainParent.myChibby.GetComponent<Chiby>().pos = other.GetComponent<Strech>().chibyPos3.gameObject;
                            }
                            myChainParent.myChibby.GetComponent<Chiby>().pos.GetComponent<Pos>().isEmpty = false;

                            myChainParent.myChibby.transform.position = transform.position;
                            myChainParent.myChibby.transform.DOScaleX(myChainParent.myChibby.transform.localScale.x * 2f, 4.9f).SetEase(Ease.Linear);
                            Hip.transform.localPosition = Vector3.zero;
                            myChainParent.isEmpty = true;
                            myChainParent.ChainPassive();
                            myStrech.currentDummyCount += 1;
                            myChainParent.myChibby.GetComponent<Chiby>().myStrech = other.gameObject;
                            myChainParent.myChibby.GetComponent<Chiby>().isStreching = true;
                            myChainParent.myChibby = null;
                            emptyChainCount += 1;
                            return;
                        }
                    }
                }
            }
        }
        if (other.tag == "Smash")
        {
            timerBool = true;
            if (timer > 0.5f)
            {
                var mySmash = other.GetComponent<Smash>();
                if (mySmash.currentDummyCount < mySmash.capacity)
                {
                    timer = 0;
                    for (int i = 0; i < totalChainCount; i++)
                    {
                        var myChainParent = chains[i].GetComponent<ChainParent>();
                        if (!myChainParent.isEmpty)
                        {
                            var Hip = myChainParent.myChibby.GetComponent<Chiby>().hip;
                            myChainParent.myChibby.GetComponent<Animator>().enabled = true;
                            myChainParent.myChibby.GetComponent<Animator>().SetBool("Smash", true);
                            Destroy(Hip.GetComponent<HingeJoint>());
                            Destroy(Hip.GetComponent<Rigidbody>());


                            if (other.GetComponent<Smash>().chibyPos.GetComponent<Pos>().isEmpty && myChainParent.myChibby.GetComponent<Chiby>().pos == null)
                            {
                                myChainParent.myChibby.transform.DOMove(mySmash.chibyPos.position, 1);
                                myChainParent.myChibby.GetComponent<Chiby>().pos = other.GetComponent<Smash>().chibyPos.gameObject;
                            }
                            if (other.GetComponent<Smash>().chibyPos2.GetComponent<Pos>().isEmpty && myChainParent.myChibby.GetComponent<Chiby>().pos == null)
                            {
                                myChainParent.myChibby.transform.DOMove(mySmash.chibyPos2.position, 1);
                                myChainParent.myChibby.GetComponent<Chiby>().pos = other.GetComponent<Smash>().chibyPos2.gameObject;
                            }
                            if (other.GetComponent<Smash>().chibyPos3.GetComponent<Pos>().isEmpty && myChainParent.myChibby.GetComponent<Chiby>().pos == null)
                            {
                                myChainParent.myChibby.transform.DOMove(mySmash.chibyPos3.position, 1);
                                myChainParent.myChibby.GetComponent<Chiby>().pos = other.GetComponent<Smash>().chibyPos3.gameObject;
                            }
                            myChainParent.myChibby.GetComponent<Chiby>().pos.GetComponent<Pos>().isEmpty = false;


                            
                            myChainParent.myChibby.transform.DOLocalRotateQuaternion(Quaternion.Euler(mySmash.chibyRotX, mySmash.chibyRotY, mySmash.chibyRotZ), 1);
                            myChainParent.myChibby.transform.position = transform.position;
                            Hip.transform.localPosition = Vector3.zero;
                            myChainParent.isEmpty = true;
                            myChainParent.ChainPassive();
                            mySmash.currentDummyCount += 1;
                            myChainParent.myChibby.GetComponent<Chiby>().mySmash = other.gameObject;
                            myChainParent.myChibby.GetComponent<Chiby>().isSmashing = true;
                            myChainParent.myChibby = null;
                            emptyChainCount += 1;
                            return;
                        }
                    }
                }
            }
        }
        if (other.tag == "Lava")
        {
            timerBool = true;
            if (timer > 0.5f)
            {
                var myLava = other.GetComponent<Lava>();
                if (myLava.currentDummyCount < myLava.capacity)
                {
                    timer = 0;
                    for (int i = 0; i < totalChainCount; i++)
                    {
                        var myChainParent = chains[i].GetComponent<ChainParent>();
                        if (!myChainParent.isEmpty)
                        {
                            var Hip = myChainParent.myChibby.GetComponent<Chiby>().hip;
                            myChainParent.myChibby.GetComponent<Animator>().enabled = true;
                            myChainParent.myChibby.GetComponent<Animator>().SetBool("Boiler", true);
                            Destroy(Hip.GetComponent<HingeJoint>());
                            Destroy(Hip.GetComponent<Rigidbody>());


                            if (other.GetComponent<Lava>().chibyPos.GetComponent<Pos>().isEmpty && myChainParent.myChibby.GetComponent<Chiby>().pos == null)
                            {
                                myChainParent.myChibby.transform.DOJump(other.GetComponent<Lava>().chibyPos.position, 4, 1, 1);
                                myChainParent.myChibby.GetComponent<Chiby>().pos = other.GetComponent<Lava>().chibyPos.gameObject;
                            }
                            if (other.GetComponent<Lava>().chibyPos2.GetComponent<Pos>().isEmpty && myChainParent.myChibby.GetComponent<Chiby>().pos == null)
                            {
                                myChainParent.myChibby.transform.DOJump(other.GetComponent<Lava>().chibyPos2.position, 4, 1, 1);
                                myChainParent.myChibby.GetComponent<Chiby>().pos = other.GetComponent<Lava>().chibyPos2.gameObject;
                            }
                            if (other.GetComponent<Lava>().chibyPos3.GetComponent<Pos>().isEmpty && myChainParent.myChibby.GetComponent<Chiby>().pos == null)
                            {
                                myChainParent.myChibby.transform.DOJump(other.GetComponent<Lava>().chibyPos3.position, 4, 1, 1);
                                myChainParent.myChibby.GetComponent<Chiby>().pos = other.GetComponent<Lava>().chibyPos3.gameObject;
                            }
                            myChainParent.myChibby.GetComponent<Chiby>().pos.GetComponent<Pos>().isEmpty = false;


                            myChainParent.myChibby.transform.DOLocalRotateQuaternion(Quaternion.Euler(myLava.chibyRotX, myLava.chibyRotY, myLava.chibyRotZ), 1);
                            myChainParent.myChibby.transform.position = transform.position;
                            Hip.transform.localPosition = Vector3.zero;
                            myChainParent.isEmpty = true;
                            myChainParent.ChainPassive();
                            myLava.currentDummyCount += 1;
                            myChainParent.myChibby.GetComponent<Chiby>().myLava = other.gameObject;
                            myChainParent.myChibby.GetComponent<Chiby>().isLava = true;
                            myChainParent.myChibby = null;
                            emptyChainCount += 1;
                            return;
                        }
                    }
                }
            }
        }
        if (other.tag == "Kebab")
        {
            timerBool = true;
            if (timer > 0.5f)
            {
                var myKebab = other.GetComponent<Kebab>();
                if (myKebab.currentDummyCount < myKebab.capacity)
                {
                    timer = 0;
                    for (int i = 0; i < totalChainCount; i++)
                    {
                        var myChainParent = chains[i].GetComponent<ChainParent>();
                        if (!myChainParent.isEmpty)
                        {
                            var Hip = myChainParent.myChibby.GetComponent<Chiby>().hip;
                            myChainParent.myChibby.GetComponent<Animator>().enabled = true;
                            myChainParent.myChibby.GetComponent<Animator>().SetBool("Kebab", true);
                            Destroy(Hip.GetComponent<HingeJoint>());
                            Destroy(Hip.GetComponent<Rigidbody>());


                            if (other.GetComponent<Kebab>().chibyPos.GetComponent<Pos>().isEmpty && myChainParent.myChibby.GetComponent<Chiby>().pos == null)
                            {
                                myChainParent.myChibby.transform.DOJump(other.GetComponent<Kebab>().chibyPos.position, 0, 1, 1);
                                myChainParent.myChibby.GetComponent<Chiby>().pos = other.GetComponent<Kebab>().chibyPos.gameObject;
                            }
                            if (other.GetComponent<Kebab>().chibyPos2.GetComponent<Pos>().isEmpty && myChainParent.myChibby.GetComponent<Chiby>().pos == null)
                            {
                                myChainParent.myChibby.transform.DOJump(other.GetComponent<Kebab>().chibyPos2.position, 0, 1, 1);
                                myChainParent.myChibby.GetComponent<Chiby>().pos = other.GetComponent<Kebab>().chibyPos2.gameObject;
                            }
                            if (other.GetComponent<Kebab>().chibyPos3.GetComponent<Pos>().isEmpty && myChainParent.myChibby.GetComponent<Chiby>().pos == null)
                            {
                                myChainParent.myChibby.transform.DOJump(other.GetComponent<Kebab>().chibyPos3.position, 0, 1, 1);
                                myChainParent.myChibby.GetComponent<Chiby>().pos = other.GetComponent<Kebab>().chibyPos3.gameObject;
                            }
                            myChainParent.myChibby.GetComponent<Chiby>().pos.GetComponent<Pos>().isEmpty = false;



                           
                            myChainParent.myChibby.transform.DOLocalRotateQuaternion(Quaternion.Euler(myKebab.chibyRotX, myKebab.chibyRotY, myKebab.chibyRotZ), 1);
                            myChainParent.myChibby.transform.position = transform.position;
                            Hip.transform.localPosition = Vector3.zero;
                            myChainParent.isEmpty = true;
                            myChainParent.ChainPassive();
                            myKebab.currentDummyCount += 1;
                            myChainParent.myChibby.GetComponent<Chiby>().myKebab = other.gameObject;
                            myChainParent.myChibby.GetComponent<Chiby>().isKebab = true;
                            myChainParent.myChibby = null;
                            emptyChainCount += 1;
                            return;
                        }
                    }
                }
            }
        }
        if (other.tag == "Runner")
        {
            timerBool = true;
            if (timer > 0.5f)
            {
                var myRunner = other.GetComponent<Runner>();
                if (myRunner.currentDummyCount < myRunner.capacity)
                {
                    timer = 0;
                    for (int i = 0; i < totalChainCount; i++)
                    {
                        var myChainParent = chains[i].GetComponent<ChainParent>();
                        if (!myChainParent.isEmpty)
                        {
                            var Hip = myChainParent.myChibby.GetComponent<Chiby>().hip;
                            myChainParent.myChibby.GetComponent<Animator>().enabled = true;
                            myChainParent.myChibby.GetComponent<Animator>().SetBool("Runner", true);
                            Destroy(Hip.GetComponent<HingeJoint>());
                            Destroy(Hip.GetComponent<Rigidbody>());

                            myChainParent.myChibby.transform.DOMove(myRunner.chibyPos.position, 1);
                            myChainParent.myChibby.GetComponent<Chiby>().pos = other.GetComponent<Runner>().chibyPos.gameObject;

                            myChainParent.myChibby.GetComponent<Chiby>().pos.GetComponent<Pos>().isEmpty = false;

                            myChainParent.myChibby.transform.position = transform.position;
                            myChainParent.myChibby.transform.DOScaleZ(0, 4.9f).SetEase(Ease.Linear);
                            Hip.transform.localPosition = Vector3.zero;
                            myChainParent.isEmpty = true;
                            myChainParent.ChainPassive();
                            myRunner.currentDummyCount += 1;
                            myChainParent.myChibby.GetComponent<Chiby>().myRunner = other.gameObject;
                            myChainParent.myChibby.GetComponent<Chiby>().isRunner = true;
                            myChainParent.myChibby = null;
                            emptyChainCount += 1;
                            return;
                        }
                    }
                }
            }
        }
    }
}
