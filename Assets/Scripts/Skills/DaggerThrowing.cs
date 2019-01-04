using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerThrowing : AttackSkill
{
    public float daggerSpeed;
    public GameObject bullet_dagger;

    public override void ActivateSkill()
    {
        Instantiate(bullet_dagger, transform).GetComponent<Bullet>().ShotToDirection(this, skillDirection, daggerSpeed);
    }

    public override void ReleaseSkill()
    {
    }
}
