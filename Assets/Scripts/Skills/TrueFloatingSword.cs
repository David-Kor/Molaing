using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueFloatingSword : AttackSkill
{
    public float swordSpeed;
    public GameObject bullet_sword;

    public override void ActivateSkill()
    {
        Instantiate(bullet_sword, transform).GetComponent<Bullet>().ShotToDirection(this, skillDirection, swordSpeed, lifeTime);
    }

    public override void ReleaseSkill()
    {
    }
}
