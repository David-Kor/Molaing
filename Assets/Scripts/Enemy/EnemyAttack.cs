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
        tackle = new Tackle
        {
            isKnockBack = true,
            knockBackPower = status.tackleKnockBackPower,
            skillCaster = transform.parent.gameObject
        };
        tackle.SetDamage(status.tackleDamage);
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
