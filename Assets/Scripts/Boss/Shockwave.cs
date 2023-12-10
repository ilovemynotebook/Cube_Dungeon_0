using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Shockwave : MonoBehaviour
{
    [Header("Ä«¸Þ¶ó Èçµé¸² ÁøÆø")]
    [SerializeField] private float _shakeDuration = 0.5f;

    [Header("Ä«¸Þ¶ó Èçµé¸² ÁøÆø")]
    [SerializeField] private float _shakeAmplitude = 1f;

    [Header("Ä«¸Þ¶ó Èçµé¸² ºóµµ")]
    [SerializeField] private float _shakeFrequency = 0.5f;

    [Space(10)]

    [SerializeField] private AudioClip _clip;

    [Space(10)]

    [SerializeField] private float _destroyTime;

    private AudioSource _source;

    private float _power;

    private bool _isAttackClear;

    public void SetPower(float power)
    {
        _power = power;
    }

    private void Start()
    {
        CinemachineCamera.Instance.CameraShake(_shakeDuration, _shakeAmplitude, _shakeFrequency);

        _source = GetComponent<AudioSource>();

        if(_clip != null)
            _source.PlayOneShot(_clip);

        Destroy(gameObject, _destroyTime);
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
