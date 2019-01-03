using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : ObjectControl
{
    private Vector2 lookDirect;

    private GameObject target;  // 공격 대상
    private EnemyAnimation aniControl;
    private EnemyMove move;
    private EnemyStatus status;

    void Start()
    {
        aniControl = GetComponentInChildren<EnemyAnimation>();
        move = GetComponent<EnemyMove>();
        status = GetComponent<EnemyStatus>();
        lookDirect = aniControl.GetDirect();
        //적끼리의 물리적 충돌 무시 (밀림현상 방지)
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Enemy"), true);
    }


    /* 타겟 발견 */
    public void DiscoverTarget(GameObject _target)
    {
        target = _target;
        move.MoveToThisObject(target);
        aniControl.LookAtTarget(target);
    }


    /* 타겟 분실 */
    public void TargetLost()
    {
        target = null;
        move.StopMove();
        aniControl.Standing();
    }


    /* 순찰 중 */
    public void Patrol(Vector2 _direct) { aniControl.PlayPatrol(_direct); }


    /* 대기 중 */
    public void Hold() { aniControl.Standing(); }


    /* 공격당할 때 호출되는 함수 */
    public override void OnHitAttack(AttackSkill _skill)
    {
        aniControl.ShowGetDamage();
        //스탯에 피해량(damage) 정보를 넘김
        status.TakeDamage(_skill.damage);

        //현재 체력이 바닥났을 경우
        if (status.currentHP <= 0)
        {
            Dead(_skill.skillCaster);
        }

        DiscoverTarget(_skill.skillCaster);
        if (_skill.isKnockBack) { move.KnockBack(_skill.skillDirect * _skill.knockBackPower); }
    }


    public Vector2 GetLookDirect() { return lookDirect; }


    public void SetLookDirect(Vector2 _direct) { lookDirect = _direct; }


    /* HP가 0이하로 떨어졌을 때 호출 됨 */
    public void Dead(GameObject killer)
    {
        if (killer.CompareTag("Player"))
        {
            killer.GetComponent<PlayerControl>().TakeEXP(status.exp);
        }
        Destroy(gameObject);
    }
}
