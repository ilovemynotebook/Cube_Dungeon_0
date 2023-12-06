using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossAIss
{
    protected BehaviorTree _tree;

    protected BossController _boss;

    protected SkillPattern _currentSkillPattern = new SkillPattern();



    public BossAIss(BossController boss)
    {
        _boss = boss;
        _tree = new BehaviorTree(SettingBT());
    }


    public void Update()
    {
        if(_boss.State != BossState.Die)
            StartBT();
    }

    private void StartBT()
    {
        _tree.Operate();
    }

    protected abstract INode SettingBT();

}

