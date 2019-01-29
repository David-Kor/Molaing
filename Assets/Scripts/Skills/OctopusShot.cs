using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusShot : AttackSkill
{
    public float bulletSpeed;
    public GameObject oct_bullet;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = null;
    }

    public override void ActivateSkill()
    {
        Instantiate(oct_bullet, transform).GetComponent<Bullet>().ShotToDirection(this, skillDirection, bulletSpeed, lifeTime);
    }

    public override void ReleaseSkill()
    {
    }
}
