using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingSword : AttackSkill
{
    public float turnSpeed;                 //회전 속도(초당 회전 수)

    private SpriteRenderer[] childRenderers;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = null;
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


    private void OnTriggerEnter2D(Collider2D col)
    {
        //시전시간이 끝나지 않았으면 무시
        if (!f_delayEnd) { return; }
        if (col.CompareTag("HitPoint"))
        {
            HitObject hit = col.GetComponent<HitObject>();
            if (hit == null) { return; }
            if (hit.transform.parent.tag == skillCaster.tag) { return; }      //시전자라면 무시
            col.GetComponent<HitObject>().OnHitSkill(this);
        }
    }


    public override void ActivateSkill()
    {
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
