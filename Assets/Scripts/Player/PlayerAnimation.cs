using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIRECTION = EnumInterface.DIRECTION_TO_FLOAT;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerControl control;
    private Animator playerAnimator;   //에니메이션 관리 컴포넌트
    private SpriteRenderer sprite;
    private Vector2 curSpriteDirection;     //현재 바라보는 방향
    private Vector2 nextSpriteDirection;   //다음 바라볼 방향
    //private GameObject helperCanvas;
    private bool isWalk;
    private bool isAttack;

    // * 애니메이터의 Direction 패러미터에 의해 바라보는 방향이 결정
    // * Direction는 float형이므로 어느 방향을 의미하는지 쉽게 알기 위해 상수형 변수 const 사용
    private const float DOWN = (float)DIRECTION.DOWN;
    private const float UP = (float)DIRECTION.UP;
    private const float LEFT = (float)DIRECTION.LEFT;
    private const float RIGHT = (float)DIRECTION.RIGHT;

    private float atkMotionTimer;            //공격 모션 타이머
    private float atkMotionSpeed = 1.0f;  //공격 모션 속도 (기본값 : 1)

    //데미지를 입었을 때 사용 변수들
    private bool isGetDamage;
    private float dmgMotionTimer;
    private Color[] dmgMotionColor;

    void Start()
    {
        control = GetComponentInParent<PlayerControl>();
        playerAnimator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        curSpriteDirection = Vector2.right;
        nextSpriteDirection = Vector2.right;
        isWalk = false;
        atkMotionTimer = 0;
        dmgMotionColor = new Color[2];
        dmgMotionColor[0] = new Color(1.0f, 0.4f, 0.4f, 0.8f);
        dmgMotionColor[1] = new Color(1.0f, 0.4f, 0.4f, 0.4f);
        //helperCanvas = transform.GetChild(1).gameObject;
    }

    void Update()
    {
        //바라보는 방향이 바뀌면
        if (curSpriteDirection != nextSpriteDirection)
        {
            curSpriteDirection = nextSpriteDirection;

            //Animator의 파라미터 Direction에 의해 어느 방향의 애니메이션을 실행할 것인지 결정
            if (curSpriteDirection == Vector2.right)
            {
                //X스케일을 1로 하여 원래 방향을 보게 함
                transform.localScale = Vector2.right + Vector2.up;
                playerAnimator.SetFloat("Direction", RIGHT);
                transform.localPosition = Vector2.left * 0.03f;
            }
            if (curSpriteDirection == Vector2.left)
            {
                //X스케일을 -1로 하여 반대 방향을 보게 함
                transform.localScale = Vector2.left + Vector2.up;
                playerAnimator.SetFloat("Direction", LEFT);
                transform.localPosition = Vector2.right * 0.03f;
            }
            //헬퍼 UI는 X스케일이 항상 0보다 커야함(텍스트가 뒤집힘)
            //if (helperCanvas.transform.lossyScale.x < 0) { helperCanvas.transform.localScale *= Vector2.left + Vector2.up; }
        }

        //이동 중인 상태라면 이동 애니메이션으로 전환
        if (playerAnimator.GetBool("IsWalk") != isWalk && !playerAnimator.GetBool("IsAttack"))
        {
            playerAnimator.SetBool("IsWalk", isWalk);
        }

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

        //데미지를 입은 경우 피격 모션
        if (isGetDamage)
        {
            dmgMotionTimer += Time.deltaTime;
            if (Mathf.FloorToInt(dmgMotionTimer * 10) % 2 == 0) { sprite.color = dmgMotionColor[0]; }
            else { sprite.color = dmgMotionColor[1]; }

            if (control.IsAttackable())
            {
                isGetDamage = false;
                sprite.color = Color.white;
                dmgMotionTimer = 0;
            }
        }
    }


    public void StartWalking() { isWalk = true; }


    public void StopWalking() { isWalk = false; }


    public void StartAttack() { isAttack = true; }


    public void StopAttack() { isAttack = false; }


    public void SetAttackMotionSpeed(float _speed) { atkMotionSpeed = _speed; }


    public void TurnPlayer(Vector2 _spriteDirection) { nextSpriteDirection = _spriteDirection; }


    public Vector2 GetPlayerSpriteDirection() { return curSpriteDirection; }


    /* 데미지 피격 모션 실행 */
    public void ShowGetDamage()
    {
        isGetDamage = true;
        sprite.color = dmgMotionColor[0];
    }

}
