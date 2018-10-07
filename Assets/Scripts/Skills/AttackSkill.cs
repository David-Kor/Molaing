using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSkill : Skill
{
    public bool isKnockBack;
    public float knockBackPower;
    public Vector2 attackDirect = Vector2.zero;
    public int damage;

    public AttackSkill(string _name) : base(_name)
    {
    }

    public void SetDamage(int _dmg) { damage = _dmg; }
    public int GetDamage() { return damage; }
}
