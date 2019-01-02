using System.Collections.Generic;
using UnityEngine;

/* 모든 스킬들의 최상위 가상 클래스 */
public abstract class Skill : MonoBehaviour
{
    public string skillName;            //스킬명
    public GameObject skillCaster;   //시전자
    public Sprite skill_IMG;             //스킬 이미지(UI)
    public float coolDown;             //쿨타임
    public float delay;                   //스킬 딜레이(후딜)
    public Vector2 skillDirect = Vector2.zero;     //스킬 시전 방향 및 넉백 방향
    public Sprite[] effects;              //스킬 이펙트 이미지
    public float frameTime;            //이펙트 이미지 1개당 출력시간
    public bool isLoop;                 //이펙트 루프
    public float lifeTime;               //(루프 사용 시)스킬 지속시간

    private float e_timer;  //이펙트 타이머
    private float l_timer;   //지속시간 타이머
    private int index;       //현재 출력될 이펙트 이미지

    void Start()
    {
        l_timer = 0f;
        e_timer = frameTime;
        index = 0;

    }

    void Update()
    {
        SkillProduction();
    }


    protected void SkillProduction()
    {
        if (effects == null) { return; }
        e_timer += Time.deltaTime;
        l_timer += Time.deltaTime;

        if (isLoop)
        {
            if (l_timer >= lifeTime)
            {
                ReleaseSkill();
                Destroy(gameObject);
            }
            else if (index >= effects.Length) { index = 0; }
        }

        if (e_timer >= frameTime)
        {
            if (!isLoop && index >= effects.Length)
            {
                ReleaseSkill();
                Destroy(gameObject);
            }

            if (index < effects.Length) { GetComponent<SpriteRenderer>().sprite = effects[index]; }
            e_timer = 0;
            index++;
        }
    }


    public abstract void ActivateSkill();


    public abstract void ReleaseSkill();
}
