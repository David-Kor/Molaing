using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackCountPerSec = 2.0f;  //초당 기본 공격횟수

    private PlayerAnimation playerAnimation;
    public float basicAttackTimer;
    private bool isAttackInput;

    void Start()
    {
        playerAnimation = transform.parent.GetComponentInChildren<PlayerAnimation>();
        basicAttackTimer = 0f;
        isAttackInput = false;
    }

    void Update()
    {
        //타이머가 초기화 되지 않았고, 공격 버튼을 누르지 않은 상태
        if (basicAttackTimer > 0)
        {
            basicAttackTimer -= Time.deltaTime;
        }

        //공격 버튼이 눌려있는 상태
        if (isAttackInput)
        {
            if (basicAttackTimer <= 0)
            {
                basicAttackTimer = 1 / attackCountPerSec;
                playerAnimation.SetAttackMotionSpeed(attackCountPerSec);
                playerAnimation.StartAttack();
                Debug.Log("Attack!");
            }
        }
        else { playerAnimation.StopAttack(); }
    }


    public void Attack() { isAttackInput = true; }

    public void StopAttack() { isAttackInput = false; }
}
