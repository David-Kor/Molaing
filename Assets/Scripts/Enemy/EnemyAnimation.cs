using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIRECT = EnumInterface.DIRECT_TO_FLOAT;

public class EnemyAnimation : MonoBehaviour
{

    public Animator animator;

    // * 애니메이터의 Direct 속성값에 의해 바라보는 방향이 결정
    // * Direct는 float형이므로 어느 방향을 의미하는지 가독성을 높이기 위해 상수형 변수 const 사용
    private const float DOWN = (float)DIRECT.DOWN;
    private const float UP = (float)DIRECT.UP;
    private const float LEFT = (float)DIRECT.LEFT;
    private const float RIGHT = (float)DIRECT.RIGHT;

    private GameObject target;
    private Vector2 directToTarget;

    void Update()
    {
        if (target != null)
        {
            float x = target.transform.position.x - transform.position.x;
            float y = target.transform.position.y - transform.position.y;

            if (Mathf.Abs(x) >= Mathf.Abs(y))
            {
                if (x < 0) { directToTarget = Vector2.left; }
                else { directToTarget = Vector2.right; }
            }
            else
            {
                if (y < 0) { directToTarget = Vector2.down; }
                else { directToTarget = Vector2.up; }
            }

            PlayPatrol(directToTarget);
        }
    }


    public void LookAtTarget(GameObject _target) { target = _target; }


    public void PlayPatrol(Vector2 _direct)
    {
        animator.SetBool("IsWalk", true);
        transform.rotation = Quaternion.Euler(0, 0, 0);

        if (_direct == Vector2.down)
        {
            animator.SetFloat("Direct", DOWN);
        }
        if (_direct == Vector2.up)
        {
            animator.SetFloat("Direct", UP);
        }
        if (_direct == Vector2.right)
        {
            animator.SetFloat("Direct", RIGHT);
        }
        if (_direct == Vector2.left)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            animator.SetFloat("Direct", LEFT);
        }
    }


    public void Standing()
    {
        animator.SetBool("IsWalk", false);
        target = null;
    }

}
