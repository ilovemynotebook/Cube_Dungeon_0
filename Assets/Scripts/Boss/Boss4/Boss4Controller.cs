using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Boss4Controller : BossController
{
    protected override void Update()
    {
        base.Update();
    }


    protected override void Init()
    {
        base.Init();
        _ai = new Boss4AI(this);
    }
}
