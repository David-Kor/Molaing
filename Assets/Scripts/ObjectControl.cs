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

    public void OnGround(string col_tag)
    {
        onGround = true;
        StartCoroutine("VelocityYCheck");
        if (col_tag == "Wall")
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ground"), true);
        }
    }

    public void ExitGround()
    {
        onGround = false;
        StartCoroutine("VelocityYCheck");
    }


    protected IEnumerator VelocityYCheck()
    {
        bool f_detect = false;
        while (true)
        {
            if (rigid.velocity.y > 0)
            {
                isJumping = true;
                isFalling = false;
            }
            else if (rigid.velocity.y < 0)
            {
                isFalling = true;
                if (isFalling != f_detect)
                {
                    f_detect = isFalling;
                    Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ground"), false);
                }
            }
            else
            {
                isFalling = false;
                isJumping = false;
                StopCoroutine("VelocityYCheck");
            }

            yield return null;
        }
    }

    /* 공격받은 경우 호출되는 함수 */
    public abstract void OnHitAttack(AttackSkill _skill);

    /* 쿨타임 활성화 함수 */
    public abstract void CoolDownActive(int _index, float _value);
}
