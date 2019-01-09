using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingSword : AttackSkill
{
    public float turnSpeed;                 //회전 속도(초당 회전 수)
    public float collisionCheckInterval;  //충돌 검사 간격

    private float timer;
    private List<HitObject> hitObjects;
    private Collider2D[] colliders;
    private SpriteRenderer[] childRenderers;

    void Start()
    {
        timer = 0;
        colliders = GetComponentsInChildren<Collider2D>();
    }

    void Update()
    {
        if (!f_delayEnd)
        {
            WaitFirstDelayWithSprites();
            return;
        }

        SkillProduction();
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime * 360);
        timer += Time.deltaTime;

        if (timer >= collisionCheckInterval)
        {
            timer = 0;
            CollisionCheck();
        }
    }

    private void WaitFirstDelayWithSprites()
    {
        WaitFirstDelay();
        if (childRenderers == null)
        {
            childRenderers = GetComponentsInChildren<SpriteRenderer>();
        }

        if (f_delayEnd)
        {
            for (int i = 0; i < childRenderers.Length; i++)
            {
                childRenderers[i].enabled = true;
            }
        }
    }


    private void CollisionCheck()
    {
        int i, j;

        for (i = 0; i < colliders.Length; i++)
        {
            hitObjects = OnHitCheck(colliders[i]);

            for (j = 0; j < hitObjects.Count; j++)
            {
                hitObjects[j].OnHitSkill(this);
            }
        }
    }


    public override void ActivateSkill()
    {
        timer = 0;
        childRenderers = GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < childRenderers.Length; i++)
        {
            childRenderers[i].enabled = false;
        }
    }

    public override void ReleaseSkill()
    {
    }
}
