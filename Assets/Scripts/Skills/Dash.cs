using UnityEngine;

public class Dash : SupportSkill
{
    public float value;
    public float dashTime;
    public GameObject effectObj;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        SkillProduction();
        if (timer >= 0.03f)
        {
            timer = 0;
            Instantiate(effectObj).transform.position = transform.position;
        }
    }

    public override void ActivateSkill()
    {
        timer = 0;
        skillCaster.GetComponent<PlayerControl>().GetPlayerMove().knockBackTimer = dashTime;
        skillCaster.GetComponent<Rigidbody2D>().velocity = skillDirect * value;
    }


    public override void ReleaseSkill()
    {
    }
}
