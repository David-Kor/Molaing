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


    private IEnumerator Move()
    {
        List<HitObject> hits = null;
        int i = 0;

        while (true)
        {
            transform.Translate(skillDirection * speed * Time.deltaTime);
            yield return null;
            //스킬을 오른쪽으로 사용한 경우
            if (skillDirection.x > 0)
            {
                if (prevPosition.x + interval <= transform.position.x)
                {
                    prevPosition = transform.position;
                    Instantiate(part_earthquake, transform);
                    hits = OnHitCheck(myCollider);      //이펙트를 생성할 때마다 충돌체크

                    if (hits == null) { continue; }

                    for (i = 0; i < hits.Count; i++)
                    {
                        hits[i].OnHitSkill(this);
                        hits[i].GetComponentInParent<ObjectControl>().ForceJump(bouncePower);
                    }
                }
            }
            //스킬을 왼쪽으로 사용한 경우
            else
            {
                if (prevPosition.x - interval >= transform.position.x)
                {
                    prevPosition = transform.position;
                    Instantiate(part_earthquake, transform);
                    hits = OnHitCheck(myCollider);      //이펙트를 생성할 때마다 충돌체크

                    if (hits == null) { continue; }

                    for (i = 0; i < hits.Count; i++)
                    {
                        hits[i].OnHitSkill(this);
                        hits[i].GetComponentInParent<ObjectControl>().ForceJump(bouncePower);
                    }
                }
            }
        }
    }


    public override void ActivateSkill()
    {
        transform.SetParent(null);
        myCollider = GetComponent<Collider2D>();
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        prevPosition = transform.position;
        Instantiate(part_earthquake, transform);
        StartCoroutine("Move");
    }

    public override void ReleaseSkill()
    {
        StopCoroutine("Move");
    }
}
