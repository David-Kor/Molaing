using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : ObjectStatus
{
    /* 연산 방식이 변경(플레이어스탯)되었으므로 참조하는 모든 변수들에 대한 변경사항 적용할 것 */
    public float gracePeriodTime; //피격 시 무적 지속시간(GPT)
    public float currentEXP;        //경험치
    public float requireEXP;        //다음 레벨까지 필요한 경험치
    public int statusPoint = 0;     //스탯을 올릴 수 있는 횟수(SP)

    private int bonusHP;            //추가 체력

    private int bonusSTR;           //추가 힘
    private int bonusAGI;           //추가 민첩
    private int bonusINT;           //추가 지력

    private int bonusATK;           //추가 공격력
    private float bonusASP;        //추가 공속
    private float bonusMSP;       //추가 이속
    private float bonusJMP;       //추가 점프력

    private float bonusKBP;       //추가 넉백
    private int bonusKBR;         //추가 넉백저항
    private int bonusHSR;         //추가 경직저항

    private float bonusGPT;       //추가 무적시간

    private UI_Controller ui;

    void Start()
    {
        ui = Camera.main.GetComponent<UI_Controller>();
    }

    /* 레벨 업에 관한 처리 */
    public void LevelUp()
    {
        level++;
        currentEXP -= requireEXP;
        statusPoint += 5;
        MaxHP_Up((maxHP / 100) + 1);
    }


    /* 경험치 획득 */
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


    /* 최대 체력 최종값 반환 */
    public int GetMaxHP() { return maxHP + bonusHP; }
    /* 최대 HP 영구 증가 */
    public void MaxHP_Up(int bonus)
    {
        maxHP += bonus;
        currentHP += bonus;     //추가된 최대 HP만큼 현재 HP 추가
        ui.HP(currentHP, GetMaxHP());
    }
    /* 추가 최대 HP 증가 */
    public void BonusMaxHP(int bonus)
    {
        bonusHP += bonus;
        currentHP += bonus;     //추가된 최대 HP 만큼 현재 HP 증가
        ui.HP(currentHP, GetMaxHP());
    }
    /* 추가 최대 HP 감소 */
    public void CancelBonusMaxHP(int bonus)
    {
        bonusHP -= bonus;
        if (currentHP > bonus) { currentHP -= bonus; }  //감소된 최대 HP만큼 현재 HP 감소
        ui.HP(currentHP, GetMaxHP());
    }


    /* STR 최종값 반환 */
    public int GetSTR() { return strength + bonusSTR; }
    /* STR 영구 증가 */
    public void STR_Up(int bonus) { strength += bonus; }
    /* 추가 STR 증가 */
    public void BonusSTR(int bonus) { bonusSTR += bonus; }
    /* 추가 STR 감소 */
    public void CancelBonusSTR(int bonus) { bonusSTR -= bonus; }


    /* AGI 최종값 반환 */
    public int GetAGI() { return agility + bonusAGI; }
    /* AGI 영구 증가 */
    public void AGI_Up(int bonus) { agility += bonus; }
    /* 추가 AGI 증가 */
    public void BonusAGI(int bonus) { bonusAGI += bonus; }
    /* 추가 AGI 감소 */
    public void CancelBonusAGI(int bonus) { bonusAGI -= bonus; }


    /* INT 최종값 반환 */
    public int GetINT() { return intelligence + bonusINT; }
    /* INT 영구 증가 */
    public void INT_Up(int bonus) { intelligence += bonus; }
    /* 추가 INT 증가 */
    public void BonusINT(int bonus) { bonusINT += bonus; }
    /* 추가 INT 감소 */
    public void CancelBonusINT(int bonus) { bonusINT -= bonus; }


    /* 공격력 최종값 반환 */
    public int GetATK() { return attackDamage + bonusATK; }
    /* 공격력(ATK) 영구 증가 */
    public void ATK_Up(int bonus) { attackDamage += bonus; }
    /* 추가 공격력(ATK) 증가 */
    public void BonusATK(int bonus) { bonusATK += bonus; }
    /* 추가 공격력(ATK) 감소 */
    public void CancelBonusATK(int bonus) { bonusATK -= bonus; }


    /* 공격속도 최종값 반환 */
    public float GetASP() { return attackSpeed + bonusASP; }
    /* 공속(ASP) 증가 */
    public void ASP_Up(float bonus) { attackSpeed += bonus; }
    /* 공속(ASP) 증가 */
    public void BonusASP(float bonus) { bonusASP += bonus; }
    /* 공속(ASP) 감소 */
    public void CancelBonusASP(float bonus) { bonusASP -= bonus; }


    /* 이동속도 최종값 반환 */
    public float GetMSP() { return moveSpeed + bonusMSP; }
    /* 이속(MSP) 영구 증가 */
    public void MSP_Up(float bonus) { moveSpeed += bonus; }
    /* 추가 이속(MSP) 증가 */
    public void BonusMSP(float bonus) { bonusMSP += bonus; }
    /* 추가 이속(MSP) 감소 */
    public void CancelBonusMSP(float bonus) { bonusMSP -= bonus; }


    /* 점프력 최종값 반환 */
    public float GetJMP() { return jumpPower + bonusJMP; }
    /* 점프력(JMP) 영구 증가 */
    public void JMP_Up(float bonus) { jumpPower += bonus; }
    /* 추가 점프력(JMP) 증가 */
    public void BonusJMP(float bonus) { bonusJMP += bonus; }
    /* 추가 점프력(JMP) 감소 */
    public void CancelBonusJMP(float bonus) { bonusJMP -= bonus; }


    /* 넉백 최종값 반환 */
    public float GetKBP() { return knockBackPower + bonusKBP; }
    /* 넉백(KBP) 영구 증가 */
    public void KBP_Up(float bonus) { knockBackPower += bonus; }
    /* 추가 넉백(KBP) 증가 */
    public void BonusKBP(float bonus) { bonusKBP += bonus; }
    /* 추가 넉백(KBP) 감소 */
    public void CancelBonusKBP(float bonus) { bonusKBP -= bonus; }


    /* 넉백저항 최종값 반환 */
    public float GetKBR()
    {
        if ((knockBackResistance + bonusKBR) >= 100) { return 100f; }
        else { return knockBackResistance + bonusKBR; }
    }
    /* 넉백저항(KBR) 영구 증가 */
    public void KBR_Up(int bonus) { knockBackResistance += bonus; }
    /* 추가 넉백저항(KBR) 증가 */
    public void BonusKBR(int bonus) { bonusKBR += bonus; }
    /* 추가 넉백저항(KBR) 감소 */
    public void CancelBonusKBR(int bonus) { bonusKBR -= bonus; }

    
    /* 경직저항 최종값 반환 */
    public float GetHSR()
    {
        if ((hitStunResistance + bonusHSR) >= 100) { return 100f; }
        else { return hitStunResistance + bonusHSR; }
    }
    /* 경직저항(HSR) 영구 증가 */
    public void HSR_Up(int bonus) { hitStunResistance += bonus; }
    /* 추가 경직저항(HSR) 증가 */
    public void BonusHSR(int bonus) { bonusHSR += bonus; }
    /* 추가 경직저항(HSR) 감소 */
    public void CancelBonusHSR(int bonus) { bonusHSR -= bonus; }


    /* 무적 지속시간 최종값 반환 */
    public float GetGPT() { return gracePeriodTime + bonusGPT; }
    /* 무적시간(GPT) 영구 증가 */
    public void GPT_Up(int bonus) { gracePeriodTime += bonus; }
    /* 추가 무적시간(GPT) 증가 */
    public void BonusGPT(int bonus) { bonusGPT += bonus; }
    /* 추가 무적시간(GPT) 감소 */
    public void CancelBonusGPT(int bonus) { bonusGPT -= bonus; }


    public override void TakeDamage(int _dmg)
    {
        currentHP -= _dmg;
        if (currentHP > maxHP) { currentHP = maxHP; }
        ui.HP(currentHP, GetMaxHP());
    }
}
