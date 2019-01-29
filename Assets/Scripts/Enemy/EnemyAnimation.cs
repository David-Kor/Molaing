using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIRECTION = EnumInterface.DIRECTION_TO_FLOAT;

public class EnemyAnimation : MonoBehaviour
{

    public Animator animator;

    // * 애니메이터의 Direction 속성값에 의해 바라보는 방향이 결정
    // * Direction는 float형이므로 어느 방향을 의미하는지 가독성을 높이기 위해 상수형 변수 const 사용
    private const float DOWN = (float)DIRECTION.DOWN;
    private const float UP = (float)DIRECTION.UP;
    private const float LEFT = (float)DIRECTION.LEFT;
    private const float RIGHT = (float)DIRECTION.RIGHT;

    private EnemyControl control;
    private SpriteRenderer sprite;
    public GameObject target;
    private Vector2 directionToTarget;

    private bool isGetDamage;
    private float dmgMotionTimer;
    private Color dmgMotionColor;

    void Start()
    {
        control = GetComponentInParent<EnemyControl>();
        sprite = GetComponent<SpriteRenderer>();
        dmgMotionColor = new Color(1.0f, 0.2f, 0.2f, 0.8f);
        dmgMotionTimer = 0;
    }

    void Update()
    {
        if (target != null)
        {
            float x = target.transform.position.x - transform.position.x;

            if (x < 0) { directionToTarget = Vector2.left; }
            else { directionToTarget = Vector2.right; }

            PlayPatrol(directionToTarget);
        }
        
        if (isGetDamage)
        {
            dmgMotionTimer += Time.deltaTime;
            if (dmgMotionTimer >= 0.15f)
            {
                dmgMotionTimer = 0;
                isGetDamage = false;
                sprite.color = Color.white;
            }
        }

    }


    public void LookAtTarget(GameObject _target) { target = _target; }


    public void PlayPatrol(Vector2 _direction)
    {
        animator.SetBool("IsWalk", true);
        transform.rotation = Quaternion.Euler(0, 0, 0);

        if (_direction == Vector2.down)
        {
            animator.SetFloat("Direction", DOWN);
        }
        if (_direction == Vector2.up)
        {
            animator.SetFloat("Direction", UP);
        }
        if (_direction == Vector2.right)
        {
            animator.SetFloat("Direction", RIGHT);
        }
        if (_direction == Vector2.left)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            animator.SetFloat("Direction", LEFT);
        }

        control.SetLookDirection(_direction);
    }


    public void Standing()
    {
        animator.SetBool("IsWalk", false);
        target = null;
    }


    public void ShowGetDamage()
    {
        isGetDamage = true;
        sprite.color = dmgMotionColor;
    }


    public Vector2 GetDirection() { return directionToTarget; }

}
