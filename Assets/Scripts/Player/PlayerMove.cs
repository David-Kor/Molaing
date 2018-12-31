using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private PlayerStatus playerStatus;
    private Rigidbody2D rigid2D;

    private const float DEFAULT_HIT_STUN_TIME = 0.25f;
    private float hitStunTimer;

    void Start()
    {
        playerStatus = GetComponent<PlayerStatus>();
        rigid2D = GetComponent<Rigidbody2D>();
        hitStunTimer = 0;
    }

    void Update()
    {
        if (hitStunTimer > 0)
        {
            hitStunTimer -= Time.deltaTime;
        }

        if (rigid2D.velocity != Vector2.zero && hitStunTimer <= 0)
        {
            rigid2D.velocity = Vector2.zero;
        }
    }


    public void Move(Vector2 _moveDirect)
    {
        if (hitStunTimer > 0) { return; }

        transform.Translate(_moveDirect * playerStatus.moveSpeed * Time.deltaTime, Space.World);
    }

    public void HitStun() { hitStunTimer = DEFAULT_HIT_STUN_TIME * (100 - playerStatus.hitStunResistance) / 100; }

    public void KnockBack(Vector2 dir_dist)
    {
        rigid2D.velocity = dir_dist * (100 - playerStatus.knockBackResistance) / 100;
    }
}
