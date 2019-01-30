using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyControl : ObjectControl
{
    public float minPatrolDelay;
    public float maxPatrolDelay;
    public float maxDistancePatrol;

    protected Vector2 lookDirection;
    public GameObject target;  // 공격 대상
    protected EnemyAnimation aniControl;
    protected EnemyMove move;
    protected EnemyStatus status;
    protected EnemyAttack attack;
    protected Collider2D[] allGround;
    protected Collider2D myCollider;

    
    protected bool isDelay;
    protected float patrolTimer;
    protected float patrolDelay;
    protected float distance;
    protected Vector2 randDirection;

    void Start()
    {
        aniControl = GetComponentInChildren<EnemyAnimation>();
        move = GetComponent<EnemyMove>();
        status = GetComponent<EnemyStatus>();
        attack = GetComponentInChildren<EnemyAttack>();
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

        patrolTimer = 0f;
        patrolDelay = Random.Range(minPatrolDelay, maxPatrolDelay);
        isDelay = false;
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1)) { SetIsDelay(!isDelay); }
        if (target == null && !isDelay)
        {
            patrolTimer += Time.deltaTime;

            if (patrolTimer >= patrolDelay)
            {
                patrolTimer = 0;
                //임의의 시간 (min ~ max 실수)
                patrolDelay = Random.Range(minPatrolDelay, maxPatrolDelay);

                //임의의 거리 (0 ~ maxDistancePatrol 실수)
                distance = Random.Range(0, maxDistancePatrol);
                //임의의 정수 (0~3)
                switch (Random.Range(0, 2))
                {
                    case 0:
                        randDirection = Vector2.left;
                        break;
                    case 1:
                        randDirection = Vector2.right;
                        break;
                }
                Patrol(randDirection, distance);
            }
        }
    }


    /* 딜레이 상태 (Set / Get) */
    public void SetIsDelay(bool value)
    {
        isDelay = value;
        if (isDelay)
        {
            move.StopPatrol();
            move.SetMovable(false);
        }
        else
        {
            move.SetMovable(true);
        }
    }
    public bool GetIsDelay() { return isDelay; }



    /* 타겟 발견 */
    public abstract void DiscoverTarget(GameObject _target);


    /* 타겟 분실 */
    public void TargetLost()
    {
        target = null;
        move.StopMoveToObject();
        patrolTimer = 0f;
        Hold();
    }


    /* 순찰 중 */
    public void Patrol(Vector2 _direction, float _distance)
    {
        aniControl.PlayPatrol(_direction);
        move.StartPatrol(_direction * _distance);
    }


    /* 대기 중 */
    public void Hold()
    {
        aniControl.Standing();
        move.StopPatrol();
    }


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
