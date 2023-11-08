using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Buffs : MonoBehaviour
{
    public float EffectValue;

    public float Duration;

    public float cooldown;
    public float waitSec;

    public int possessionCount;
    Coroutine coroutine;

    private Player player;


    /*    public delegate void EfffectStart();
        public event EfffectStart onStart;

        public delegate void EfffectEnd();
        public event EfffectEnd onEnd;*/


    public UnityEvent startEffects;
    public UnityEvent endEffects;

    void Start()
    {
        player = GameManager.Instance.Player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waitSec > 0)
        {
            waitSec -= Time.deltaTime;
        }
        else if (waitSec < 0)
        {
            waitSec = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Activate();
        }
    }


    public void Activate()
    {
        if (coroutine == null && possessionCount > 0 && waitSec <= 0)
        {
            possessionCount--;
            coroutine = StartCoroutine(buffCoroutine(Duration));
            waitSec = cooldown;
            coroutine = null;
        }
    }

    IEnumerator buffCoroutine(float duration)
    {
        startEffects.Invoke();
        yield return new WaitForSeconds(duration);
        endEffects.Invoke();
    }


    //=================

    public void dmgBuffStart()
    {
        
        player.buffedDmg += EffectValue;

    }
    public void dmgBuffEnd()
    {
        player.buffedDmg -= EffectValue;
    }

    public void speedBuffStart()
    {

        player.buffedSpeed += EffectValue;

    }
    public void speedBuffEnd()
    {
        player.buffedSpeed -= EffectValue;
    }

}
