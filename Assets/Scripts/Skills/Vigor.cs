using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //버서커 상태가 이미 활성화되어 있으면 비활성화시키고 오브젝트 제거
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
