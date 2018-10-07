using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIRECT = EnumInterface.DIRECT_TO_INT;


public class EnemyMove : MonoBehaviour
{
    public float minPatrolDelay;
    public float maxPatrolDelay;
    public float maxDistancePatrol;

    private const int DOWN = (int)DIRECT.DOWN;
    private const int UP = (int)DIRECT.UP;
    private const int LEFT = (int)DIRECT.LEFT;
    private const int RIGHT = (int)DIRECT.RIGHT;

    private EnemyStatus status;
    private EnemyControl control;
    private GameObject targetObject;
    private Vector2 targetPosition;

    private Vector2 randDirect;
    private float distance;
    private float patrolDelay;
    private float patrolTimer;
    private bool isPatrol;

    void Start()
    {
        control = GetComponent<EnemyControl>();
        status = GetComponent<EnemyStatus>();
        targetObject = null;
        targetPosition = transform.position;
        patrolTimer = 0f;
        patrolDelay = Random.Range(minPatrolDelay, maxPatrolDelay);
        isPatrol = false;
    }


    void Update()
    {
        if (targetObject != null)
        {
            //순찰 중지
            isPatrol = false;
            iTween.Stop(gameObject);
            transform.Translate((targetObject.transform.position - transform.position).normalized * status.moveSpeed * Time.deltaTime);
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
                switch (Random.Range(0, 4))
                {
                    case DOWN:
                        randDirect = Vector2.down;
                        break;
                    case UP:
                        randDirect = Vector2.up;
                        break;
                    case LEFT:
                        randDirect = Vector2.left;
                        break;
                    case RIGHT:
                        randDirect = Vector2.right;
                        break;
                }

                iTween.MoveBy(gameObject, iTween.Hash(
                    "x", randDirect.x * distance,
                    "y", randDirect.y * distance,
                    "speed", status.moveSpeed,
                    "easetype", iTween.EaseType.linear,
                    "onstart", "StartPatrol",
                    "oncomplete", "CompletePatrol")
                    );

            }

        }
    }


    void StartPatrol()
    {
        isPatrol = true;
        control.Patrol(randDirect);
    }

    void CompletePatrol()
    {
        isPatrol = false;
        control.Hold();
    }


    public void MoveToThisObject(GameObject _target) { targetObject = _target; }


    public void MoveToThisPosition(Vector2 _pos) { targetPosition = _pos; }


    public void StopMove() { targetObject = null; }


    public void KnockBack(Vector2 dir_dist)
    {
        transform.Translate(dir_dist, Space.World);
    }


}
