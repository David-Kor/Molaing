using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 스킬 : 광폭
 * 분류 : 활성화/비활성화 버프 스킬
 * 효과 : 일정시간마다 지속적으로 체력감소
 *         공격력, 공격속도, 넉백, 넉백저항력, 경직저항력 증가
 */
public class Berserk : SupportSkill
{
    public float costTimeCycle;
    public int costHP;
    public int bonusATK;
    public float bonusASP;
    public float bonusKBP;
    public int bonusKBR;
    public int bonusHSR;

    private bool isActSkill;
    private float timer;
    private PlayerStatus status;

    void Start()
    {
        timer = 0;
    }

    void Update()
    {
        if (!f_delayEnd)
        {
            WaitFirstDelay();
            return;
        }

        if (isActSkill)
        {
            timer += Time.deltaTime;
            if (timer >= costTimeCycle)
            {
                timer = 0;
                //현재 체력이 소모비용보다 많으면 지속데미지
                if (status.currentHP > costHP)
                {
                    status.TakeDamage(costHP);
                }
                //현재 체력이 모자라면 비활성화시키고 제거
                else
                {
                    ReleaseSkill();
                    Destroy(gameObject);
                }
            }
        }
    }


    public override void ActivateSkill()
    {
        transform.SetParent(null);
        //버서커 상태가 이미 활성화되어 있으면 비활성화시키고 오브젝트 제거
        Berserk berserkAlready = skillCaster.GetComponentInChildren<Berserk>();
        if (berserkAlready != null)
        {
            berserkAlready.ReleaseSkill();
            Destroy(berserkAlready.gameObject);
            Destroy(gameObject);
            skillCaster.GetComponent<PlayerControl>().SetIsDelay(false);
            return;
        }
        
        transform.SetParent(skillCaster.transform);
        timer = 0;
        status = skillCaster.GetComponent<PlayerStatus>();
        //현재 체력이 부족하면 활성화되지 않음
        if (status.currentHP <= costHP)
        {
            Destroy(gameObject);
            return;
        }
        isActSkill = true;
        status.BonusATK(bonusATK);
        status.BonusASP(bonusASP);
        status.BonusKBP(bonusKBP);
        status.BonusKBR(bonusKBR);
        status.BonusHSR(bonusHSR);
    }


    public override void ReleaseSkill()
    {
        if (status == null)
        {
            status = skillCaster.GetComponent<PlayerStatus>();
        }
        status.CancelBonusATK(bonusATK);
        status.CancelBonusASP(bonusASP);
        status.CancelBonusKBP(bonusKBP);
        status.CancelBonusKBR(bonusKBR);
        status.CancelBonusHSR(bonusHSR);
    }
}
