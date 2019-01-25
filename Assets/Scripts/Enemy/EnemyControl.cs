using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyControl : ObjectControl
{
    protected Vector2 lookDirection;
    protected GameObject target;  // 공격 대상
    protected EnemyAnimation aniControl;
    protected EnemyMove move;
    protected EnemyStatus status;
    protected Collider2D[] allGround;
    protected Collider2D myCollider;

    void Start()
    {
        aniControl = GetComponentInChildren<EnemyAnimation>();
        move = GetComponent<EnemyMove>();
        status = GetComponent<EnemyStatus>();
        lookDirection = aniControl.GetDirection();
        //적끼리의 물리적 충돌 무시 (밀림현상 방지)
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Enemy"), true);
        myCollider = GetComponent<Collider2D>();

        GameObject[] grounds = GameObject.FindGameObjectsWithTag("Ground");
        allGround = new Collider2D[grounds.Length];
        for (int i = 0; i < grounds.Length; i++)
        {
            allGround[i] = grounds[i].GetComponent<Collider2D>();
        }
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
    public void Patrol(Vector2 _direction) { aniControl.PlayPatrol(_direction); }


    /* 대기 중 */
    public void Hold() { aniControl.Standing(); }


    /* 공격당할 때 호출되는 함수 */
    public override void OnHitAttack(AttackSkill _skill)
    {
        Debug.Log("[ " + name + " ]" + " Take [" + _skill.damage + "] Damage From [ " + _skill.skillCaster.name + " ]");
        aniControl.ShowGetDamage();
        //스탯에 피해량(damage) 정보를 넘김
        status.TakeDamage(_skill.damage);

        //현재 체력이 바닥났을 경우
        if (status.currentHP <= 0)
        {
            Dead(_skill.skillCaster);
        }

        DiscoverTarget(_skill.skillCaster);
        if (_skill.isKnockBack) { move.KnockBack(_skill.skillDirection * _skill.knockBackPower); }
    }


    public Vector2 GetLookDirection() { return lookDirection; }


    public void SetLookDirection(Vector2 _direction) { lookDirection = _direction; }


    /* HP가 0이하로 떨어졌을 때 호출 됨 */
    public void Dead(GameObject killer)
    {
        if (killer.CompareTag("Player"))
        {
            killer.GetComponent<PlayerControl>().TakeEXP(status.gainableEXP);
        }
        Destroy(gameObject);
    }


    /* 땅 위에 착지할 때 호출됨 */
    public override void OnGround(string col_tag)
    {
        onGround = true;
        StartCoroutine("VelocityYCheck");
        if (col_tag.Equals("Earth"))
        {
            IgnoreAllGround();
        }
    }


    /* Y방향 속도에 따라 변수 조정 */
    protected override IEnumerator VelocityYCheck()
    {
        bool f_detect = false;
        while (true)
        {
            if (rigid.velocity.y > 0)
            {
                isJumping = true;
                isFalling = false;
            }
            else if (rigid.velocity.y < 0)
            {
                isFalling = true;
                if (isFalling != f_detect)
                {
                    f_detect = isFalling;
                    NoIgnoreAllGround();
                }
            }
            else
            {
                isFalling = false;
                isJumping = false;
                if (onGround) { break; }
            }

            yield return null;
        }
    }


    /* 모든 Ground 충돌체와의 충돌 무시 */
    private void IgnoreAllGround()
    {
        if (allGround == null) { return; }
        for (int i = 0; i < allGround.Length; i++)
        {
            Physics2D.IgnoreCollision(myCollider, allGround[i], true);
        }
    }


    /* 모든 Ground 충돌체와의 충돌 허용 */
    private void NoIgnoreAllGround()
    {
        if (allGround == null) { return; }
        for (int i = 0; i < allGround.Length; i++)
        {
            Physics2D.IgnoreCollision(myCollider, allGround[i], false);
        }
    }
}
