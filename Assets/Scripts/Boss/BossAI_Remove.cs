using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BossAIs
{
    protected BehaviorTree _tree;

    protected BossController _boss;

    protected SkillPattern _currentSkillPattern = new SkillPattern();



    public BossAIs(BossController boss)
    {
        _boss = boss;
        _tree = new BehaviorTree(SettingBT());
    }


    public void Update()
    {
        StartBT();
    }

    private void StartBT()
    {
        _tree.Operate();
    }

    protected abstract INode SettingBT();

}
