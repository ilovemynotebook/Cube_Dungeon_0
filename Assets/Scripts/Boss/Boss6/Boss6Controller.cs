using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Boss6Controller : BossController
{
     

    protected override void Update()
    {
        base.Update();
    }


    protected override void Init()
    {
        base.Init();
        _ai = new Boss6AI(this);
    }


  
    public override SkillPattern GetUsableSkill()
    {
        _usableSkillList.Clear();

        foreach (SkillPattern pattern in _skillPatterns)
        {
            if (0 < pattern.CurrentCoolTime)
            {
                Debug.Log("ÄðÅ¸ÀÓ ¾ÆÁ÷");
                continue;
            }


            if (pattern.Distance < TargetDistance)
            {
                Debug.Log("°Å¸®°¡ ¸Ø");
                continue;
            }

            _usableSkillList.Add(pattern);
        }


        if (_usableSkillList.Count == 0)
            return default;

        int randInt = Random.Range(0, _usableSkillList.Count);

        return _usableSkillList[randInt];
    }
}
