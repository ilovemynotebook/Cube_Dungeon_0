using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CameraShakeData
{
    [Header("카메라 흔들림 지속 시간")]
    [SerializeField] private float _shakeDuration = 0.5f;
    public float ShakeDuration => _shakeDuration;

    [Header("카메라 흔들림 진폭")]
    [SerializeField] private float _shakeAmplitude = 1f;
    public float ShakeAmplitude => _shakeAmplitude;

    [Header("카메라 흔들림 빈도")]
    [SerializeField] private float _shakeFrequency = 0.5f;
    public float ShakeFrequency => _shakeFrequency;


    [Tooltip("활성화 프레임")]
    [SerializeField] private int _activateFrame;
    public int ActivateFrame => _activateFrame;

    [Tooltip("비 활성화 프레임")]
    [SerializeField] private int _disabledFrame;
    public int DisabledFrame => _disabledFrame;


    [HideInInspector] public bool _isActivated;
    [HideInInspector] public bool _isDisabled;
}

public class BossCameraShakeMachine : BossStateMachineBehaviour
{
    [SerializeField] private CameraShakeData _shakeData;

    private int _currentFrame;

    private AnimationClip _clip;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        float animeLength = stateInfo.length;
        _boss.AddWaitTimer(animeLength);

        _clip = animator.GetCurrentAnimatorClipInfo(layerIndex)[0].clip;
    }


    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _currentFrame = (int)(stateInfo.normalizedTime * (_clip.length * _clip.frameRate));

        if (_currentFrame == _shakeData.ActivateFrame && !_shakeData._isActivated)
        {
            CinemachineCamera.Instance.CameraShake(_shakeData.ShakeDuration, _shakeData.ShakeAmplitude, _shakeData.ShakeFrequency);
            _shakeData._isActivated = true;
        }

        else if (_currentFrame == _shakeData.DisabledFrame && !_shakeData._isDisabled)
        {
            _shakeData._isDisabled = true;
        }
    }


    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        _shakeData._isDisabled = false;
        _shakeData._isActivated = false;
    }
}
