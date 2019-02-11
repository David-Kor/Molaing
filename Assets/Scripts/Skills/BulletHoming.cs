using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHoming : MonoBehaviour
{
    private BulletFloatingSword bullet;
    private GameObject target;

    void Start()
    {
        bullet = GetComponentInParent<BulletFloatingSword>();
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (target != null) { return; }

        if (col.CompareTag("HitPoint"))
        {
            if (!col.transform.parent.CompareTag(bullet.GetSkillInfo().skillCaster.tag))
            {
                target = col.transform.parent.gameObject;
                bullet.target = target;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (target == null) { return; }

        if (col.CompareTag("HitPoint"))
        {
            if (target != null && target.CompareTag(col.transform.parent.tag))
            {
                target = null;
                bullet.target = null;
            }
        }
    }
}
