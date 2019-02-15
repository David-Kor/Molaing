using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Earthquake : AttackSkill
{
    public float bouncePower;
    public float speed;
    public float interval;
    public GameObject part_earthquake;

    private Vector2 prevPosition;
    private Collider2D myCollider;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = null;
    }


    /* 충돌 체크 */
    private void HitCheck()
    {
        List<HitObject> hits = null;
        int i = 0;

        hits = OnHitCheck(myCollider);      //이펙트를 생성할 때마다 충돌체크

        if (hits == null) { return; }

        for (i = 0; i < hits.Count; i++)
        {
            hits[i].OnHitSkill(this);
            hits[i].GetComponentInParent<ObjectControl>().ForceJump(bouncePower);
        }
    }


    private IEnumerator Move()
    {
        SpriteRenderer sprite;

        for (int i = 0; ;)
        {
            transform.Translate(skillDirection * speed * Time.deltaTime);
            yield return null;
            //스킬을 오른쪽으로 사용한 경우
            if (skillDirection.x > 0)
            {
                if (prevPosition.x + interval <= transform.position.x)
                {
                    prevPosition = transform.position;
                    sprite = Instantiate(part_earthquake, transform).GetComponent<SpriteRenderer>();
                    sprite.GetComponent<SpriteRenderer>().sortingOrder = i++;
                    HitCheck();
                }
            }
            //스킬을 왼쪽으로 사용한 경우
            else
            {
                if (prevPosition.x - interval >= transform.position.x)
                {
                    prevPosition = transform.position;
                    sprite = Instantiate(part_earthquake, transform).GetComponent<SpriteRenderer>();
                    sprite.flipX = true;
                    sprite.GetComponent<SpriteRenderer>().sortingOrder = i++;
                    HitCheck();
                }
            }
        }
    }


    public override void ActivateSkill()
    {
        transform.SetParent(null);
        transform.position += Vector3.up * 0.15f;
        myCollider = GetComponent<Collider2D>();
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        prevPosition = transform.position;
        Instantiate(part_earthquake, transform).GetComponent<SpriteRenderer>().flipX = skillDirection.x < 0;
        HitCheck();
        StartCoroutine("Move");
    }

    public override void ReleaseSkill()
    {
        StopCoroutine("Move");
    }
}
