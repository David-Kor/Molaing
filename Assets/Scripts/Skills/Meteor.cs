using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : AttackSkill
{
    public float spawnHeight;       //메테오 생성 높이(로컬)
    public float spawnWidth;  //메테오 생성 폭(로컬, 절반)
    public float moveSpeed;         //스포너 이동속도
    public float fallingSpeed;        //메테오 낙하속도
    public float createDelay;         //생성 주기
    public GameObject bullet_meteor;        //메테오 프리팹

    private bool doCreate;
    private Vector2 horDirection;

    void Start()
    {
        doCreate = false;
        GetComponent<SpriteRenderer>().sprite = null;
        horDirection = Vector2.left;
    }

    void Update()
    {
        if (!f_delayEnd)
        {
            WaitFirstDelay();
            return;
        }
        SkillProduction();

        if (skillDirection.x > 0)
        {
            if (transform.localPosition.x < (spawnWidth * -0.75))
            {
                horDirection = Vector2.right;
            }
            if (transform.localPosition.x > (spawnWidth * 0.25))
            {
                horDirection = Vector2.left;
            }
        }
        else
        {
            if (transform.localPosition.x < (spawnWidth * -0.25))
            {
                horDirection = Vector2.right;
            }
            if (transform.localPosition.x > (spawnWidth * 0.75))
            {
                horDirection = Vector2.left;
            }
        }

        if (doCreate)
        {
            transform.Translate(horDirection * moveSpeed * Time.deltaTime, Space.World);
        }
    }


    /* 목표 높이까지 상승 */
    private IEnumerator ShootUp()
    {
        while (transform.localPosition.y < spawnHeight)
        {
            transform.Translate(Vector2.up * fallingSpeed * Time.deltaTime, Space.World);
            yield return null;
        }
        doCreate = true;
    }


    /* 일정 주기마다 메테오 생성 */
    private IEnumerator CreateMeteor()
    {
        while (!doCreate) { yield return null; }
        while (doCreate)
        {
            Instantiate(bullet_meteor, transform).GetComponent<Bullet>()
                .InitInfo(this, Vector2.down + skillDirection, fallingSpeed, lifeTime);
            yield return new WaitForSeconds(createDelay);
        }
    }


    public override void ActivateSkill()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        StartCoroutine("ShootUp");
        StartCoroutine("CreateMeteor");
    }

    public override void ReleaseSkill()
    {
        doCreate = false;
        StopCoroutine("CreateMeteor");
    }
}
