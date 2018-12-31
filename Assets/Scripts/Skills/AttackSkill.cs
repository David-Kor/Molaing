using UnityEngine;

public class AttackSkill : Skill
{
    public bool isKnockBack;          //넉백 사용
    public float knockBackPower;    //넉백 강도
    public Vector2 attackDirect = Vector2.zero;     //공격 방향 (동시에 넉백 방향)
    public int damage;

    public AttackSkill(string _name) : base(_name)
    {
    }
}
