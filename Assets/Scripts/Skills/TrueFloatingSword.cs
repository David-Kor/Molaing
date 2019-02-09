using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueFloatingSword : AttackSkill
{
    public float swordSpeed;
    public int swordCount;
    public float summonInterval;
    public GameObject bullet_sword;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = null;
    }


    private IEnumerator SummonSword()
    {
        while (!f_delayEnd) { yield return null; }

        for (int i = 0; i < swordCount; i++)
        {
            Instantiate(bullet_sword, transform).GetComponent<Bullet>().ShotToDirection(this, skillDirection, swordSpeed, lifeTime);
            yield return new WaitForSeconds(summonInterval);
        }
    }


    public override void ActivateSkill()
    {
        StartCoroutine("SummonSword");
    }

    public override void ReleaseSkill()
    {
    }
}
