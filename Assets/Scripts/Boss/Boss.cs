using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss
{
    private BehaviorTree _tree;

    private Boss _boss;

    private SkillPattern _currentSkillPattern = new SkillPattern();



    public Boss(Boss boss)
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
