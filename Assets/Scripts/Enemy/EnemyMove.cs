using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIRECTION = EnumInterface.DIRECTTION_TO_INT;


public class EnemyMove : MonoBehaviour
{
    public float minPatrolDelay;
    public float maxPatrolDelay;
    public float maxDistancePatrol;

    private const int DOWN = (int)DIRECTION.DOWN;
    private const int UP = (int)DIRECTION.UP;
    private const int LEFT = (int)DIRECTION.LEFT;
    private const int RIGHT = (int)DIRECTION.RIGHT;

    private EnemyStatus status;
    private EnemyControl control;
    private Rigidbody2D rigid2D;
    private GameObject targetObject;
    private Vector2 targetPosition;

    private Vector2 randDirection;
    private float distance;
    private float patrolDelay;
    private float patrolTimer;
    private bool isPatrol;

    private const float DEFAULT_HIT_STUN_TIME = 0.25f;       //피격 경직시간 기본값
    private const float DEFAULT_KNOCK_BACK_TIME = 0.05f;  //넉백시간 기본값
    private float knockBackTimer;    //넉백 타이머
    private float hitStunTime;         //경직 타이머 

    void Start()
    {
        control = GetComponent<EnemyControl>();
        status = GetComponent<EnemyStatus>();
        rigid2D = GetComponent<Rigidbody2D>();
        targetObject = null;
        targetPosition = transform.position;
        patrolTimer = 0f;
        patrolDelay = Random.Range(minPatrolDelay, maxPatrolDelay);
        isPatrol = false;
        hitStunTime = 0;
    }


    void Update()
    {
        if (hitStunTime > 0) { hitStunTime -= Time.deltaTime; }
        if (knockBackTimer > 0) { knockBackTimer -= Time.deltaTime; }

        if (targetObject != null)
        {
            //순찰 중지
            isPatrol = false;
            iTween.Stop(gameObject);
            if (hitStunTime <= 0)
            {
                if (targetObject.transform.position.x < transform.position.x)
                {
                    transform.Translate(Vector2.left * status.moveSpeed * Time.deltaTime);
                }
                else
                {
                    transform.Translate(Vector2.right* status.moveSpeed * Time.deltaTime);
                }
            }

        }
        else if (!isPatrol)
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
                switch (Random.Range(2, 4))
                {
                    case LEFT:
                        randDirection = Vector2.left;
                        break;
                    case RIGHT:
                        randDirection = Vector2.right;
                        break;
                }

                iTween.MoveBy(gameObject, iTween.Hash(
                    "x", randDirection.x * distance,
                    "y", randDirection.y * distance,
                    "speed", status.moveSpeed,
                    "easetype", iTween.EaseType.linear,
                    "onstart", "StartPatrol",
                    "oncomplete", "CompletePatrol")
                    );

            }

        }

        if (rigid2D.velocity != Vector2.zero && knockBackTimer <= 0)
        {
            rigid2D.velocity = rigid2D.velocity.y * Vector2.up;
        }
    }


    void StartPatrol()
    {
        isPatrol = true;
        control.Patrol(randDirection);
    }

    void CompletePatrol()
    {
        isPatrol = false;
        control.Hold();
    }


    public void MoveToThisObject(GameObject _target) { targetObject = _target; }


    public void MoveToThisPosition(Vector2 _pos) { targetPosition = _pos; }


    public void StopMove() { targetObject = null; }


    public void KnockBack(Vector2 dir_val)
    {
        rigid2D.velocity = dir_val * (100 - status.knockBackResistance) / 100;
        hitStunTime = DEFAULT_HIT_STUN_TIME * (100 - status.hitStunResistance) / 100;
        knockBackTimer = DEFAULT_KNOCK_BACK_TIME;
    }

}
