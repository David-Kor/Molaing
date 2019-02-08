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


    private IEnumerator Move()
    {
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
                    Instantiate(part_earthquake, transform).GetComponent<PartOfEarthquake>().InitInfo(bouncePower, skillCaster.tag, this);
                }
            }
            //스킬을 왼쪽으로 사용한 경우
            else
            {
                if (prevPosition.x - interval >= transform.position.x)
                {
                    prevPosition = transform.position;
                    Instantiate(part_earthquake, transform).GetComponent<PartOfEarthquake>().InitInfo(bouncePower, skillCaster.tag, this);
                }
            }
        }
    }


    public override void ActivateSkill()
    {
        transform.SetParent(null);
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        prevPosition = transform.position;
        Instantiate(part_earthquake, transform).GetComponent<PartOfEarthquake>().InitInfo(bouncePower, skillCaster.tag, this);
        StartCoroutine("Move");
    }

    public override void ReleaseSkill()
    {
        StopCoroutine("Move");
    }
}
