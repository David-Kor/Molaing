using System.Collections.Generic;
using UnityEngine;

/*
 * Sample Child Of Skill
 */
public class Smite : AttackSkill
{
    public override void ActivateSkill()
    {
        transform.SetParent(null);
        List<HitObject> hitObjects = OnHitCheck(GetComponent<Collider2D>());

        for (int i = 0; i < hitObjects.Count; i++)
        {
            hitObjects[i].OnHitSkill(this);
        }
    }

    public override void ReleaseSkill()
    {
    }
}
