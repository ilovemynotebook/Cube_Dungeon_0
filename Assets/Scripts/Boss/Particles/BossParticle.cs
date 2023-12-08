using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossParticle : MonoBehaviour
{
    [Header("ī�޶� ��鸲 ���� �ð�")]
    [SerializeField] private float _shakeDuration = 0.5f;

    [Header("ī�޶� ��鸲 ����")]
    [SerializeField] private float _shakeAmplitude = 1f;

    [Header("ī�޶� ��鸲 ��")]
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
