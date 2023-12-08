using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEffectBehaviour : BossStateMachineBehaviour
{
    [SerializeField] private GameObject Effect;
    [SerializeField] private int ActivateFrame;
    [SerializeField] private int DisabledFrame;

    private bool _isActivated;

    private bool _isDisabled;

    private AnimationClip _clip;
    private int _currentFrame;

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

        if (_currentFrame == ActivateFrame && !_isActivated)
        {
            _isActivated = true;
            Debug.Log("Ω√¿€");
        }

        else if (_currentFrame == DisabledFrame && !_isDisabled)
        {
            _isDisabled = true;
        }
    }


    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        _isDisabled = false;
        _isActivated = false;
    }
}
