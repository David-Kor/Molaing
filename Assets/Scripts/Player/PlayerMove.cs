using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float hitStunTimer;           //경직 타이머
    public float knockBackTimer;        //넉백 타이머

    private PlayerStatus playerStatus;  //플레이어의 스탯 클래스
    private PlayerControl playerControl;
    private Rigidbody2D rigid2D;       //물리 클래스
    private const float DEFAULT_HIT_STUN_TIME = 0.25f;  //피격시 경직시간 기본값
    private const float DEFAULT_KNOCK_BACK_TIME = 0.1f;  //넉백시간 기본값

    void Start()
    {
        playerControl = GetComponent<PlayerControl>();
        playerStatus = GetComponent<PlayerStatus>();
        rigid2D = GetComponent<Rigidbody2D>();
        hitStunTimer = 0;
        knockBackTimer = 0;
    }

    void Update()
    {
        if (hitStunTimer > 0) { hitStunTimer -= Time.deltaTime; }
        if (knockBackTimer > 0) { knockBackTimer -= Time.deltaTime; }

        if (rigid2D.velocity.x != 0f && knockBackTimer <= 0)
        {
            rigid2D.velocity = rigid2D.velocity.y * Vector2.up;
        }
    }


    /* 경직 지속 시간동안 입력 불가 */
    private IEnumerator StunBlock(float _time)
    {
        playerControl.SetIsDelay(true);
        yield return new WaitForSeconds(_time);
        playerControl.SetIsDelay(false);
    }


    /* 해당 방향으로 이동 */
    public void Move(Vector2 _moveDirection)
    {
        if (hitStunTimer > 0) { return; }
        transform.Translate(_moveDirection * playerStatus.moveSpeed * Time.deltaTime, Space.World);
    }


    public void Jump(float force)
    {
        if (hitStunTimer > 0) { return; }
        rigid2D.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }

    /* 피격 시 경직 적용 */
    public void HitStun()
    {
        hitStunTimer = (DEFAULT_HIT_STUN_TIME) * (100 - playerStatus.hitStunResistance) / 100;
        StartCoroutine("StunBlock", hitStunTimer);
    }


    /* 넉백 적용 */
    public void KnockBack(Vector2 dir_dist)
    {
        knockBackTimer = DEFAULT_KNOCK_BACK_TIME;
        rigid2D.velocity = dir_dist * (100 - playerStatus.knockBackResistance) / 100 + rigid2D.velocity.y * Vector2.up;
    }
}
