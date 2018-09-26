
/*
 * Skill -> AttackSkill -> BasicAttack 상속 관계
 * 기본 공격에 관한 정보를 갖는 클래스.
 */

public class BasicAttack : AttackSkill
{
    public BasicAttack(string _name = "Basic Attack") : base(_name)
    {
    }
}
