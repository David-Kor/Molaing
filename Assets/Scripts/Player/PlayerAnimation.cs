using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIRECT = EnumInterface.DIRECT_TO_FLOAT;

public class PlayerAnimation : MonoBehaviour
{
    private Animator playerAnimator;
    private Vector2 curSpriteDirect;   //현재 바라보는 방향
    private Vector2 nextSpriteDirect;   //다음 바라볼 방향
    private bool isWalk;
    private bool isAttack;

    // * 애니메이터의 Direct 속성값에 의해 바라보는 방향이 결정
    // * Direct는 float형이므로 어느 방향을 의미하는지 가독성을 높이기 위해 상수형 변수 const 사용
    private const float DOWN = (float)DIRECT.DOWN;
    private const float UP = (float)DIRECT.UP;
    private const float LEFT = (float)DIRECT.LEFT;
    private const float RIGHT = (float)DIRECT.RIGHT;
    private float timer;
    private float attackMotionSpeed = 1.0f;     //default : 1

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        curSpriteDirect = Vector2.down;
        nextSpriteDirect = Vector2.down;
        isWalk = false;
        timer = 0;
    }

    void Update()
    {
        if (curSpriteDirect != nextSpriteDirect)
        {
            curSpriteDirect = nextSpriteDirect;
            transform.rotation = Quaternion.Euler(0, 0, 0);

            if (curSpriteDirect == Vector2.down)
            {
                playerAnimator.SetFloat("Direct", DOWN);
            }
            if (curSpriteDirect == Vector2.up)
            {
                playerAnimator.SetFloat("Direct", UP);
            }
            if (curSpriteDirect == Vector2.right)
            {
                playerAnimator.SetFloat("Direct", RIGHT);
            }
            if (curSpriteDirect == Vector2.left)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                playerAnimator.SetFloat("Direct", LEFT);   //Side Sprite가 오른쪽 방향 이미지이므로, 세로축으로 180도 회전하여 좌우반전
            }
        }

        if (playerAnimator.GetBool("IsWalk") != isWalk)
        {
            playerAnimator.SetBool("IsWalk", isWalk);
        }

        if (!isWalk)
        {
            playerAnimator.SetBool("IsAttack", isAttack);
            if (isAttack)
            {
                timer += Time.deltaTime;
                if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    playerAnimator.SetFloat("AttackMotionSpeed", attackMotionSpeed);
                    if (playerAnimator.GetCurrentAnimatorStateInfo(0).length < timer) { isAttack = false; }
                }
            }
            else { timer = 0; }
        }
        else if (playerAnimator.GetBool("IsAttack"))
        {
            isAttack = false;
            playerAnimator.SetBool("IsAttack", isAttack);
        }

    }


    public void StartWalking() { isWalk = true; }


    public void StopWalking() { isWalk = false; }


    public void StartAttack() { isAttack = true; }


    public void StopAttack() { isAttack = false; }


    public void SetAttackMotionSpeed(float _speed) { attackMotionSpeed = _speed; }


    public void TurnPlayer(Vector2 _spriteDirect) { nextSpriteDirect = _spriteDirect; }


    public Vector2 GetPlayerSpriteDirect() { return curSpriteDirect; }

}
