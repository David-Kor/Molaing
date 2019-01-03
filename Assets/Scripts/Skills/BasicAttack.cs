
/*
 * Skill -> AttackSkill -> BasicAttack 상속 관계
 * 기본 공격에 관한 정보를 갖는 클래스.
 */

using System.Collections.Generic;

public class BasicAttack : AttackSkill
{
    public override void ActivateSkill()
    {
        List<HitObject> hitObjects = OnHitCheck();

        for (int i = 0; i < hitObjects.Count; i++)
        {
            hitObjects[i].OnHitSkill(this);
        }
    }

    public override void ReleaseSkill()
    {
    }

}
