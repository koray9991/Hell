using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Manager : MonoBehaviour
{
    public GameManager gm;
    [Header("Boiler")]
    public Boiler boiler;
    public Button boilerUpgradeButton;
    public int boilerCost;
    public TextMeshProUGUI boilerCostText;
    public int boilerLevel;
    public TextMeshProUGUI boilerLevelText;
    public GameObject lockedImage;
    public GameObject maxText;
    public Transform level1Pos1;
    public Transform level2Pos1, level2Pos2;
    public Transform level3Pos1, level3Pos2, level3Pos3;

    [Header("Strech")]
    public Strech strech;
    public Button strechUpgradeButton;
    public int strechCost;
    public TextMeshProUGUI strechCostText;
    public int strechLevel;
    public TextMeshProUGUI strechLevelText;
    public GameObject strechlockedImage;
    public GameObject strechmaxText;
    public Transform strechlevel1Pos1;
    public Transform strechlevel2Pos1, strechlevel2Pos2;
    public Transform strechlevel3Pos1, strechlevel3Pos2, strechlevel3Pos3;

    [Header("Smash")]
    public Smash smash;
    public Button smashUpgradeButton;
    public int smashCost;
    public TextMeshProUGUI smashCostText;
    public int smashLevel;
    public TextMeshProUGUI smashLevelText;
    public GameObject smashlockedImage;
    public GameObject smashmaxText;
    public Transform smashlevel1Pos1;
    public Transform smashlevel2Pos1, smashlevel2Pos2;
    public Transform smashlevel3Pos1, smashlevel3Pos2, smashlevel3Pos3;

    [Header("Lava")]
    public Lava lava;
    public Button lavaUpgradeButton;
    public int lavaCost;
    public TextMeshProUGUI lavaCostText;
    public int lavaLevel;
    public TextMeshProUGUI lavaLevelText;
    public GameObject lavalockedImage;
    public GameObject lavamaxText;
    public Transform lavalevel1Pos1;
    public Transform lavalevel2Pos1, lavalevel2Pos2;
    public Transform lavalevel3Pos1, lavalevel3Pos2, lavalevel3Pos3;

    [Header("Kebab")]
    public Kebab kebab;
    public Button kebabUpgradeButton;
    public int kebabCost;
    public TextMeshProUGUI kebabCostText;
    public int kebabLevel;
    public TextMeshProUGUI kebabLevelText;
    public GameObject kebablockedImage;
    public GameObject kebabmaxText;
    public Transform kebablevel1Pos1;
    public Transform kebablevel2Pos1, kebablevel2Pos2;
    public Transform kebablevel3Pos1, kebablevel3Pos2, kebablevel3Pos3;

    [Header("Speed")]
    public Player player;
    public Button speedUpgradeButton;
    public int speedCost;
    public TextMeshProUGUI speedCostText;
    public int speedLevel;
    public TextMeshProUGUI speedLevelText;
    public GameObject speedmaxText;

    [Header("Chain")]
    public Button chainUpgradeButton;
    public int chainCost;
    public TextMeshProUGUI chainCostText;
    public int chainLevel;
    public TextMeshProUGUI chainLevelText;
    public GameObject chainmaxText;

    [Header("BoatSpeed")]
    public Button boatUpgradeButton;
    public int boatCost;
    public TextMeshProUGUI boatCostText;
    public int boatLevel;
    public TextMeshProUGUI boatLevelText;
    public GameObject boatmaxText;

    [Header("Dock")]
    public Button dockUpgradeButton;
    public int dockCost;
    public TextMeshProUGUI dockCostText;
    public int dockLevel;
    public TextMeshProUGUI dockLevelText;
    public GameObject dockmaxText;



    private void Start()
    {
        CheckBoiler();
        CheckStrech();
        CheckSmash();
        CheckLava();
        CheckKebab();
        CheckSpeed();
        CheckChain();
        CheckBoat();
        CheckDock();
    }

  
    public void CheckBoiler()
    {
        if (boiler.GetComponent<Memory>().isEnable == 1)
        {
            boilerLevel = PlayerPrefs.GetInt("boilerLevel");
            if (boilerLevel == 0) { boilerLevel = 1; PlayerPrefs.SetInt("boilerLevel", boilerLevel); }
            boilerUpgradeButton.gameObject.SetActive(true);
            boilerLevelText.gameObject.SetActive(true);
            lockedImage.SetActive(false);
            maxText.SetActive(false);
            if (boilerLevel == 1)
            {
                boiler.level1Boiler.SetActive(true);
                boiler.level2boiler.SetActive(false);
                boiler.level3boiler.SetActive(false);
                boilerCost = 20;
                boilerCostText.text = boilerCost.ToString();
                boiler.capacity = 1;
                boilerLevelText.text = "LEVEL " + boilerLevel.ToString();
                boiler.chibyPos.position = level1Pos1.position;
            }
            if (boilerLevel == 2)
            {
                boiler.level1Boiler.SetActive(false);
                boiler.level2boiler.SetActive(true);
                boiler.level3boiler.SetActive(false);
                boilerCost = 50;
                boilerCostText.text = boilerCost.ToString();
                boiler.capacity = 2;
                boilerLevelText.text = "LEVEL " + boilerLevel.ToString();
                boiler.chibyPos.position = level2Pos1.position;
                boiler.chibyPos2.position = level2Pos2.position;
            }
            if (boilerLevel == 3)
            {
                boiler.level1Boiler.SetActive(false);
                boiler.level2boiler.SetActive(false);
                boiler.level3boiler.SetActive(true);
                boilerUpgradeButton.gameObject.SetActive(false);
                maxText.SetActive(true);
                boiler.capacity = 3;
                boilerLevelText.text = "LEVEL " + boilerLevel.ToString();
                boiler.chibyPos.position = level3Pos1.position;
                boiler.chibyPos2.position = level3Pos2.position;
                boiler.chibyPos3.position = level3Pos3.position;
            }
        }
        else
        {
            boilerUpgradeButton.gameObject.SetActive(false);
            lockedImage.SetActive(true);
            boilerLevelText.gameObject.SetActive(false);
            maxText.SetActive(false);
        }
      
    }
    public void CheckStrech()
    {
        if (strech.GetComponent<Memory>().isEnable == 1)
        {
            strechLevel = PlayerPrefs.GetInt("strechLevel");
            if (strechLevel == 0) { strechLevel = 1; PlayerPrefs.SetInt("strechLevel", strechLevel); }
            strechUpgradeButton.gameObject.SetActive(true);
            strechLevelText.gameObject.SetActive(true);
            strechlockedImage.SetActive(false);
            strechmaxText.SetActive(false);
            if (strechLevel == 1)
            {
                strech.level1strech.SetActive(true);
                strech.level2strech.SetActive(false);
                strech.level3strech.SetActive(false);
                strechCost = 20;
                strechCostText.text = strechCost.ToString();
                strech.capacity = 1;
                strechLevelText.text = "LEVEL " + strechLevel.ToString();
                strech.chibyPos.position = strechlevel1Pos1.position;
            }
            if (strechLevel == 2)
            {
                strech.level1strech.SetActive(false);
                strech.level2strech.SetActive(true);
                strech.level3strech.SetActive(false);
                strechCost = 50;
                strechCostText.text = strechCost.ToString();
                strech.capacity = 2;
                strechLevelText.text = "LEVEL " + strechLevel.ToString();
                strech.chibyPos.position = strechlevel2Pos1.position;
                strech.chibyPos2.position = strechlevel2Pos2.position;
            }
            if (strechLevel == 3)
            {
                strech.level1strech.SetActive(false);
                strech.level2strech.SetActive(false);
                strech.level3strech.SetActive(true);
                strechUpgradeButton.gameObject.SetActive(false);
                strechmaxText.SetActive(true);
                strech.capacity = 3;
                strechLevelText.text = "LEVEL " + strechLevel.ToString();
                strech.chibyPos.position = strechlevel3Pos1.position;
                strech.chibyPos2.position = strechlevel3Pos2.position;
                strech.chibyPos3.position = strechlevel3Pos3.position;
            }
        }
        else
        {
            strechUpgradeButton.gameObject.SetActive(false);
            strechlockedImage.SetActive(true);
            strechLevelText.gameObject.SetActive(false);
            strechmaxText.SetActive(false);
        }

    }
    public void CheckSmash()
    {
        if (smash.GetComponent<Memory>().isEnable == 1)
        {
            smashLevel = PlayerPrefs.GetInt("smashLevel");
            if (smashLevel == 0) { smashLevel = 1; PlayerPrefs.SetInt("smashLevel", smashLevel); }
            smashUpgradeButton.gameObject.SetActive(true);
            smashLevelText.gameObject.SetActive(true);
            smashlockedImage.SetActive(false);
            smashmaxText.SetActive(false);
            if (smashLevel == 1)
            {
                smash.level1smash.SetActive(true);
                smash.level2smash.SetActive(false);
                smash.level3smash.SetActive(false);
                smashCost = 20;
                smashCostText.text = smashCost.ToString();
                smash.capacity = 1;
                smashLevelText.text = "LEVEL " + smashLevel.ToString();
                smash.chibyPos.position = smashlevel1Pos1.position;
            }
            if (smashLevel == 2)
            {
                smash.level1smash.SetActive(false);
                smash.level2smash.SetActive(true);
                smash.level3smash.SetActive(false);
                smashCost = 50;
                smashCostText.text = smashCost.ToString();
                smash.capacity = 2;
                smashLevelText.text = "LEVEL " + smashLevel.ToString();
                smash.chibyPos.position = smashlevel2Pos1.position;
                smash.chibyPos2.position = smashlevel2Pos2.position;
            }
            if (smashLevel == 3)
            {
                smash.level1smash.SetActive(false);
                smash.level2smash.SetActive(false);
                smash.level3smash.SetActive(true);
                smashUpgradeButton.gameObject.SetActive(false);
                smashmaxText.SetActive(true);
                smash.capacity = 3;
                smashLevelText.text = "LEVEL " + smashLevel.ToString();
                smash.chibyPos.position = smashlevel3Pos1.position;
                smash.chibyPos2.position = smashlevel3Pos2.position;
                smash.chibyPos3.position = smashlevel3Pos3.position;
            }
        }
        else
        {
            smashUpgradeButton.gameObject.SetActive(false);
            smashlockedImage.SetActive(true);
            smashLevelText.gameObject.SetActive(false);
            smashmaxText.SetActive(false);
        }

    }
    public void CheckLava()
    {
        if (lava.GetComponent<Memory>().isEnable == 1)
        {
            lavaLevel = PlayerPrefs.GetInt("lavaLevel");
            if (lavaLevel == 0) { lavaLevel = 1; PlayerPrefs.SetInt("lavaLevel", lavaLevel); }
            lavaUpgradeButton.gameObject.SetActive(true);
            lavaLevelText.gameObject.SetActive(true);
            lavalockedImage.SetActive(false);
            lavamaxText.SetActive(false);
            if (lavaLevel == 1)
            {
                lava.level1lava.SetActive(true);
                lava.level2lava.SetActive(false);
                lava.level3lava.SetActive(false);
                lavaCost = 20;
                lavaCostText.text = lavaCost.ToString();
                lava.capacity = 1;
                lavaLevelText.text = "LEVEL " + lavaLevel.ToString();
                lava.chibyPos.position = lavalevel1Pos1.position;
            }
            if (lavaLevel == 2)
            {
                lava.level1lava.SetActive(false);
                lava.level2lava.SetActive(true);
                lava.level3lava.SetActive(false);
                lavaCost = 50;
                lavaCostText.text = lavaCost.ToString();
                lava.capacity = 2;
                lavaLevelText.text = "LEVEL " + lavaLevel.ToString();
                lava.chibyPos.position = lavalevel2Pos1.position;
                lava.chibyPos2.position = lavalevel2Pos2.position;
            }
            if (lavaLevel == 3)
            {
                lava.level1lava.SetActive(false);
                lava.level2lava.SetActive(false);
                lava.level3lava.SetActive(true);
                lavaUpgradeButton.gameObject.SetActive(false);
                lavamaxText.SetActive(true);
                lava.capacity = 3;
                lavaLevelText.text = "LEVEL " + lavaLevel.ToString();
                lava.chibyPos.position = lavalevel3Pos1.position;
                lava.chibyPos2.position = lavalevel3Pos2.position;
                lava.chibyPos3.position = lavalevel3Pos3.position;
            }
        }
        else
        {
            lavaUpgradeButton.gameObject.SetActive(false);
            lavalockedImage.SetActive(true);
            lavaLevelText.gameObject.SetActive(false);
            lavamaxText.SetActive(false);
        }

    }
    public void CheckKebab()
    {
        if (kebab.GetComponent<Memory>().isEnable == 1)
        {
            kebabLevel = PlayerPrefs.GetInt("kebabLevel");
            if (kebabLevel == 0) { kebabLevel = 1; PlayerPrefs.SetInt("kebabLevel", kebabLevel); }
            kebabUpgradeButton.gameObject.SetActive(true);
            kebabLevelText.gameObject.SetActive(true);
            kebablockedImage.SetActive(false);
            kebabmaxText.SetActive(false);
            if (kebabLevel == 1)
            {
                kebab.level1kebab.SetActive(true);
                kebab.level2kebab.SetActive(false);
                kebab.level3kebab.SetActive(false);
                kebabCost = 20;
                kebabCostText.text = kebabCost.ToString();
                kebab.capacity = 1;
                kebabLevelText.text = "LEVEL " + kebabLevel.ToString();
                kebab.chibyPos.position = kebablevel1Pos1.position;
            }
            if (kebabLevel == 2)
            {
                kebab.level1kebab.SetActive(false);
                kebab.level2kebab.SetActive(true);
                kebab.level3kebab.SetActive(false);
                kebabCost = 50;
                kebabCostText.text = kebabCost.ToString();
                kebab.capacity = 2;
                kebabLevelText.text = "LEVEL " + kebabLevel.ToString();
                kebab.chibyPos.position = kebablevel2Pos1.position;
                kebab.chibyPos2.position = kebablevel2Pos2.position;
            }
            if (kebabLevel == 3)
            {
                kebab.level1kebab.SetActive(false);
                kebab.level2kebab.SetActive(false);
                kebab.level3kebab.SetActive(true);
                kebabUpgradeButton.gameObject.SetActive(false);
                kebabmaxText.SetActive(true);
                kebab.capacity = 3;
                kebabLevelText.text = "LEVEL " + kebabLevel.ToString();
                kebab.chibyPos.position = kebablevel3Pos1.position;
                kebab.chibyPos2.position = kebablevel3Pos2.position;
                kebab.chibyPos3.position = kebablevel3Pos3.position;
            }
        }
        else
        {
            kebabUpgradeButton.gameObject.SetActive(false);
            kebablockedImage.SetActive(true);
            kebabLevelText.gameObject.SetActive(false);
            kebabmaxText.SetActive(false);
        }

    }
    public void CheckSpeed()
    {
        speedLevel = PlayerPrefs.GetInt("speedLevel");
        if (speedLevel == 0) { speedLevel = 1; PlayerPrefs.SetInt("speedLevel", speedLevel); }
        speedUpgradeButton.gameObject.SetActive(true);
        speedLevelText.gameObject.SetActive(true);
        speedmaxText.SetActive(false);
        if (speedLevel == 1)
        {
            player.Speed = 6;
            speedCost = 20;
            speedCostText.text = speedCost.ToString();
            speedLevelText.text = "LEVEL " + speedLevel.ToString();
        }
        if (speedLevel == 2)
        {
            player.Speed = 8f;
            speedCost = 50;
            speedCostText.text = speedCost.ToString();
            speedLevelText.text = "LEVEL " + speedLevel.ToString();
        }
        if (speedLevel == 3)
        {
            player.Speed = 10;
            speedCost = 50;
            speedCostText.text = speedCost.ToString();
            speedLevelText.text = "LEVEL " + speedLevel.ToString();
            speedUpgradeButton.gameObject.SetActive(false);
            speedmaxText.SetActive(true);
        }

    }
    public void CheckChain()
    {
        chainLevel = PlayerPrefs.GetInt("chainLevel");
        if (chainLevel == 0) { chainLevel = 1; PlayerPrefs.SetInt("chainLevel", chainLevel); }
        chainUpgradeButton.gameObject.SetActive(true);
        chainLevelText.gameObject.SetActive(true);
        chainmaxText.SetActive(false);

        player.totalChainCount = chainLevel;
        chainCost = 10*chainLevel;
        chainCostText.text = chainCost.ToString();
        chainLevelText.text = "LEVEL " + chainLevel.ToString();
        if (chainLevel == 8)
        {
            chainLevelText.text = "LEVEL " + chainLevel.ToString();
            chainUpgradeButton.gameObject.SetActive(false);
            chainmaxText.SetActive(true);
        }
        player.emptyChainCount += 1;
    }
    public void CheckBoat()
    {
        boatLevel = PlayerPrefs.GetInt("boatLevel");
        if (boatLevel == 0) { boatLevel = 1; PlayerPrefs.SetInt("boatLevel", boatLevel); }
        boatUpgradeButton.gameObject.SetActive(true);
        boatLevelText.gameObject.SetActive(true);
        boatmaxText.SetActive(false);
        if (boatLevel == 1)
        {
            gm.maxBoatTime = 6;
            boatCost = 20;
            boatCostText.text = boatCost.ToString();
            boatLevelText.text = "LEVEL " + boatLevel.ToString();
        }
        if (boatLevel == 2)
        {
            gm.maxBoatTime = 5;
            boatCost = 50;
            boatCostText.text = boatCost.ToString();
            boatLevelText.text = "LEVEL " + boatLevel.ToString();
        }
        if (boatLevel == 3)
        {
            gm.maxBoatTime = 4;
            boatCost = 50;
            boatCostText.text = boatCost.ToString();
            boatLevelText.text = "LEVEL " + boatLevel.ToString();
            boatUpgradeButton.gameObject.SetActive(false);
            boatmaxText.SetActive(true);
        }

    }
    public void CheckDock()
    {
        dockLevel = PlayerPrefs.GetInt("dockLevel");
        if (dockLevel == 0) { dockLevel = 1; PlayerPrefs.SetInt("dockLevel", dockLevel); }
        dockUpgradeButton.gameObject.SetActive(true);
        dockLevelText.gameObject.SetActive(true);
        dockmaxText.SetActive(false);
        if (dockLevel == 1)
        {
            gm.jumpChibyParentCapacity = 5;
            dockCost = 20;
            dockCostText.text = dockCost.ToString();
            dockLevelText.text = "LEVEL " + dockLevel.ToString();
        }
        if (dockLevel == 2)
        {
            gm.jumpChibyParentCapacity = 10;
            dockCost = 50;
            dockCostText.text = dockCost.ToString();
            dockLevelText.text = "LEVEL " + dockLevel.ToString();
        }
        if (dockLevel == 3)
        {
            gm.jumpChibyParentCapacity = 15;
            dockCost = 50;
            dockCostText.text = dockCost.ToString();
            dockLevelText.text = "LEVEL " + dockLevel.ToString();
            dockUpgradeButton.gameObject.SetActive(false);
            dockmaxText.SetActive(true);
        }

    }
    public void BoilerUpgrade()
    {
        if (gm.ghostCount >= boilerCost)
        {
            gm.ghostCount -= boilerCost;
            gm.ghostCountText.text = gm.ghostCount.ToString();
            boilerLevel += 1;
            PlayerPrefs.SetInt("boilerLevel", boilerLevel);
            CheckBoiler();
        }
    }
    public void StechUpgrade()
    {
        if (gm.ghostCount >= strechCost)
        {
            gm.ghostCount -= strechCost;
            gm.ghostCountText.text = gm.ghostCount.ToString();
            strechLevel += 1;
            PlayerPrefs.SetInt("strechLevel", strechLevel);
            CheckStrech();
        }
    }
    public void SmashUpgrade()
    {
        if (gm.ghostCount >= smashCost)
        {
            gm.ghostCount -= smashCost;
            gm.ghostCountText.text = gm.ghostCount.ToString();
            smashLevel += 1;
            PlayerPrefs.SetInt("smashLevel", smashLevel);
            CheckSmash();
        }
    }
    public void LavaUpgrade()
    {
        if (gm.ghostCount >= lavaCost)
        {
            gm.ghostCount -= lavaCost;
            gm.ghostCountText.text = gm.ghostCount.ToString();
            lavaLevel += 1;
            PlayerPrefs.SetInt("lavaLevel", lavaLevel);
            CheckLava();
        }
    }
    public void KebabUpgrade()
    {
        if (gm.ghostCount >= kebabCost)
        {
            gm.ghostCount -= kebabCost;
            gm.ghostCountText.text = gm.ghostCount.ToString();
            kebabLevel += 1;
            PlayerPrefs.SetInt("kebabLevel", kebabLevel);
            CheckKebab();
        }
    }
    public void SpeedUpgrade()
    {
        if (gm.ghostCount >= speedCost)
        {
            gm.ghostCount -= speedCost;
            gm.ghostCountText.text = gm.ghostCount.ToString();
            speedLevel += 1;
            PlayerPrefs.SetInt("speedLevel", speedLevel);
            CheckSpeed();
        }
    }
    public void ChainUpgrade()
    {
        if (gm.ghostCount >= chainCost)
        {
            gm.ghostCount -= chainCost;
            gm.ghostCountText.text = gm.ghostCount.ToString();
            chainLevel += 1;
            PlayerPrefs.SetInt("chainLevel", chainLevel);
            CheckChain();
        }
    }
    public void BoatUpgrade()
    {
        if (gm.ghostCount >= boatCost)
        {
            gm.ghostCount -= boatCost;
            gm.ghostCountText.text = gm.ghostCount.ToString();
            boatLevel += 1;
            PlayerPrefs.SetInt("boatLevel", boatLevel);
            CheckBoat();
        }
    }
    public void DockUpgrade()
    {
        if (gm.ghostCount >= dockCost)
        {
            gm.ghostCount -= dockCost;
            gm.ghostCountText.text = gm.ghostCount.ToString();
            dockLevel += 1;
            PlayerPrefs.SetInt("dockLevel", dockLevel);
            CheckDock();
        }
    }
}
