using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossParticle : MonoBehaviour
{
    [Header("카메라 흔들림 지속 시간")]
    [SerializeField] private float _shakeDuration = 0.5f;

    [Header("카메라 흔들림 진폭")]
    [SerializeField] private float _shakeAmplitude = 1f;

    [Header("카메라 흔들림 빈도")]
    [SerializeField] private float _shakeFrequency = 0.5f;


    [SerializeField] protected float _disabledTime;

    protected float _power;

    protected BossController _boss;

    public void Init(BossController boss, float power)
    {
        _boss = boss;
       _power = power;
    }

    public virtual void Start()
    {
        Destroy(gameObject, _disabledTime);
        CinemachineCamera.Instance.CameraShake(_shakeDuration, _shakeAmplitude, _shakeFrequency);
    }
}
