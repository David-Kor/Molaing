using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {

    private GameObject target;  // 공격 대상
    private EnemyAnimation aniControl;
    private EnemyMove move;
    private EnemyStatus status;
    private Vector2 patrolDirect;

    void Start()
    {
        aniControl = GetComponentInChildren<EnemyAnimation>();
        move = GetComponent<EnemyMove>();
        status = GetComponent<EnemyStatus>();
        patrolDirect = Vector2.zero;

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


    public void Patrol(Vector2 _direct)
    {
        aniControl.PlayPatrol(_direct);
    }


    public void Hold() { aniControl.Standing(); }


}
