using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Boss5TeleportStateMachine : BossStateMachineBehaviour
{
    [SerializeField] private BossSkillData _bossSkillData;

    private Boss5Controller _boss5;

    private int _currentFrame;

    private AnimationClip _clip;




    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        animator.TryGetComponent(out _boss5);
        float animeLength = stateInfo.length;
        _boss.AddWaingTimer(animeLength);
        _clip = animator.GetCurrentAnimatorClipInfo(layerIndex)[0].clip;
    }


    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _currentFrame = (int)(stateInfo.normalizedTime * (_clip.length * _clip.frameRate));

        if (_currentFrame == _bossSkillData.ActivateFrame && !_bossSkillData._isActivated)
        {
            _bossSkillData._isActivated = true;
            _boss5.TeleportClass.AttackTrigger.SkillStart();
            Debug.Log("Ω√¿€");
        }

        else if (_currentFrame == _bossSkillData.DisabledFrame && !_bossSkillData._isDisabled)
        {
            _bossSkillData._isDisabled = true;
            _boss5.TeleportClass.AttackTrigger.SkillEnd();
        }
    }


    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        _bossSkillData._isDisabled = false;
        _bossSkillData._isActivated = false;
    }
}
