using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIRECTION = EnumInterface.DIRECTTION_TO_INT;


public class EnemyMove : MonoBehaviour
{
    private const int LEFT = (int)DIRECTION.LEFT;
    private const int RIGHT = (int)DIRECTION.RIGHT;

    private EnemyStatus status;
    private EnemyControl control;
    private Rigidbody2D rigid2D;
    public GameObject targetObject;

    private bool isPatrol;
    private bool isMovable;

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
        isPatrol = false;
        hitStunTime = 0;
        isMovable = true;
    }


    void Update()
    {
        if (hitStunTime > 0) { hitStunTime -= Time.deltaTime; }
        if (knockBackTimer > 0) { knockBackTimer -= Time.deltaTime; }

        if (rigid2D.velocity.x != 0 && knockBackTimer <= 0)
        {
            rigid2D.velocity = rigid2D.velocity.y * Vector2.up;
        }

        if (targetObject != null)
        {
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
    }

    private void CompletePatrol()
    {
        control.Hold();
    }


    public void StartPatrol(Vector2 _dir_dist)
    {
        isPatrol = true;
        iTween.MoveBy(gameObject, iTween.Hash(
                    "x", _dir_dist.x,
                    "speed", status.moveSpeed,
                    "easetype", iTween.EaseType.linear,
                    "oncomplete", "CompletePatrol")
                    );
    }

    public void StopPatrol()
    {
        isPatrol = false;
        iTween.Stop(gameObject);
    }


    public void MoveToThisObject(GameObject _target) { targetObject = _target; }


    public void StopMoveToObject() { targetObject = null; }


    public void KnockBack(Vector2 dir_val)
    {
        rigid2D.velocity = dir_val * (100 - status.knockBackResistance) / 100 + rigid2D.velocity.y * Vector2.up;
        hitStunTime = DEFAULT_HIT_STUN_TIME * (100 - status.hitStunResistance) / 100;
        knockBackTimer = DEFAULT_KNOCK_BACK_TIME;
    }


    public void SetMovable(bool _value) { isMovable = _value; }
}
