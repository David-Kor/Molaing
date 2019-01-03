using System.Collections.Generic;
using UnityEngine;

/*
 * Sample Child Of Skill
 */
public class Smite : AttackSkill
{
    public override void ActivateSkill()
    {
        List<HitObject> hitObjects = OnHitCheck();

        for (int i = 0; i < hitObjects.Count; i++)
        {
            hitObjects[i].OnHitSkill(this);
        }
    }

    public override void ReleaseSkill()
    {

    }
}
