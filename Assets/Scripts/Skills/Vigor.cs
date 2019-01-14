using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vigor : SupportSkill
{
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
                //현재 체력이 소모비용보다 많으면 지속데미지
                if (status.currentHP > healHP)
                {
                    status.TakeDamage(healHP);
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
        Vigor healAlready = skillCaster.GetComponentInChildren<Vigor>();
        if (healAlready != null)
        {
            Destroy(healAlready.gameObject);
            Destroy(gameObject);
            return;
        }
        
        transform.SetParent(skillCaster.transform);
        timer = 0;
        status = skillCaster.GetComponent<PlayerStatus>();
        isActSkill = true;
    }


    public override void ReleaseSkill()
    {
    }
}
