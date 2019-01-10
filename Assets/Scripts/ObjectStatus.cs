using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 모든 오브젝트의 스탯 클래스
 * 각 NPC(미정), 플레이어, 몬스터로 나뉘어 상속받는다.
 */
public abstract class ObjectStatus : MonoBehaviour
{
    public string objName;

    public int level;   //레벨

    public int maxHP;       //최대 체력(HP)
    public int currentHP;   //현재 체력

    public int strength;      //힘(STR)
    public int agility;         //민첩(AGI)
    public int intelligence;  //지력(INT)

    public int attackDamage;    //기본 공격력(ATK)
    public float attackSpeed;    //초당 기본 공격횟수(ASP)
    public float moveSpeed;     //이동 속도(MSP)

    public float knockBackPower;  //넉백 수치

    public int knockBackResistance;  //넉백 저항률(KBR)
    public int hitStunResistance;      //피격 시 경직 저항률(HSR)

    void Awake()
    {
        currentHP = maxHP;
    }


    /* 공격을 받으면 데미지 수치만큼 현재 hp 감소 */
    public abstract void TakeDamage(int _dmg);

}
