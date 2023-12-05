using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHitBox : MonoBehaviour
{
    public List<GameObject> alreadyHit = new List<GameObject>();
    public float dmg;
    public GameObject caster;
    public float existTime = 0.3f;
    public bool isOneHit = true;

    private float lifeTime = 0;

    public void SetUp(float _dmg, float _existTime, bool _isOneHit, GameObject _caster)
    {
        dmg = _dmg;
        existTime = _existTime;
        isOneHit = _isOneHit;
        caster = _caster;
    }

    void Start()
    {
        alreadyHit.Clear();
    }

    void Update()
    {
        DestroyTimer();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy" && !alreadyHit.Contains(other.gameObject))
        {
            Enemy en = other.GetComponent<Enemy>();
            //en.GetHit(currentWeapon.Dmg + buffedDmg);
            en.GetHit(dmg);
            en.KnockBack((other.transform.position - caster.transform.position).normalized * 15);

            if(isOneHit) alreadyHit.Add(other.gameObject);
            //Debug.Log(other.name);
        }


    }

    void DestroyTimer()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime >= existTime) Destroy(transform.parent.gameObject);
    }
}
