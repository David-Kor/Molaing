using UnityEngine;

public class Dash : SupportSkill
{
    public float value;
    public float dashTime;
    public GameObject effectObj;

    private float timer;
    private GameObject effectClone;

    void Start()
    {
        timer = 0;
    }

    void Update()
    {
        if (!f_delayEnd)
        {
            WaitFirstDelay();
            return;
        }

        timer += Time.deltaTime;
        SkillProduction();
        if (timer >= 0.03f)
        {
            timer = 0;
            effectClone = Instantiate(effectObj);
            effectClone.transform.position = transform.position;
            effectClone.GetComponent<AffterImage>().Init(skillCaster.GetComponent<SpriteRenderer>(), null, 0);
        }
    }

    public override void ActivateSkill()
    {
        timer = 0;
        skillCaster.GetComponent<PlayerControl>().GetPlayerMove().knockBackTimer = dashTime;
        skillCaster.GetComponent<Rigidbody2D>().velocity = skillDirection * value;
    }


    public override void ReleaseSkill()
    {
    }
}
