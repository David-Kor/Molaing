using UnityEngine;

public abstract class AttackSkill : Skill
{
    public bool isKnockBack;          //넉백 사용
    public float knockBackPower;    //넉백 강도
    public int damage;

    public override abstract void ActivateSkill();


    public override abstract void ReleaseSkill();
}
