using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerThrowing : AttackSkill
{
    public float daggerSpeed;
    public GameObject bullet_dagger;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = null;
    }

    public override void ActivateSkill()
    {
        Instantiate(bullet_dagger, transform).GetComponent<Bullet>().ShotToDirection(this, skillDirection, daggerSpeed, lifeTime);
    }

    public override void ReleaseSkill()
    {
    }
}
