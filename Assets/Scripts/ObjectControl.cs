using System.Collections;
using UnityEngine;

public abstract class ObjectControl : MonoBehaviour
{
    protected Rigidbody2D rigid;

    public bool onGround;
    public bool isFalling;
    public bool isJumping;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity += Vector2.down * 0.001f;
        StartCoroutine("VelocityYCheck");
    }

    
    /* 땅 위에 착지할 때 호출됨 */
    public abstract void OnGround(string col_tag);


    /* 땅에서 발이 떨어지면 호출됨 */
    public void ExitGround()
    {
        onGround = false;
        StartCoroutine("VelocityYCheck");
    }


    /* Y방향 속도에 따라 변수 조정 */
    protected abstract IEnumerator VelocityYCheck();
    

    /* 공격받은 경우 호출되는 함수 */
    public abstract void OnHitAttack(AttackSkill _skill);


    /* 쿨타임 활성화 함수 */
    public abstract void CoolDownActive(int _index, float _value);
}
