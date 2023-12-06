using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Boss3Controller : BossController
{
    protected override void Update()
    {
        base.Update();
    }


    protected override void Init()
    {
        base.Init();
        _ai = new Boss3AI(this);
    }
}
