using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    private float _power;

    private bool _isAttackClear;
    public void SetPower(float power)
    {
        _power = power;
    }

    private void Start()
    {
        Destroy(gameObject, 0.5f);
    }


    public void OnTriggerStay(Collider other)
    {
        if (_isAttackClear)
            return;

        if(other.TryGetComponent(out Character character))
        {
            character.GetHit(_power);
            _isAttackClear = true;
        }
    }
}
