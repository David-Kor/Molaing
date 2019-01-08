using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTanGang : Bullet
{
    public Sprite[] effects;              //검기 이펙트 이미지
    public float frameTime;

    private int index;
    private float e_timer;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        e_timer = frameTime;
        index = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isPullTrigger)
        {
            MoveBullet();
            e_timer += Time.deltaTime;

            //타이머가 frameTime 주기마다 이미지 갱신
            if (e_timer >= frameTime)
            {
                spriteRenderer.sprite = effects[index++];
                if (index >= effects.Length) { index = 0; }
                e_timer = 0;
            }
        }
    }


    protected override void OnHitBullet(HitObject hit)
    {
        if (hitCount >= skillInfo.maxHitCount)
        {
            Destroy(gameObject);
            return;
        }

        hitCount++;
        hit.OnHitSkill(skillInfo);
    }
}
