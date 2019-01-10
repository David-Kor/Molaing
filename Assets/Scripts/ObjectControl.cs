using UnityEngine;

public abstract class ObjectControl : MonoBehaviour
{
    /* 공격받은 경우 호출되는 함수 */
    public abstract void OnHitAttack(AttackSkill _skill);

    /* 쿨타임 활성화 함수 */
    public abstract void CoolDownActive(int _index, float _value);
}
