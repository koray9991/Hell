using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class Buyable : MonoBehaviour
{
    public float startCost;
    [HideInInspector] public float cost;
    public TextMeshProUGUI costText;
    public Image amount;
    public GameObject openableObject;
    public GameObject canvas;
    public bool isNextLevelArea;

    private void Start()
    {
        cost = startCost;
        costText.text = cost.ToString();
        DOVirtual.DelayedCall(1, () => {
            if (openableObject.GetComponent<Memory>().isEnable == 1)
            {
                transform.tag = "Untagged";
            }
        });
        if (isNextLevelArea)
        {
           
            cost = PlayerPrefs.GetFloat("nextLevelCost");
            if (cost == 0)
            {
                cost = 250* (SceneManager.GetActiveScene().buildIndex+1);
                PlayerPrefs.SetFloat("nextLevelCost", cost);
            }
            costText.text = cost.ToString();
            amount.fillAmount = PlayerPrefs.GetFloat("fillAmount");
        }

    }

}
