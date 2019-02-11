using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartOfEarthquake : MonoBehaviour
{
    public float highestValue;
    public float speed;

    private bool goUp = true;
    private float bouncePower;
    private string casterTag;
    private Skill earthquake;

    void Start()
    {
        transform.SetParent(null);
    }

    void Update()
    {
        if (goUp)
        {
            transform.localScale += Vector3.up * speed * Time.deltaTime;
            transform.localPosition += Vector3.up * 0.13f * speed * Time.deltaTime;
            if (transform.localScale.y >= highestValue)
            {
                goUp = false;
            }
        }
        else
        {
            transform.localScale += Vector3.down * speed * Time.deltaTime;
            transform.localPosition += Vector3.down * speed * 0.13f * Time.deltaTime;
            if (transform.localScale.y <= 0.1f)
            {
                Destroy(gameObject);
            }
        }    
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("HitPoint"))
        {
            HitObject hit = col.GetComponent<HitObject>();
            if (hit == null) { return; }
            if (col.transform.parent.tag == casterTag) { return; }
            Rigidbody2D rigid = hit.GetComponentInParent<Rigidbody2D>();
            rigid.velocity = (rigid.velocity.x * Vector2.right) + (bouncePower * Vector2.up);
            hit.OnHitSkill(earthquake);
        }
    }


    public void InitInfo(float bounce, string caster_tag, Skill skill_earthquake)
    {
        bouncePower = bounce;
        casterTag = caster_tag;
        earthquake = skill_earthquake;
    }
}
