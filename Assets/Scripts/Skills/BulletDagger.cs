using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDagger : Bullet
{
    protected override void OnHitBullet(HitObject hit)
    {
        if (hitCount >= skillInfo.maxHitCount) { return; }

        hitCount++;
        hit.OnHitSkill(skillInfo);
        Destroy(gameObject);
    }
}
