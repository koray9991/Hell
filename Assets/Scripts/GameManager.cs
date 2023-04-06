using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int ghostCount;
    public TextMeshProUGUI ghostCountText;
    float ghostTextX, ghostTextY, ghostTextZ;
    public Transform boatSpawnTransform,boatLocation1,boatLocation2,jumpChibyParent;
    float boatTimer;
    public float maxBoatTime;
    public GameObject boat;
    public int jumpChibyParentCapacity, jumpChibyEmptyCount;
    public bool managerPanelClosed;
    public GameObject managerPanel;
    public float managerPanelTimer;
    public int level;
    public GameObject winPanel;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        level = PlayerPrefs.GetInt("level");
        if (SceneManager.GetActiveScene().buildIndex!=level)
        {
            SceneManager.LoadScene(level);
        }
        ghostCount= PlayerPrefs.GetInt("ghost");
        if (ghostCount < 5)
        {
            ghostCount = 5;
        }
        ghostCountText.text = ghostCount.ToString();
        InvokeRepeating("setGhost", 1.0f,0.3f);
       
        ghostTextX = ghostCountText.transform.localScale.x;
        ghostTextY = ghostCountText.transform.localScale.y;
        ghostTextZ = ghostCountText.transform.localScale.z;
        ghostCountText.text = ghostCount.ToString();
        CheckJumpCapacity();
    }
    void setGhost()
    {
        PlayerPrefs.SetInt("ghost", ghostCount);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            ghostCount += 1000;
        }
        BoatSpawnCheck();
        managerPanelTimer += Time.deltaTime;
    }
    void CheckJumpCapacity()
    {
        for (int i = 0; i < jumpChibyParentCapacity; i++)
        {
            jumpChibyParent.GetChild(i).gameObject.SetActive(true);
        }
    }
   public void EarnGhost(int value)
    {
        ghostCount += value;
        ghostCountText.text = ghostCount.ToString();
        ghostCountText.transform.DOPunchScale(new Vector3(ghostTextX, ghostTextY, ghostTextZ)/7, 0.2f, 1, 1).OnComplete(() => transform.localScale=new Vector3(ghostTextX,ghostTextY,ghostTextZ));
    }

  
  public void SpawnChiby()
    {
        Instantiate(boat, boatSpawnTransform.position, Quaternion.identity);
    }
    
    void BoatSpawnCheck()
    {
        boatTimer += Time.deltaTime;
        if (boatTimer > maxBoatTime)
        {
            for (int i = 0; i < jumpChibyParent.childCount; i++)
            {
                if (jumpChibyParent.GetChild(i).gameObject.activeInHierarchy)
                {
                    if (jumpChibyParent.GetChild(i).GetComponent<ChibyJumpPos>().isEmpty)
                    {
                        SpawnChiby();
                        CheckJumpCapacity();
                        boatTimer = 0;
                        return;
                    }
                }
            }
            boatTimer = 0;
            CheckJumpCapacity();
        }
    }

    public void ManagerPanelClose()
    {
        managerPanelClosed = true;
        managerPanel.SetActive(false);
        managerPanelTimer = 0;
    }

}
