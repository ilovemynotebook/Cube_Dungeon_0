using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BossExplotionParticle : BossParticle
{
    [SerializeField] private Collider _collider;

    [SerializeField] private AudioClip _clip;

    private AudioSource _source;

    private bool _isDamageDisabled;

    public override void Start()
    {
        base.Start();

        _source = GetComponent<AudioSource>();

        if (_clip != null)
            _source.PlayOneShot(_clip);

    }

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
