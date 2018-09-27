using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIRECT = EnumInterface.DIRECT_TO_FLOAT;

public class PlayerAnimation : MonoBehaviour
{
    private Animator playerAnimator;    //에니메이션 관리 컴포넌트
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
    private float atkMotionTimer;
    private float atkMotionSpeed = 1.0f;     //default : 1

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        curSpriteDirect = Vector2.down;
        nextSpriteDirect = Vector2.down;
        isWalk = false;
        atkMotionTimer = 0;
    }

    void Update()
    {
        //바라보는 방향이 바뀌면
        if (curSpriteDirect != nextSpriteDirect)
        {
            curSpriteDirect = nextSpriteDirect;
            transform.rotation = Quaternion.Euler(0, 0, 0); //회전값 초기화

            //Animator의 파라미터 Direct에 의해 어느 방향의 애니메이션을 실행할 것인지 결정
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

        //이동 중인 상태라면 이동 애니메이션으로 전환
        if (playerAnimator.GetBool("IsWalk") != isWalk)
        {
            playerAnimator.SetBool("IsWalk", isWalk);
        }

        //이동 중이 아닌 경우
        if (!isWalk)
        {
            //공격 애니메이션 실행 중 공격키를 뗀 경우
            //마지막 공격 모션이 끝날 때까지 애니메이션이 계속됨
            //(공격키를 땠을 때 모션이 캔슬되는 것을 방지하기 위함)
            if (playerAnimator.GetBool("IsAttack") && !isAttack)
            {
                atkMotionTimer += Time.deltaTime;
                if (playerAnimator.GetCurrentAnimatorStateInfo(0).length < atkMotionTimer)
                {
                    isAttack = false;
                    playerAnimator.SetBool("IsAttack", isAttack);
                    atkMotionTimer = 0;
                }
            }
            //공격키를 계속 누르고 있는 상태이면 isAttack과 동기화
            //모션이 한 번 사이클할 때마다 false로 변한 후 공격 딜레이가 끝나면 다시 true로 변함
            else { playerAnimator.SetBool("IsAttack", isAttack); }

            //기본 공격키가 입력 중인 경우
            if (isAttack)
            {
                atkMotionTimer += Time.deltaTime;
                //현재 실행중인 애니메이션이 Attack인 경우
                if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    playerAnimator.SetFloat("AttackMotionSpeed", atkMotionSpeed);

                    //애니메이션 재생이 끝나면 초기화
                    if (playerAnimator.GetCurrentAnimatorStateInfo(0).length < atkMotionTimer)
                    {
                        isAttack = false;
                        playerAnimator.SetBool("IsAttack", isAttack);
                        atkMotionTimer = 0;
                    }
                }
            }
            else if (playerAnimator.GetBool("IsAttack") == isAttack) { atkMotionTimer = 0; }
        }
        //공격 중에 이동하거나 이동 중에 공격하는 경우
        //공격 애니메이션을 재생하지 않게 함
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


    public void SetAttackMotionSpeed(float _speed) { atkMotionSpeed = _speed; }


    public void TurnPlayer(Vector2 _spriteDirect) { nextSpriteDirect = _spriteDirect; }


    public Vector2 GetPlayerSpriteDirect() { return curSpriteDirect; }

}
