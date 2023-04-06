using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Chiby : MonoBehaviour
{
    public GameObject pos;
    public GameObject hip;
    public bool isBoiling,isStreching,isSmashing,isLava,isKebab,isRunner;
    [HideInInspector] public float timer, destroyTime;
    public GameObject myBoiler,myStrech,mySmash,myLava,myKebab,myRunner;
    public GameObject particle;
    public GameManager gm;
    public Transform jumpPos;
    public bool frameBool;
    
    private void Start()
    {
        destroyTime = 5;
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }
    private void FixedUpdate()
    {
        if (isBoiling)
        {
            Boiling();
        }
        if (isStreching)
        {
            Streching();
        }
        if (isSmashing)
        {
            Smashing();
        }
        if (isLava)
        {
            Lava();
        }
        if (isKebab)
        {
            Kebab();
        }
        if (isRunner)
        {
            Running();
        }
    }
    private void Boiling()
    {
        transform.Rotate(0, 2, 0);
        timer += Time.deltaTime;
        if (timer > destroyTime)
        {
            pos.GetComponent<Pos>().isEmpty = true;
            isBoiling = false;
            timer = 0;
            myBoiler.GetComponent<Boiler>().currentDummyCount -= 1;
            particle.SetActive(true);
            particle.transform.parent = null;
            gm.EarnGhost(myBoiler.GetComponent<Boiler>().givenGhost);
            Destroy(particle, 3f);
            Destroy(gameObject);
        }
    }
    private void Streching()
    {
        timer += Time.deltaTime;
        var lookPos = myStrech.GetComponent<Strech>().lookPos.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);
     
        if (timer > destroyTime)
        {
            transform.localScale = new Vector3(2, 2, 2);
            pos.GetComponent<Pos>().isEmpty = true;
            isStreching = false;
            timer = 0;
            myStrech.GetComponent<Strech>().currentDummyCount -= 1;
            particle.SetActive(true);
            particle.transform.parent = null;
            gm.EarnGhost(myStrech.GetComponent<Strech>().givenGhost);
            Destroy(particle, 3f);
            Destroy(gameObject);
        }
    }
    private void Smashing()
    {
        timer += Time.deltaTime;
        if (!frameBool)
        {
            frameBool=true;
            transform.DOScaleZ(0, 4.9f).SetEase(Ease.Linear);
        }
       
        if (timer > destroyTime)
        {
            transform.localScale = new Vector3(2, 2, 2);
            pos.GetComponent<Pos>().isEmpty = true;
            isSmashing = false;
            timer = 0;
            mySmash.GetComponent<Smash>().currentDummyCount -= 1;
            particle.SetActive(true);
            particle.transform.parent = null;
            particle.transform.localScale = particle.transform.localScale / 3;
            gm.EarnGhost(mySmash.GetComponent<Smash>().givenGhost);
            mySmash.GetComponent<Smash>().bang.DOMoveY(mySmash.GetComponent<Smash>().defaultPosy, 2f);
            mySmash.GetComponent<Smash>().downing = false;
            Destroy(particle, 3f);
            Destroy(gameObject);
        }
    }
    private void Lava()
    {
        transform.Rotate(0, 2, 0);
        timer += Time.deltaTime;
        if (timer > destroyTime)
        {
            pos.GetComponent<Pos>().isEmpty = true;
            isLava = false;
            timer = 0;
            myLava.GetComponent<Lava>().currentDummyCount -= 1;
            particle.SetActive(true);
            particle.transform.parent = null;
            gm.EarnGhost(myLava.GetComponent<Lava>().givenGhost);
            Destroy(particle, 3f);
            Destroy(gameObject);
        }
    }
    private void Kebab()
    {
        transform.Rotate(-6, 0, 0);
        timer += Time.deltaTime;
        if (timer > destroyTime)
        {
            pos.GetComponent<Pos>().isEmpty = true;
            isKebab = false;
            timer = 0;
            myKebab.GetComponent<Kebab>().currentDummyCount -= 1;
            particle.SetActive(true);
            particle.transform.parent = null;
            gm.EarnGhost(myKebab.GetComponent<Kebab>().givenGhost);
            Destroy(particle, 3f);
            Destroy(gameObject);
        }
    }
    private void Running()
    {
        timer += Time.deltaTime;
        var lookPos = myRunner.GetComponent<Runner>().lookPos.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);

        if (timer > destroyTime)
        {
            transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            pos.GetComponent<Pos>().isEmpty = true;
            isRunner = false;
            timer = 0;
            myRunner.GetComponent<Runner>().currentDummyCount -= 1;
            particle.SetActive(true);
            particle.transform.parent = null;
            gm.EarnGhost(1);
            Destroy(particle, 3f);
            Destroy(gameObject);
        }
    }


}
