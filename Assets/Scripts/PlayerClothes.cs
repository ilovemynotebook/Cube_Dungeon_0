using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClothes : MonoBehaviour
{
    [Header("6 items. 2 is obsolete but included for unity")]
    public GameObject[] unUpgraded;
    public GameObject[] upgraded;
    //(모자는 항상 Upgraded일 것이다. hud 통일성 때문이다.)


    [Header("Set them in hierarchy. in order. 0~5")]
    public GameObject Hats;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void itemApply()
    {
        if(CanvasManager.Instance.isUpgraded_weapon)
        {
            unUpgraded[0]?.SetActive(false);
            upgraded[0]?.SetActive(true);
        }
        else
        {
            unUpgraded[0]?.SetActive(true);
            upgraded[0]?.SetActive(false);
        }

        if (CanvasManager.Instance.isUpgraded_shield)
        {
            unUpgraded[1]?.SetActive(false);
            upgraded[1]?.SetActive(true);
        }
        else
        {
            unUpgraded[1]?.SetActive(true);
            upgraded[1]?.SetActive(false);
        }

        if (CanvasManager.Instance.isUpgraded_Item_0)
        {
            //unUpgraded[2]?.SetActive(false);
            //upgraded[2]?.SetActive(true);
        }
        else
        {
            //unUpgraded[2]?.SetActive(true);
            //upgraded[2]?.SetActive(false);
        }

        if (CanvasManager.Instance.isUpgraded_Item_1)
        {
            unUpgraded[3]?.SetActive(false);
            upgraded[3]?.SetActive(true);
        }
        else
        {
            unUpgraded[3]?.SetActive(true);
            upgraded[3]?.SetActive(false);
        }

        if (CanvasManager.Instance.isUpgraded_Item_2)
        {
            if (unUpgraded[4] != null)
                unUpgraded[4]?.SetActive(false);
            upgraded[4]?.SetActive(true);
        }
        else
        {
            if (unUpgraded[4] != null)
                unUpgraded[4]?.SetActive(true);
            upgraded[4]?.SetActive(false);
        }

        if (CanvasManager.Instance.isUpgraded_Item_3)
        {
            unUpgraded[5]?.SetActive(false);
            upgraded[5]?.SetActive(true);
        }
        else
        {
            unUpgraded[5]?.SetActive(true);
            upgraded[5]?.SetActive(false);
        }

        for (int i =0; i< 6;i++){
            Hats.transform.GetChild(i).gameObject.SetActive(false);
        }
        Hats.transform.GetChild(CanvasManager.Instance.whatHat).gameObject.SetActive(true);
    }
}
