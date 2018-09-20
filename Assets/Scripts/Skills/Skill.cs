using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillType = EnumInterface.TYPE_OF_SKILL;

/* 모든 스킬들의 최상위 가상 클래스 */
public abstract class Skill : MonoBehaviour
{
    public SkillType type;
    public GameObject[] target;
    public int damage;

    /* 생성자 */
    public Skill(SkillType _typeOfSkill, string _skillName)
    {
        type = _typeOfSkill;
        name = _skillName;
    }

    public abstract void AttackRange(Vector2 _position, float _range);
    public abstract void AttackRange(Vector2 _position, Vector2 _range);
}
