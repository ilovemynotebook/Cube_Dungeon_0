using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BossSkillData
{
    [Tooltip("공격 활성화 프레임")]
    [SerializeField] private int _activateFrame;
    public int ActivateFrame => _activateFrame;

    [Tooltip("공격 비 활성화 프레임")]
    [SerializeField] private int _disabledFrame;
    public int DisabledFrame => _disabledFrame;

    [Tooltip("보스 스크립트에 존재하는 스킬 패턴 인덱스를 가져오는 함수")]
    [SerializeField] private int _skillPatternsIndex;
    public int SkillPatternsIndex => _skillPatternsIndex;

    [HideInInspector] public bool _isActivated;
    [HideInInspector] public bool _isDisabled;
}

public class BossSkillStateMachine : BossStateMachineBehaviour
{
    private int _currentFrame;

    private AnimationClip _clip;

    [SerializeField] private BossSkillData _bossSkillData;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
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
            _boss.SkillPatterns[_bossSkillData.SkillPatternsIndex].AttackTrigger.SkillStart();
            Debug.Log("시작");
        }

        else if (_currentFrame == _bossSkillData.DisabledFrame && !_bossSkillData._isDisabled)
        {
            _bossSkillData._isDisabled = true;
            _boss.SkillPatterns[_bossSkillData.SkillPatternsIndex].AttackTrigger.SkillEnd();
        }
    }


    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        _bossSkillData._isDisabled = false;
        _bossSkillData._isActivated = false;
    }
}
