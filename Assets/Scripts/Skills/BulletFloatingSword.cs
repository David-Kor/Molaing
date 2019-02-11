using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFloatingSword : Bullet
{
    public  GameObject target;

    private GameObject caster;
    private float angle;
    private float timeStream;
    private Vector2 targetPosition;
    private Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        caster = skillInfo.skillCaster;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        StartCoroutine("TimeToLive");
    }

    void Update()
    {

        if (target != null && isPullTrigger)
        {
            targetPosition = target.transform.position - transform.position;
            targetPosition.Normalize();
            rigid.velocity += targetPosition * speed * Time.deltaTime;
            LootAt();
        }

        if (caster != null)
        {
            timeStream += Time.deltaTime * speed;
            if (target == null || !isPullTrigger)
            {
                rigid.velocity = Vector2.zero;
                targetPosition.Set(caster.transform.position.x + 0.6f * Mathf.Cos(timeStream),
                    caster.transform.position.y + 0.6f * Mathf.Sin(timeStream));
                targetPosition -= (Vector2)transform.position;
                transform.Translate(targetPosition * speed * Time.deltaTime, Space.World);
                LootAt();
            }
        }
        else
        {
            StopCoroutine("TimeToLive");
            Destroy(gameObject);
            return;
        }
    }


    private void LootAt()
    {
        angle = Mathf.Rad2Deg * Mathf.Atan2(targetPosition.y, targetPosition.x) - 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }


    private IEnumerator TimeToLive()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }


    protected override void OnHitBullet(HitObject hit)
    {
        hitCount++;
        hit.OnHitSkill(skillInfo);
    }
}
