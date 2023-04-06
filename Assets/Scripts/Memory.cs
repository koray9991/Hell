using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class Memory : MonoBehaviour
{
    public int isEnable;
   
    public GameObject canvas;
    public bool tagNotChange;

    private void Awake()
    {
        isEnable = PlayerPrefs.GetInt(transform.name);
        if (isEnable == 0)
        {
            gameObject.SetActive(false);
        }
        if (isEnable == 1)
        {
            canvas.SetActive(false);
        }
    }
    private void OnEnable()
    {
        var x = transform.localScale.x;
        var y = transform.localScale.y;
        var z = transform.localScale.z;
        transform.DOPunchScale(-new Vector3(x/2, y/2, z/2), 1, 5, 1);
    }
}
