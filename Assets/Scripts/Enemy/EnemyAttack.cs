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
        tackle = gameObject.AddComponent<Tackle>();
        tackle.isKnockBack = true;
        tackle.knockBackPower = status.tackleKnockBackPower;
        tackle.skillCaster = transform.parent.gameObject;
        tackle.damage = status.tackleDamage;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("HitPoint")) {

            if (col.transform.parent.CompareTag("Player")) {
                tackle.skillDirect = control.GetLookDirect();
                col.GetComponent<HitObject>().OnHitSkill(tackle);
            }

        }
    }
}
