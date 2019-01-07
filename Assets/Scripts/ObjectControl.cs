using UnityEngine;

public abstract class ObjectControl : MonoBehaviour
{
    /* 공격받은 경우 호출되는 함수 */
    public abstract void OnHitAttack(AttackSkill _skill);
}
