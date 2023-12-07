using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossExplotionParticle : BossParticle
{
    [SerializeField] private Collider _collider;

    private bool _isDamageDisabled;


    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            if (_isDamageDisabled)
                return;

            player.GetHit(_power);
            _isDamageDisabled = true; 
        }
    }
}
