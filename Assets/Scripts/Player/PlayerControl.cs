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
    private UI_Controller ui;
    private Vector2 spriteDirection;  //바라보는 방향
    private Vector2 moveDirection;  //움직이는 방향
    private float axisX;    //수평 입력값 (-1 ~ 1)
    private float axisY;    //수직 입력값 (-1 ~ 1)
    private Vector2 firstDirection;

    private bool isAttackable;         //피격 가능 상태
    private float gracePeriodTimer;  //피격 시 무적 타이머

    private bool inventoryActive;    //인벤토리 창의 활성화 여부
    private int skillIndex;              //스킬 입력 인덱스(1~4)
    private bool isDelay;              //딜레이 상태

    private bool controllable;

    void Start()
    {
        inventoryActive = false;
        isDelay = false;
        isAttackable = true;
        controllable = true;
        status = GetComponent<PlayerStatus>();
        playerMove = GetComponent<PlayerMove>();
        playerAnimation = GetComponentInChildren<PlayerAnimation>();
        playerAttack = GetComponentInChildren<PlayerAttack>();
        playerSkill = GetComponentInChildren<PlayerSkill>();
        ui = Camera.main.GetComponent<UI_Controller>();
        moveDirection = Vector2.zero;
        spriteDirection = Vector2.down;
        firstDirection = Vector2.zero;

        TakeEXP(0f);
        //플레이어와 적 간의 물리적 충돌 무시 (밀림현상 방지)
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
    }


    void Update()
    {
        //조작키(Control)가 블록 되어있으면 동작 안함
        if (controllable == false) { return; }

        //인벤토리 키 입력 확인
        if (CheckInventoryKeyInput())
        {
            inventoryActive = !inventoryActive;
            ui.Control_Inventory(inventoryActive);
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
                    SetSpriteDirection();                      //바라볼 방향 결정
                    playerMove.Move(moveDirection);   //플레이어 이동
                    playerAttack.StopAttack();          //공격 취소

                    //이동키 입력에 의한 플레이어 애니메이션 적용
                    playerAnimation.TurnPlayer(spriteDirection);
                    playerAnimation.StopAttack();
                    if (moveDirection == Vector2.zero) { playerAnimation.StopWalking(); }
                    else { playerAnimation.StartWalking(); }

                    //이동 중 사용 가능한 스킬을 사용했을 경우
                    if (skillIndex >= 0)
                    {
                        if (playerSkill.GetSkill(skillIndex).usableOnMove)
                        {
                            playerSkill.UseSkill(skillIndex, spriteDirection);
                        }
                    }
                }
                //이동키를 누르지 않은 경우
                else
                {
                    //대기 애니메이션 적용
                    playerAnimation.StopWalking();
                    firstDirection = Vector2.zero;

                    //공격키를 눌렀는지 확인
                    if (CheckAttackKeyInput())
                    {
                        playerAttack.Attack();
                    }
                    else { playerAttack.StopAttack(); }

                    //스킬 사용
                    if (skillIndex >= 0)
                    {
                        playerSkill.UseSkill(skillIndex, spriteDirection);
                    }
                }
            }
            //딜레이 상태인 경우
            else
            {
                if (skillIndex >= 0 && playerSkill.GetSkill(skillIndex).delayCancelable)
                {
                    playerSkill.CancelSpell();
                    playerSkill.UseSkill(skillIndex, spriteDirection);
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
        if (firstDirection == Vector2.zero)
        {
            //수직과 수평 방향키를 동시에 누른 경우
            if ((Input.GetKey("down") || Input.GetKey("up"))
                && (Input.GetKey("left") || Input.GetKey("right")))
            {
                if (Input.GetKey("left")) { firstDirection = Vector2.left; }
                if (Input.GetKey("right")) { firstDirection = Vector2.right; }
            }
            //각각의 방향
            else if (Input.GetKey("down")) { firstDirection = Vector2.down; }
            else if (Input.GetKey("up")) { firstDirection = Vector2.up; }
            else if (Input.GetKey("right")) { firstDirection = Vector2.right; }
            else if (Input.GetKey("left")) { firstDirection = Vector2.left; }
        }


        //각각의 방향에 대한 입력
        //첫번째 if는 반대방향을 동시 입력 시 방향값(Vector)을 0(zero)으로 하여 어느 쪽으로도 움직이지 못하게 한다.
        //두번째, 세번째 if는 수직 방향과 수평 방향이 같이 눌려있을 경우 해당 방향으로의 방향값을 추가
        //두번째, 세번째 if가 모두 참일 경우 방향값은 0이 된다.
        if (Input.GetKey("down"))
        {
            moveDirection = Vector2.down;
            if (Input.GetKey("up")) { moveDirection = Vector2.zero; }

            if (Input.GetKey("left")) { moveDirection += Vector2.left; }
            if (Input.GetKey("right")) { moveDirection += Vector2.right; }
            return true;
        }
        else if (Input.GetKey("up"))
        {
            moveDirection = Vector2.up;
            if (Input.GetKey("down")) { moveDirection = Vector2.zero; }

            if (Input.GetKey("left")) { moveDirection += Vector2.left; }
            if (Input.GetKey("right")) { moveDirection += Vector2.right; }
            return true;
        }
        else if (Input.GetKey("left"))
        {
            moveDirection = Vector2.left;
            if (Input.GetKey("right")) { moveDirection = Vector2.zero; }

            if (Input.GetKey("down")) { moveDirection += Vector2.down; }
            if (Input.GetKey("up")) { moveDirection += Vector2.up; }
            return true;
        }
        else if (Input.GetKey("right"))
        {
            moveDirection = Vector2.right;
            if (Input.GetKey("left")) { moveDirection = Vector2.zero; }

            if (Input.GetKey("down")) { moveDirection += Vector2.down; }
            if (Input.GetKey("up")) { moveDirection += Vector2.up; }
            return true;
        }

        //아무런 이동키 입력이 없을 경우 방향값을 0으로 하고 false반환
        moveDirection = Vector2.zero;
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
    private void SetSpriteDirection()
    {
        //먼저 입력된 방향(firstDirection)을 바라보게 함
        //먼저 입력된 방향키가 현재는 입력되고 있지 않을 때 firstDirection초기화
        if (firstDirection == Vector2.down)
        {
            if (!Input.GetKey("down")) { firstDirection = Vector2.zero; }
            else { spriteDirection = firstDirection; }
        }
        if (firstDirection == Vector2.up)
        {
            if (!Input.GetKey("up")) { firstDirection = Vector2.zero; }
            else { spriteDirection = firstDirection; }
        }
        if (firstDirection == Vector2.right)
        {
            if (!Input.GetKey("right")) { firstDirection = Vector2.zero; }
            else { spriteDirection = firstDirection; }
        }
        if (firstDirection == Vector2.left)
        {
            if (!Input.GetKey("left")) { firstDirection = Vector2.zero; }
            else { spriteDirection = firstDirection; }
        }

        //상하키 또는 좌우키가 동시에 눌린 상태에서 움직일 때 방향처리
        if ((Input.GetKey("down") && Input.GetKey("up")))
        {
            if (Input.GetKey("left")) { spriteDirection = Vector2.left; }
            if (Input.GetKey("right")) { spriteDirection = Vector2.right; }
        }
        if ((Input.GetKey("left") && Input.GetKey("right")))
        {
            if (Input.GetKey("down")) { spriteDirection = Vector2.down; }
            if (Input.GetKey("up")) { spriteDirection = Vector2.up; }
        }
    }


    /* 이동 방향을 반환 */
    public Vector2 GetMoveDirection() { return moveDirection; }


    /* 공격 받을 때 호출되는 함수 */
    public override void OnHitAttack(AttackSkill _skill)
    {
        //무적 상태에 있으면 공격받지 않음
        if (!isAttackable) { return; }

        Debug.Log("[ " + name + " ]" + " Take [" + _skill.damage + "] Damage From [ " + _skill.skillCaster.name + " ]");
        SetInvincibleTime(status.gracePeriod);
        playerAnimation.ShowGetDamage();
        //스탯에 피해량(damage) 정보를 넘김
        status.TakeDamage(_skill.damage);
        ui.HP(status.currentHP, status.maxHP);
        playerMove.HitStun();

        if (_skill.isKnockBack) { playerMove.KnockBack(_skill.skillDirection * _skill.knockBackPower); }

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
        status.EXP_Up(exp);
        ui.Exp(status.currentEXP, status.requireEXP, status.level);
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

    /* 조작키 입력을 막음 */
    public void BlockControlInput() { controllable = false; }
    /* 조작키 입력을 허용 */
    public void UnblockControlInput() { controllable = true; }


    /* 쿨타임 활성화 */
    public override void CoolDownActive(int _index, float _value)
    {
        playerSkill.CoolDownTimerActive(_index, _value);
    }
}
