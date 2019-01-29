using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Ent : EnemyControl
{

    public override void CoolDownActive(int _index, float _value)
    {
    }

    public override void DiscoverTarget(GameObject _target)
    {
        target = _target;
        Hold();
        move.MoveToThisObject(target);
        aniControl.LookAtTarget(target);
    }
}
