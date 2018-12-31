using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EnemyControl control;
    private Tackle tackle;
    private EnemyStatus status;

    void Start()
    {
        control = GetComponentInParent<EnemyControl>();
        status = GetComponentInParent<EnemyStatus>();
        tackle = new Tackle();
        tackle.SetDamage(status.tackleDamage);
        tackle.isKnockBack = true;
        tackle.knockBackPower = 0.05f;
        tackle.skillCaster = transform.parent.gameObject;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("HitPoint")) {

            if (col.transform.parent.CompareTag("Player")) {
                tackle.attackDirect = control.GetLookDirect();
                col.GetComponent<HitObject>().OnHitSkill(tackle);
            }

        }
    }
}
