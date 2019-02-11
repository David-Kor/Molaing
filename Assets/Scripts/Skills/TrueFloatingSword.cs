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

        int i;
        Bullet[] bullets = new Bullet[swordCount];

        for (i = 0; i < swordCount; i++)
        {
            bullets[i] = Instantiate(bullet_sword, transform).GetComponent<Bullet>().InitInfo(this, skillDirection, swordSpeed, lifeTime, false);
            yield return new WaitForSeconds(summonInterval);
        }

        yield return new WaitForSeconds(1.0f);

        for (i = 0; i < swordCount; i++)
        {
            bullets[i].ShootTrigger();
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
