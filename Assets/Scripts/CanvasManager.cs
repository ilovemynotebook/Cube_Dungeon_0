using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    static public CanvasManager Instance;


    /// about Hud
    public HpBar hpBar;
    public HpBar staminaBar;

    public EquipmentsHud[] equipmentsHudsAndKey = new EquipmentsHud[7];
    public Buffs[] actives = new Buffs[4];


    public bool isUpgraded_weapon = false;
    public bool isUpgraded_shield = false;
    public bool isUpgraded_Item_0 = true;
    public bool isUpgraded_Item_1 = false;
    public bool isUpgraded_Item_2 = false;
    public bool isUpgraded_Item_3 = false;
    public int hpPotion = 0;
    public int staPotion = 0;
    public int dmgPotion = 0;
    public bool key = false;

    


    void Start()
    {
        equipmentsHudsAndKey = transform.GetChild(0).transform.Find("Equipments").GetComponentsInChildren<EquipmentsHud>();
        actives = transform.GetChild(0).transform.Find("Actives").GetComponentsInChildren<Buffs>();
        
        
        if (CanvasManager.Instance == null)
        {
            CanvasManager.Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        UpdateHud();
        GameManager.Instance.Player.GetComponent<Player>().itemsSetup();

    }

    public void UpdateHud()
    {
        equipmentsHudsAndKey[0].isUpgraded = isUpgraded_weapon;
        equipmentsHudsAndKey[1].isUpgraded = isUpgraded_shield;
        equipmentsHudsAndKey[2].isUpgraded = isUpgraded_Item_0;
        equipmentsHudsAndKey[3].isUpgraded = isUpgraded_Item_1;
        equipmentsHudsAndKey[4].isUpgraded = isUpgraded_Item_2;
        equipmentsHudsAndKey[5].isUpgraded = isUpgraded_Item_3;
        equipmentsHudsAndKey[6].isUpgraded = key;

        //다른 컴포넌트들은 캔버스 매니저로만 접근할 것이므로 이렇게 관리
        actives[0].possessionCount = hpPotion;
        actives[1].possessionCount = staPotion;
        actives[2].possessionCount = dmgPotion;
        actives[3].isUnlocked = isUpgraded_Item_3;

        for (int i = 0; i<7 ;i++)
        {
            equipmentsHudsAndKey[i].UpdateImage();
        }
        for(int i = 0; i<4; i++)
        {
            actives[i].UpdateCount();
        }

        hpBar.UpdateAutoHP();
    }

    public void ItemUpdateAfterUse()
    {
        hpPotion = actives[0].possessionCount;
        staPotion = actives[1].possessionCount;
        dmgPotion = actives[2].possessionCount;
    }
}

