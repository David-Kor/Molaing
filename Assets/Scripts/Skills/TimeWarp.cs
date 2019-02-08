using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeWarp : SupportSkill
{
    public float warpPercentage;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = null;
    }

    public override void ActivateSkill()
    {
        PlayerSkill playerSkill = skillCaster.GetComponentInChildren<PlayerSkill>();

        for (int i = 0; i < playerSkill.skill_List.Length; i++)
        {
            //쿨다운이 남아있을 시 scale만큼 감소
            if (playerSkill.coolTimerList[i] > 0)
            {
                playerSkill.coolTimerList[i] -= playerSkill.GetSkill(i).coolDown * warpPercentage * 0.01f;
            }
        }
    }

    public override void ReleaseSkill()
    {
    }
}
