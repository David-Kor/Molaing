using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : ObjectStatus
{
    public float gracePeriod;   //피격 시 무적 지속시간
    public float currentEXP;    //경험치
    public float requireEXP;    //다음 레벨까지 필요한 경험치
    public int statusPoint = 0;      //스탯을 올릴 수 있는 횟수

    private UI_Controller ui;

    void Start()
    {
        ui = Camera.main.GetComponent<UI_Controller>();
        Debug.Log(ui);
    }

    /* 레벨 업에 관한 처리 */
    public void LevelUp()
    {
        level++;
        currentEXP -= requireEXP;
        statusPoint += 5;
        BonusMaxHP((maxHP / 100) + 1);
    }


    public void EXP_Up(float exp)
    {
        currentEXP += exp;

        //필요한 경험치를 모두 모은 경우
        if (currentEXP >= requireEXP)
        {
            Debug.Log("레벨 업!");
            LevelUp();
        }
        ui.Exp(currentEXP, requireEXP, level);
    }



    /* 최대 HP 증가 */
    public void BonusMaxHP(int bonusHP)
    {
        maxHP += bonusHP;
        currentHP += bonusHP;
        ui.HP(currentHP, maxHP);
    }

    /* 최대 HP 감소 */
    public void CancelBonusMaxHP(int bonusHP)
    {
        maxHP -= bonusHP;
        if (currentHP > maxHP) { currentHP = maxHP; }
        ui.HP(currentHP, maxHP);
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
    public void BonusASP(float bonusASP) { attackSpeed += bonusASP; }

    /* 공속(ASP) 감소 */
    public void CancelBonusASP(float bonusASP) { attackSpeed -= bonusASP; }

    /* 이속(MSP) 증가 */
    public void BonusMSP(float bonusMSP) { moveSpeed += bonusMSP; }

    /* 이속(MSP) 감소 */
    public void CancelBonusMSP(float bonusMSP) { moveSpeed -= bonusMSP; }

    /* 넉백(KBP) 증가 */
    public void BonusKBP(float bonusKBP) { knockBackPower += bonusKBP; }

    /* 넉백(KBP) 감소 */
    public void CancelBonusKBP(float bonusKBP) { knockBackPower -= bonusKBP; }

    /* 넉백저항(KBR) 증가 */
    public void BonusKBR(int bonusKBR)
    {
        knockBackResistance += bonusKBR;
        if (knockBackResistance > 100) { knockBackResistance = 100; }
    }

    /* 넉백저항(KBR) 감소 */
    public void CancelBonusKBR(int bonusKBR) { knockBackResistance -= bonusKBR; }

    /* 경직저항(KBR) 증가 */
    public void BonusHSR(int bonusHSR)
    {
        hitStunResistance += bonusHSR;
        if (hitStunResistance > 100) { hitStunResistance = 100; }
    }

    /* 경직저항(KBR) 감소 */
    public void CancelBonusHSR(int bonusHSR) { hitStunResistance -= bonusHSR; }

    public override void TakeDamage(int _dmg)
    {
        currentHP -= _dmg;
        ui.HP(currentHP, maxHP);
    }
}
