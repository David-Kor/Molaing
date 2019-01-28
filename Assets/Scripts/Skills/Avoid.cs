using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoid : SupportSkill
{
    public float value;
    public float moveTime;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = null;
    }

    public override void ActivateSkill()
    {
        transform.localEulerAngles = Vector3.zero;
        transform.SetParent(null);
        skillCaster.GetComponent<PlayerControl>().GetPlayerMove().knockBackTimer = moveTime;
        skillCaster.GetComponent<PlayerControl>().SetInvincibleTime(delay);
        skillCaster.GetComponent<Rigidbody2D>().velocity += skillDirection * value;
    }

    public override void ReleaseSkill()
    {

    }
}
