using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    private PlayerMove playerMove;
    private PlayerAnimation playerAnimation;
    private PlayerAttack playerAttack;
    private Vector2 spriteDirect;  //바라보는 방향
    private Vector2 moveDirect;  //움직이는 방향
    private float axisX;    //수평 입력값 (-1 ~ 1)
    private float axisY;    //수직 입력값 (-1 ~ 1)
    public Vector2 firstDirect;


    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        playerAnimation = GetComponentInChildren<PlayerAnimation>();
        playerAttack = GetComponentInChildren<PlayerAttack>();
        moveDirect = Vector2.zero;
        spriteDirect = Vector2.down;
        firstDirect = Vector2.zero;

        //플레이어와 적 간의 물리적 충돌 무시 (밀림현상 방지)
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
    }


    void Update()
    {
        if (CheckMoveKeyInput())    //이동키를 눌렀는지 확인
        {
            SetSpriteDirect();  //바라볼 방향 결정
            playerMove.Move(moveDirect);   //플레이어 이동
            playerAttack.StopAttack();  //공격 취소

            //이동키 입력에 의한 플레이어 애니메이션 적용
            playerAnimation.TurnPlayer(spriteDirect);
            playerAnimation.StopAttack();
            if (moveDirect == Vector2.zero) { playerAnimation.StopWalking(); }
            else { playerAnimation.StartWalking(); }
        }
        else    //이동키를 누르지 않은 경우
        {
            //대기 애니메이션 적용
            playerAnimation.StopWalking();
            firstDirect = Vector2.zero;

            if (CheckAttackKeyInput())  //공격키를 눌렀는지 확인
            {
                playerAttack.Attack();  //플레이어의 공격
            }
            else { playerAttack.StopAttack(); } //공격 취소
        }
    }


    /* 이동키(방향키) 입력에 관한 처리 */
    /* 방향키를 입력 중일 때만 true를 반환 */
    bool CheckMoveKeyInput()
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
    bool CheckAttackKeyInput()
    {
        return Input.GetButton("Attack");
    }


    /* 캐릭터가 바라볼 방향을 결정 */
    void SetSpriteDirect()
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


}
