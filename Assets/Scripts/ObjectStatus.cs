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

    void Start()
    {
        currentHP = maxHP;
    }


    /* 공격을 받으면 데미지 수치만큼 현재 hp 감소 */
    public void TakeDamage(int _dmg)
    {
        currentHP -= _dmg;
    }


    /* 최대 HP 증가 */
    public void BonusMaxHP(int bonusHP)
    {
        maxHP += bonusHP;
        currentHP += bonusHP;
    }

    /* 최대 HP 감소 */
    public void CancelBonusMaxHP(int bonusHP)
    {
        maxHP -= bonusHP;
        if (currentHP > maxHP) { currentHP = maxHP; }
    }

    /* STR 증가 */
    public void BonusStrength(int bonusSTR) { strength += bonusSTR; }

    /* STR 감소 */
    public void CancelBonusStrength(int bonusSTR) { strength -= bonusSTR; }

    /* AGI 증가 */
    public void BonusAGI(int bonusAGI) { agility += bonusAGI; }

    /* AGI 감소 */
    public void CancelBonusAGI(int bonusAGI) { agility -= bonusAGI; }

    /* INT 증가 */
    public void BonusINT(int bonusINT) { intelligence += bonusINT; }

    /* INT 감소 */
    public void CancelBonusINT(int bonusINT) { intelligence -= bonusINT; }

    /* 기본 공격력(ATK) 증가 */
    public void BonusATK(int bonusATK) { attackDamage += bonusATK; }

    /* 기본 공격력(ATK) 감소 */
    public void CancelBonusATK(int bonusATK) { attackDamage -= bonusATK; }

    /* 공속(ASP) 증가 */
    public void BonusASP(int bonusASP) { attackSpeed += bonusASP; }

    /* 공속(ASP) 감소 */
    public void CancelBonusASP(int bonusASP) { attackSpeed -= bonusASP; }

    /* 이속(MSP) 증가 */
    public void BonusMSP(int bonusMSP) { moveSpeed += bonusMSP; }

    /* 이속(MSP) 감소 */
    public void CancelBonusMSP(int bonusMSP) { moveSpeed -= bonusMSP; }

    /* 넉백(KBP) 증가 */
    public void BonusKBP(int bonusKBP) { knockBackResistance += bonusKBP; }

    /* 넉백(KBP) 감소 */
    public void CancelBonusKBP(int bonusKBP) { knockBackResistance -= bonusKBP; }

    /* 넉백저항(KBR) 증가 */
    public void BonusKBR(int bonusKBR) { knockBackResistance += bonusKBR; }

    /* 넉백저항(KBR) 감소 */
    public void CancelBonusKBR(int bonusKBR) { knockBackResistance -= bonusKBR; }

    /* 경직저항(KBR) 증가 */
    public void BonusHSR(int bonusHSR) { hitStunResistance += bonusHSR; }

    /* 경직저항(KBR) 감소 */
    public void CancelBonusHSR(int bonusHSR) { hitStunResistance -= bonusHSR; }

}
