using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Octopus : EnemyControl
{
    private IEnumerator TargetAttack()
    {
        while (target != null)
        {
            yield return null;
            if (target == null) { break; }
            if (attack.coolTimerList[0] > 0 ||
                isDelay)
            {
                continue;
            }

            if ((target.transform.position.x - transform.position.x) > 0)
            {
                attack.UseSkill(0, Vector2.right);
            }
            else
            {
                attack.UseSkill(0, Vector2.left);
            }
        }
    }

    public override void CoolDownActive(int _index, float _value)
    {
        attack.CoolDownTimerActive(_index, _value);
    }


    public override void DiscoverTarget(GameObject _target)
    {
        target = _target;
        Hold();
        move.MoveToThisObject(target);
        aniControl.LookAtTarget(target);
        StartCoroutine("TargetAttack");
    }
}
