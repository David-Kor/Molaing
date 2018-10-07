using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObject : MonoBehaviour
{
    private string objName;
    private ObjectControl control;

    void Start()
    {
        control = GetComponentInParent<ObjectControl>();
        objName = control.gameObject.name;
    }


    public void OnHitSkill(Skill _skill)
    {
        //_skill이 AttackSkill의 서브 클래스인 경우 -> 공격 스킬에 맞은 경우
        if (_skill.GetType().IsSubclassOf(typeof(AttackSkill)))
        {
            control.OnHitAttack(_skill as AttackSkill);
        }
    }


    public string GetName() { return objName; }

}
