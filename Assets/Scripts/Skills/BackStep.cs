using UnityEngine;

public class BackStep : SupportSkill
{
    public float value;
    public float moveTime;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = effects[0];
    }

    public override void ActivateSkill()
    {
        skillCaster.GetComponent<PlayerControl>().GetPlayerMove().knockBackTimer = moveTime;
        skillCaster.GetComponent<PlayerControl>().SetInvincibleTime(delay);
        skillCaster.GetComponent<Rigidbody2D>().velocity += skillDirection * value * -1;
    }

    public override void ReleaseSkill()
    {

    }
}
