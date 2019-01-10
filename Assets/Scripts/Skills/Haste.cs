using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haste : SupportSkill
{
    public float intervalTime;      //잔상 생성 간격
    public float durationTime;    //잔상 지속시간
    public GameObject effectObj;
    public float value;

    private float timer;
    private GameObject effectClone;
    private PlayerControl player_c;

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

        SkillProduction();
        timer += Time.deltaTime;

        //잔상 생성 시간이 되었고, 플레이어가 움직이면 잔상을 생성
        if (timer >= intervalTime)
        {
            if (player_c.GetMoveDirection() == Vector2.zero) { return; }

            timer = 0;
            effectClone = Instantiate(effectObj);
            effectClone.transform.position = transform.position;
            effectClone.transform.rotation = skillCaster.transform.GetChild(0).rotation;
            effectClone.GetComponent<AffterImage>().Init(skillCaster.GetComponent<SpriteRenderer>(),
                new Sprite[1] { skillCaster.GetComponentInChildren<SpriteRenderer>().sprite }
                , durationTime);
        }
    }

    public override void ActivateSkill()
    {
        timer = 0;
        player_c = skillCaster.GetComponent<PlayerControl>();
        player_c.GetPlayerStatus().moveSpeed += value;
    }
    public override void ReleaseSkill()
    {
        skillCaster.GetComponent<PlayerControl>().GetPlayerStatus().moveSpeed -= value;
    }
}
