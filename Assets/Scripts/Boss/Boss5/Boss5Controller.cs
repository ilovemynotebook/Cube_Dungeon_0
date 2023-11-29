using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Boss5Controller : BossController
{
    [SerializeField] private float _teleportTime;
    public float TeleportTime => _teleportTime;



    protected override void Init()
    {
        base.Init();
        _ai = new Boss5AI(this);
    }


}
