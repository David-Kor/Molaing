using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : ObjectStatus
{
    public float gracePeriod;   //피격 시 무적 지속시간
    public float currentEXP;    //경험치
    public float requireEXP;    //다음 레벨까지 필요한 경험치
    public int statusPoint = 0;      //스탯을 올릴 수 있는 횟수
    

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
    }

}
