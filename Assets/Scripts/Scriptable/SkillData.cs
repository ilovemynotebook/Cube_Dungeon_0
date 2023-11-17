using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class SkillData :MonoBehaviour
{
    [SerializeField]
    public string name;
    [SerializeField]
    public string description;
    public float cooldown;
    public float waitSec;
    public GameObject useItem;
    public int possessionCount;

    public UnityEvent Effects;

    public MonoBehaviour monoBehaviour;
    Coroutine coroutine;

    Player player;

    public float duration = 3;
    public float effectValue = 5;

    void Start()
    {
        
    }

    void Update()
    {

        FindPlayer();
        CoolDown();
    }

    void FindPlayer()
    {
        if(player == null)
        {
            player = GameManager.Instance._Player?.GetComponent<Player>();
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
    }







    /// <summary>
    /// skill effects to activate
    /// </summary>
    public void dmgUp()
    {
        Debug.Log(1);
        if (coroutine == null && possessionCount > 0 && waitSec <= 0)
        {
            possessionCount--;
            
            coroutine = monoBehaviour.StartCoroutine(dmgUpCoroutine());
            waitSec = cooldown;

        }
    }

    public void speedUp()
    {
        if (coroutine == null && possessionCount > 0 && waitSec <= 0)
        {
            possessionCount--;
            coroutine = monoBehaviour.StartCoroutine(speedUpCoroutine());
            waitSec = cooldown;
        }
    }

    IEnumerator dmgUpCoroutine()
    {
        player.buffedDmg += effectValue;
        yield return new WaitForSeconds(duration);
        player.buffedDmg -= effectValue;
    }

    IEnumerator speedUpCoroutine()
    {
        player.buffedSpeed += effectValue;
        yield return new WaitForSeconds(duration);
        player.buffedSpeed -= effectValue;
    }
}
