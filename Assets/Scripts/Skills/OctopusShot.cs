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

    private IEnumerator Shoot()
    {
        //선딜이 끝날때까지 기다렸다가 발사
        while (!f_delayEnd) { yield return null; }
        Instantiate(oct_bullet, transform).GetComponent<Bullet>().InitInfo(this, skillDirection, bulletSpeed, lifeTime);
    }


    public override void ActivateSkill()
    {
        StartCoroutine("Shoot");
    }

    public override void ReleaseSkill()
    {
    }
}
