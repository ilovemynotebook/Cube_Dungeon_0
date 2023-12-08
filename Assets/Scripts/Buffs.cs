using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;


public class Buffs : MonoBehaviour
{
    public float EffectValue;

    

    public float cooldown;
    [HideInInspector]
    public float waitSec;

    public int possessionCount;
    Coroutine coroutine;

    private Player player;

    public bool isCostNeeded = true;

    /*    public delegate void EfffectStart();
        public event EfffectStart onStart;

        public delegate void EfffectEnd();
        public event EfffectEnd onEnd;*/


    public UnityEvent startEffects;
    public UnityEvent endEffects;

    TextMeshProUGUI countText;
    Image cooldownImage;
    GameObject lockImage;

    public bool isUsable = false;
    public bool isUnlocked = true;

    [Header("for buff items only")]
    public float Duration;
    public GameObject Icon;



    void Start()
    {
        cooldownImage = transform.Find("cooldown").GetComponent<Image>();
        countText = transform.Find("count").GetComponent<TextMeshProUGUI>();
        if (!isCostNeeded) Destroy(countText.gameObject);
        lockImage = transform.Find("lock").gameObject;

        UpdateCount();
        UsableCheck();
    }

    // Update is called once per frame
    void Update()
    {
        FindPlayer();
        CoolDown();
    }

    public void UsableCheck()
    {
        if(isUnlocked && (!isCostNeeded || possessionCount > 0))
        {
            isUsable = true;
        }
        else isUsable = false;

        lockImage?.SetActive(!isUsable);
    }
    public void UpdateCount()
    {
        if(countText != null)
        countText.text = possessionCount.ToString();
        UsableCheck();
    }
    void FindPlayer()
    {
        if (player == null)
        {
            player = GameManager.Instance.Player?.GetComponent<Player>();
        }
    }

    void CoolDown()
    {
        if (waitSec > 0)
        {
            waitSec -= Time.deltaTime;
        }
        else if (waitSec < 0)
        {
            waitSec = 0;
        }
        cooldownImage.fillAmount = waitSec / cooldown;

    }

    public void Activate()
    {
        if (isCostNeeded && possessionCount < 1)
        {
            return;
        }
        else if (!isUsable || !isUnlocked) return;
        else if (coroutine == null && waitSec <= 0)
        {
            possessionCount--;
            UpdateCount();
            CanvasManager.Instance.ItemUpdateAfterUse();
            coroutine = StartCoroutine(buffCoroutine(Duration));
            waitSec = cooldown;
            coroutine = null;
        }
    }

    IEnumerator buffCoroutine(float duration)
    {
        startEffects.Invoke();
        if(Icon != null)Icon.SetActive(true);
        yield return new WaitForSeconds(duration);
        endEffects?.Invoke();
        if (Icon != null)Icon.SetActive(false);
    }



    //=================effects

    public void HPHeal()
    {
        player.hp += EffectValue;
        GameManager.Instance.Player.GetComponent<Player>().sounds.Active_AS.Play();

        CanvasManager.Instance.hpBar.UpdateValue(player.hp, player.mhp);
    }

    public void STHeal()
    {
        player.sta += EffectValue;
        GameManager.Instance.Player.GetComponent<Player>().sounds.Active_AS.Play();

        CanvasManager.Instance.staminaBar.UpdateValue(player.sta, player.msta);
    }

    public void dmgBuffStart()
    {
        player.buffedDmg += EffectValue;
        GameManager.Instance.Player.GetComponent<Player>().sounds.Active_AS.Play();
    }
    public void dmgBuffEnd()
    {
        player.buffedDmg -= EffectValue;
    }

    public void speedBuffStart()
    {
        player.buffedSpeed += EffectValue;
        GameManager.Instance.Player.GetComponent<Player>().sounds.Active_AS.Play();
    }
    public void speedBuffEnd()
    {
        player.buffedSpeed -= EffectValue;
    }

}
