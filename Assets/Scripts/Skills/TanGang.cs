using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanGang : AttackSkill
{
    public float speed;
    public GameObject bullet_TanGang;

    public override void ActivateSkill()
    {
        Instantiate(bullet_TanGang, transform).GetComponent<Bullet>().ShotToDirection(this, skillDirection, speed, lifeTime);
    }

    public override void ReleaseSkill()
    {
    }
}
