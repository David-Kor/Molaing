using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Vector2 spriteDirect;  //바라보는 방향

    private PlayerMove playerMove;
    private Vector2 moveDirect;  //움직이는 방향
    private float axisX;    //수평 입력값 (-1 ~ 1)
    private float axisY;    //수직 입력값 (-1 ~ 1)
    private bool isSimultaneously;  //수직·수평 방향키를 동시에 입력했는가

    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        moveDirect = Vector2.zero;
        spriteDirect = Vector2.down;
    }

    void Update()
    {
        if (CheckMoveKeyInput())    //이동키를 눌렀는지 확인
        {
            AxisToDirect();
            playerMove.MovePlayer(moveDirect);   //플레이어 이동
        }
    }

    /* 이동키(방향키) 입력에 관한 모든 내용 */
    /* 방향키를 입력 중일 때만 true를 반환 */
    bool CheckMoveKeyInput()
    {
        //수직/수평 동시 입력
        if ((Input.GetKeyDown("down") || Input.GetKeyDown("up"))
            && (Input.GetKeyDown("right") || Input.GetKeyDown("left")))
        {
            isSimultaneously = true;
        }
        else { isSimultaneously = false; }

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


    /* 수직·수평 방향키의 입력값으로 캐릭터가 바라볼 방향을 결정 */
    void AxisToDirect()
    {
        //수직·수평 방향 입력값을 비교하여 더 큰 쪽이 (먼저)입력된 방향이다. 최댓값은 1
        axisX = Input.GetAxis("Horizontal");    //수평 방향 입력값
        axisY = Input.GetAxis("Vertical");  //수직 방향 입력값

        //수평 방향 입력값이 더 큰경우
        if (Mathf.Abs(axisX) > Mathf.Abs(axisY)
            || isSimultaneously)    //완전히 동시 입력한 경우를 포함  => 수평 우선
        {
            if (axisX > 0) { spriteDirect = Vector2.right; }
            else { spriteDirect = Vector2.left; }
        }
        //수직 방향 입력값이 더 큰경우
        else if (Mathf.Abs(axisX) < Mathf.Abs(axisY))
        {
            if (axisY > 0) { spriteDirect = Vector2.up; }
            else { spriteDirect = Vector2.down; }
        }

        //** 수직·수평 입력값이 같은 경우를 따로 지정해놓지 않았기 때문에
        //   서로 최댓값(1)이 되어 같아 지더라도 바라보는 방향은 변화하지 않음
    }
}
