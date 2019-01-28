using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 스킬 : 활력
 * 분류 : 활성화/비활성화 버프 스킬
 * 효과 : 일정시간마다 지속적으로 체력회복
 *         이동속도 감소
 */
public class Vigor : SupportSkill
{
    public float costMSP;
    public float healTimeCycle;
    public int healHP;

    private bool isActSkill;
    private float timer;
    private PlayerStatus status;

    void Start()
    {
        timer = 0;
        GetComponent<SpriteRenderer>().sprite = null;
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
            if (timer >= healTimeCycle)
            {
                timer = 0;
                status.TakeDamage(healHP * -1);
            }
        }
    }


    public override void ActivateSkill()
    {
        transform.SetParent(null);
        //활력 상태가 이미 활성화되어 있으면 비활성화시키고 오브젝트 제거
        Vigor healAlready = skillCaster.GetComponentInChildren<Vigor>();
        if (healAlready != null)
        {
            healAlready.ReleaseSkill();
            Destroy(healAlready.gameObject);
            Destroy(gameObject);
            skillCaster.GetComponent<PlayerControl>().SetIsDelay(false);
            return;
        }
        
        transform.SetParent(skillCaster.transform);
        timer = 0;
        status = skillCaster.GetComponent<PlayerStatus>();
        isActSkill = true;
        status.CancelBonusMSP(costMSP);
    }


    public override void ReleaseSkill()
    {
        if (status != null) { skillCaster.GetComponent<PlayerStatus>(); }
        status.BonusMSP(costMSP);
    }
}
