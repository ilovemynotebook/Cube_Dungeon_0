using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachineBehaviour : StateMachineBehaviour
{
    protected Boss _boss;
    public void Init(Boss boss)
    {
        _boss = boss;
    }
}
