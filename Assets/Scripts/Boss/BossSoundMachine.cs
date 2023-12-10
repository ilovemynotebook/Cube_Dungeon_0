using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SoundData
{
    [Tooltip("리소스 폴더에서 불러올 오디오")]
    [SerializeField] private string _audioName;
    public string AudioName => _audioName;

    [Tooltip("활성화 프레임")]
    [SerializeField] private int _activateFrame;
    public int ActivateFrame => _activateFrame;

    [Tooltip("비 활성화 프레임")]
    [SerializeField] private int _disabledFrame;
    public int DisabledFrame => _disabledFrame;


    [HideInInspector] public bool _isActivated;
    [HideInInspector] public bool _isDisabled;
}

public class BossSoundMachine : BossStateMachineBehaviour
{
    [SerializeField] private SoundData _soundData;

    
    [SerializeField] [Range(0,1)] private float _volume = 1;

    [SerializeField][Range(0, 2)] private float _pitch = 1;


    [SerializeField] private bool _isExitSoundStopped;

    private int _currentFrame;

    private AnimationClip _clip;
    private AudioClip _audioClip;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        float animeLength = stateInfo.length;
        _boss.AddWaitTimer(animeLength);

        _clip = animator.GetCurrentAnimatorClipInfo(layerIndex)[0].clip;
        _audioClip = (AudioClip)Resources.Load("Audios/" + _soundData.AudioName);

        _boss.AudioSource.volume = _volume;
        _boss.AudioSource.pitch = _pitch;
    }


    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _currentFrame = (int)((stateInfo.normalizedTime % 1) * (_clip.length * _clip.frameRate));

        if (_currentFrame == _soundData.ActivateFrame && !_soundData._isActivated)
        {

            _boss.AudioSource.PlayOneShot(_audioClip);
            _soundData._isActivated = true;
        }

        else if (_currentFrame == _soundData.DisabledFrame && !_soundData._isDisabled)
        {
            _boss.AudioSource.Stop();
            _soundData._isDisabled = true;
        }
    }


    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        
        _soundData._isDisabled = false;
        _soundData._isActivated = false;

        if(_isExitSoundStopped)
            _boss.AudioSource.Stop();
    }
}
