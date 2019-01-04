using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : ObjectControl
{

    private PlayerStatus status;
    private PlayerMove playerMove;
    private PlayerAnimation playerAnimation;
    private PlayerAttack playerAttack;
    private PlayerSkill playerSkill;
    private Vector2 spriteDirect;  //바라보는 방향
    private Vector2 moveDirect;  //움직이는 방향
    private float axisX;    //수평 입력값 (-1 ~ 1)
    private float axisY;    //수직 입력값 (-1 ~ 1)
    private Vector2 firstDirect;

    private bool isAttackable;         //피격 가능 상태
    private float gracePeriodTimer;  //피격 시 무적 타이머

    private bool inventoryActive;    //인벤토리 창의 활성화 여부
    private int skillIndex;              //스킬 입력 인덱스(1~4)
    private bool isDelay;              //딜레이 상태

    void Start()
    {
        inventoryActive = false;
        isDelay = false;
        isAttackable = true;
        status = GetComponent<PlayerStatus>();
        playerMove = GetComponent<PlayerMove>();
        playerAnimation = GetComponentInChildren<PlayerAnimation>();
        playerAttack = GetComponentInChildren<PlayerAttack>();
        playerSkill = GetComponentInChildren<PlayerSkill>();
        moveDirect = Vector2.zero;
        spriteDirect = Vector2.down;
        firstDirect = Vector2.zero;

        //플레이어와 적 간의 물리적 충돌 무시 (밀림현상 방지)
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
    }


    void Update()
    {
        //인벤토리 키 입력 확인
        if (CheckInventoryKeyInput())
        {
            inventoryActive = !inventoryActive;

            if (inventoryActive)
            {
                Debug.Log("인벤토리 활성화");
                /*
                 * 인벤토리 창 활성화 함수 호출할 것
                 */
            }
            else
            {
                Debug.Log("인벤토리 비활성화");
                /*
                 * 인벤토리 창 비활성화 함수 호출할 것
                 */
            }
        }

        if (!inventoryActive)
        {
            //스킬 키를 눌렀는지 확인
            skillIndex = CheckSkillKeyInput();

            //딜레이 상태가 아니라면
            if (!isDelay)
            {
                //이동키를 눌렀는지 확인
                if (CheckMoveKeyInput())
                {
                    SetSpriteDirect();                      //바라볼 방향 결정
                    playerMove.Move(moveDirect);   //플레이어 이동
                    playerAttack.StopAttack();          //공격 취소

                    //이동키 입력에 의한 플레이어 애니메이션 적용
                    playerAnimation.TurnPlayer(spriteDirect);
                    playerAnimation.StopAttack();
                    if (moveDirect == Vector2.zero) { playerAnimation.StopWalking(); }
                    else { playerAnimation.StartWalking(); }

                    //이동 중 사용 가능한 스킬을 사용했을 경우
                    if (skillIndex >= 0)
                    {
                        if (playerSkill.GetSkill(skillIndex).usableOnMove)
                        {
                            playerSkill.UseSkill(skillIndex, spriteDirect);
                        }
                    }
                }
                //이동키를 누르지 않은 경우
                else
                {
                    //대기 애니메이션 적용
                    playerAnimation.StopWalking();
                    firstDirect = Vector2.zero;

                    //공격키를 눌렀는지 확인
                    if (CheckAttackKeyInput())
                    {
                        playerAttack.Attack();
                    }
                    else { playerAttack.StopAttack(); }

                    //스킬 사용
                    if (skillIndex >= 0)
                    {
                        playerSkill.UseSkill(skillIndex, spriteDirect);
                    }
                }
            }
            //딜레이 상태인 경우
            else
            {
                if (skillIndex >= 0 && playerSkill.GetSkill(skillIndex).delayCancelable)
                {
                    playerSkill.UseSkill(skillIndex, spriteDirect);
                }
            }
        }

        //피격 시 무적 상태인 경우
        if (!isAttackable)
        {
            gracePeriodTimer -= Time.deltaTime;
            //지속시간이 지나면 해제
            if (gracePeriodTimer <= 0)
            {
                isAttackable = true;
            }
        }
    }


    /* 이동키(방향키) 입력에 관한 처리 */
    /* 방향키를 입력 중일 때만 true를 반환 */
    private bool CheckMoveKeyInput()
    {
        //이동직전 최초 입력키를 저장
        if (firstDirect == Vector2.zero)
        {
            //수직과 수평 방향키를 동시에 누른 경우
            if ((Input.GetKey("down") || Input.GetKey("up"))
                && (Input.GetKey("left") || Input.GetKey("right")))
            {
                if (Input.GetKey("left")) { firstDirect = Vector2.left; }
                if (Input.GetKey("right")) { firstDirect = Vector2.right; }
            }
            //각각의 방향
            else if (Input.GetKey("down")) { firstDirect = Vector2.down; }
            else if (Input.GetKey("up")) { firstDirect = Vector2.up; }
            else if (Input.GetKey("right")) { firstDirect = Vector2.right; }
            else if (Input.GetKey("left")) { firstDirect = Vector2.left; }
        }


        //각각의 방향에 대한 입력
        //첫번째 if는 반대방향을 동시 입력 시 방향값(Vector)을 0(zero)으로 하여 어느 쪽으로도 움직이지 못하게 한다.
        //두번째, 세번째 if는 수직 방향과 수평 방향이 같이 눌려있을 경우 해당 방향으로의 방향값을 추가
        //두번째, 세번째 if가 모두 참일 경우 방향값은 0이 된다.
        if (Input.GetKey("down"))
        {
            moveDirect = Vector2.down;
            if (Input.GetKey("up")) { moveDirect = Vector2.zero; }

            if (Input.GetKey("left")) { moveDirect += Vector2.left; }
            if (Input.GetKey("right")) { moveDirect += Vector2.right; }
            return true;
        }
        else if (Input.GetKey("up"))
        {
            moveDirect = Vector2.up;
            if (Input.GetKey("down")) { moveDirect = Vector2.zero; }

            if (Input.GetKey("left")) { moveDirect += Vector2.left; }
            if (Input.GetKey("right")) { moveDirect += Vector2.right; }
            return true;
        }
        else if (Input.GetKey("left"))
        {
            moveDirect = Vector2.left;
            if (Input.GetKey("right")) { moveDirect = Vector2.zero; }

            if (Input.GetKey("down")) { moveDirect += Vector2.down; }
            if (Input.GetKey("up")) { moveDirect += Vector2.up; }
            return true;
        }
        else if (Input.GetKey("right"))
        {
            moveDirect = Vector2.right;
            if (Input.GetKey("left")) { moveDirect = Vector2.zero; }

            if (Input.GetKey("down")) { moveDirect += Vector2.down; }
            if (Input.GetKey("up")) { moveDirect += Vector2.up; }
            return true;
        }

        //아무런 이동키 입력이 없을 경우 방향값을 0으로 하고 false반환
        moveDirect = Vector2.zero;
        return false;
    }


    /* 기본 공격키 입력에 관한 처리 */
    /* 공격키 입력 중일 때만 true 반환 */
    private bool CheckAttackKeyInput()
    {
        return Input.GetButton("Attack");
    }


    /* 인벤토리 키 입력 확인 */
    private bool CheckInventoryKeyInput()
    {
        return Input.GetButtonDown("Inventory");
    }


    /* 스킬 사용 키 입력 확인 */
    private int CheckSkillKeyInput()
    {
        if (Input.GetButtonDown("Skill_1"))
        {
            return 0;
        }
        else if (Input.GetButtonDown("Skill_2"))
        {
            return 1;
        }
        else if (Input.GetButtonDown("Skill_3"))
        {
            return 2;
        }
        else if (Input.GetButtonDown("Skill_4"))
        {
            return 3;
        }
        return -1;
    }


    /* 캐릭터가 바라볼 방향을 결정 */
    private void SetSpriteDirect()
    {
        //먼저 입력된 방향(firstDirect)을 바라보게 함
        //먼저 입력된 방향키가 현재는 입력되고 있지 않을 때 firstDirect초기화
        if (firstDirect == Vector2.down)
        {
            if (!Input.GetKey("down")) { firstDirect = Vector2.zero; }
            else { spriteDirect = firstDirect; }
        }
        if (firstDirect == Vector2.up)
        {
            if (!Input.GetKey("up")) { firstDirect = Vector2.zero; }
            else { spriteDirect = firstDirect; }
        }
        if (firstDirect == Vector2.right)
        {
            if (!Input.GetKey("right")) { firstDirect = Vector2.zero; }
            else { spriteDirect = firstDirect; }
        }
        if (firstDirect == Vector2.left)
        {
            if (!Input.GetKey("left")) { firstDirect = Vector2.zero; }
            else { spriteDirect = firstDirect; }
        }

        //상하키 또는 좌우키가 동시에 눌린 상태에서 움직일 때 방향처리
        if ((Input.GetKey("down") && Input.GetKey("up")))
        {
            if (Input.GetKey("left")) { spriteDirect = Vector2.left; }
            if (Input.GetKey("right")) { spriteDirect = Vector2.right; }
        }
        if ((Input.GetKey("left") && Input.GetKey("right")))
        {
            if (Input.GetKey("down")) { spriteDirect = Vector2.down; }
            if (Input.GetKey("up")) { spriteDirect = Vector2.up; }
        }
    }

    
    /* 공격 받을 때 호출되는 함수 */
    public override void OnHitAttack(AttackSkill _skill)
    {
        //무적 상태에 있으면 공격받지 않음
        if (!isAttackable) { return; }

        SetInvincibleTime(status.gracePeriod);
        playerAnimation.ShowGetDamage();
        //스탯에 피해량(damage) 정보를 넘김
        status.TakeDamage(_skill.damage);
        playerMove.HitStun();

        if (_skill.isKnockBack) { playerMove.KnockBack(_skill.skillDirect * _skill.knockBackPower); }

        //현재 체력이 바닥났을 경우
        if (status.currentHP <= 0)
        {
            PlayerDead();
        }
    }


    /* 일정 시간동안 무적상태 활성화 */
    public void SetInvincibleTime(float time)
    {
        gracePeriodTimer = time;
        isAttackable = false;
    }


    /* 피격 무적상태 확인 */
    public bool IsAttackable() { return isAttackable; }


    /* 경험치 획득 */
    public void TakeEXP(float exp)
    {
        status.currentEXP += exp;
        Debug.Log("경험치 획득 : " + exp);
    }


    /* HP가 0이하로 내려갔을 때 호출됨 */
    public void PlayerDead()
    {
        Debug.Log("플레이어 사망");
    }


    /* 딜레이 상태 Set / Get */
    public void SetIsDelay(bool value) { isDelay = value; }
    public bool GetIsDelay() { return isDelay; }

    public PlayerMove GetPlayerMove() { return playerMove; }
    public PlayerAnimation GetPlayerAnimation() { return playerAnimation; }
    public PlayerStatus GetPlayerStatus() { return status; }
}
