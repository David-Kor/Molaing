using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObject : MonoBehaviour
{
    private ObjectStatus status;

    void Start()
    {
        status = GetComponentInParent<ObjectStatus>();
    }


    public void OnHitSkill(Skill _skill)
    {
        //_skill이 AttackSkill의 서브 클래스인 경우 -> 공격 스킬에 맞은 경우
        if (_skill.GetType().IsSubclassOf(typeof(AttackSkill)))
        {
            Debug.Log((_skill as AttackSkill).damage);
            //스탯에 피해량(damage) 정보를 넘김
            status.TakeDamage((_skill as AttackSkill).damage);
        }
    }

}
