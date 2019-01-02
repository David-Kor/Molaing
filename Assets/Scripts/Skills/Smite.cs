using UnityEngine;

/*
 * Sample Child Of Skill
 */
public class Smite : AttackSkill
{
    public override void ActivateSkill()
    {
        Collider2D[] hits = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D()
        {
            useTriggers = true,     //isTrigger 충돌체도 검사함
            useLayerMask = true,  //레이어 마스크 사용함

            //HitPoint 레이어 외 전부 무시
            layerMask = new LayerMask() { value = (1 << LayerMask.NameToLayer("HitPoint")) }
        };

        int count = Physics2D.OverlapCollider(GetComponent<Collider2D>(), filter, hits);

        HitObject hitObj;
        for (int i = 0; i < count; i++)
        {
            hitObj = hits[i].GetComponent<HitObject>();

            //시전자와 대상이 같으면 무시
            if (skillCaster.GetComponentInChildren<HitObject>().GetName() == hitObj.GetName()) { continue; }

            hitObj.OnHitSkill(this);
        }
    }

    public override void ReleaseSkill()
    {

    }
}
