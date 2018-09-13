using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 30.0f;
    public Vector2 spriteDirect;  //바라보는 방향

    private Vector2 moveDirect;  //움직이는 방향
    private float axisX;
    private float axisY;

    void Start()
    {
        moveDirect = Vector2.zero;
        spriteDirect = Vector2.down;
    }

    void Update()
    {
        if (CheckMoveKeyInput())    //이동키를 눌렀는지 확인
        {
            AxisToDirect();
            MovePlayer();   //플레이어 이동
        }

        if (Input.GetKey("down"))
        {
            moveDirect = Vector2.down;
            if (Input.GetKey("up")) { moveDirect = Vector2.zero; }

            if (Input.GetKey("left")) { moveDirect += Vector2.left; }
            if (Input.GetKey("right")) { moveDirect += Vector2.right; }
        }
        else if (Input.GetKey("up"))
        {
            moveDirect = Vector2.up;
            if (Input.GetKey("down")) { moveDirect = Vector2.zero; }

            if (Input.GetKey("left")) { moveDirect += Vector2.left; }
            if (Input.GetKey("right")) { moveDirect += Vector2.right; }
        }
        else if (Input.GetKey("left"))
        {
            moveDirect = Vector2.left;
            if (Input.GetKey("right")) { moveDirect = Vector2.zero; }

            if (Input.GetKey("down")) { moveDirect += Vector2.down; }
            if (Input.GetKey("up")) { moveDirect += Vector2.up; }
        }
        else if (Input.GetKey("right"))
        {
            moveDirect = Vector2.right;
            if (Input.GetKey("left")) { moveDirect = Vector2.zero; }

            if (Input.GetKey("down")) { moveDirect += Vector2.down; }
            if (Input.GetKey("up")) { moveDirect += Vector2.up; }
        }
    }

    bool CheckMoveKeyInput()
    {
        if (Input.GetKey("down")) { return true; }
        if (Input.GetKey("up")) { return true; }
        if (Input.GetKey("left")) { return true; }
        if (Input.GetKey("right")) { return true; }

        moveDirect = Vector2.zero;
        return false;
    }

    void AxisToDirect()
    {
        axisX = Input.GetAxis("Horizontal");
        axisY = Input.GetAxis("Vertical");

        if (Mathf.Abs(axisX) > Mathf.Abs(axisY))
        {
            if (axisX > 0) { spriteDirect = Vector2.right; }
            else { spriteDirect = Vector2.left; }
        }
        else if (Mathf.Abs(axisX) < Mathf.Abs(axisY))
        {
            if (axisY > 0) { spriteDirect = Vector2.up; }
            else { spriteDirect = Vector2.down; }
        }
    }

    void MovePlayer()
    {
        transform.Translate(moveDirect * speed * Time.deltaTime, Space.World);
    }

}
