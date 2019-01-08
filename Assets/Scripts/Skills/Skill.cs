using System.Collections.Generic;
using UnityEngine;

/* 모든 스킬들의 최상위 가상 클래스 */
public abstract class Skill : MonoBehaviour
{
    public string skillName;            //스킬명
    public GameObject skillCaster;   //시전자
    public int maxHitCount = 10;    //최대 Hit 대상의 수
    public float coolDown;             //쿨타임
    public float delay;                   //스킬 딜레이(후딜)
    public bool usableOnMove;      //이동 중 사용 가능한 스킬
    public bool delayCancelable;     //딜레이를 캔슬하여 사용할 수 있는 스킬
    public bool isOnHead;             //자기 자신을 중심으로한 범위 스킬
    public Vector2 skillDirection = Vector2.zero;     //스킬 시전 방향 및 넉백 방향
    public Sprite skill_IMG;             //스킬 이미지(UI)
    public Sprite[] effects;              //스킬 이펙트 이미지
    public float frameTime;            //이펙트 이미지 1개당 출력시간
    public bool isLoop;                 //이펙트 루프
    public float lifeTime;               //(루프 사용 시)스킬 지속시간

    private float e_timer;  //이펙트 타이머
    private float l_timer;   //지속시간 타이머
    private int index;       //현재 출력될 이펙트 이미지

    void Awake()
    {
        l_timer = 0f;
        e_timer = frameTime;
        index = 0;

    }

    void Update()
    {
        SkillProduction();
    }


    /* 스킬 이펙트 연출 */
    protected void SkillProduction()
    {
        if (effects == null) { return; }        //이펙트가 없는 경우
        e_timer += Time.deltaTime;
        l_timer += Time.deltaTime;

        if (isLoop)
        {
            //lifeTime이 지나면 스킬 소멸
            if (l_timer >= lifeTime)
            {
                ReleaseSkill();
                Destroy(gameObject);
            }
            //스킬 이펙트를 처음부터 반복
            else if (index >= effects.Length) { index = 0; }
        }

        //frameTime 시간에 한 번씩 이펙트 이미지 변경
        if ((e_timer >= frameTime))
        {
            //Loop 미사용 시 이펙트가 끝나면 스킬 소멸
            if (!isLoop && index >= effects.Length)
            {
                ReleaseSkill();
                Destroy(gameObject);
            }

            if ((effects.Length > 0) && (index < effects.Length)) { GetComponent<SpriteRenderer>().sprite = effects[index]; }
            e_timer = 0;
            index++;
        }
    }


    /* 스킬 범위 내 대상 체크 */
    protected List<HitObject> OnHitCheck()
    {
        List<HitObject> hitObjects = new List<HitObject>();
        Collider2D[] hits = new Collider2D[maxHitCount];
        ContactFilter2D filter = new ContactFilter2D()
        {
            useTriggers = true,     //isTrigger 충돌체도 검사함
            useLayerMask = true,  //레이어 마스크 사용함

            //HitPoint 레이어 외 전부 무시
            layerMask = new LayerMask() { value = (1 << LayerMask.NameToLayer("HitPoint")) }
        };

        //오브젝트가 가진 Collider2D의 충돌검사
        int count = Physics2D.OverlapCollider(GetComponent<Collider2D>(), filter, hits);

        HitObject hitObj;
        for (int i = 0; i < count; i++)
        {
            hitObj = hits[i].GetComponent<HitObject>();

            //시전자와 대상이 같으면 무시
            if (skillCaster.GetComponentInChildren<HitObject>().GetName() == hitObj.GetName()) { continue; }

            hitObjects.Add(hitObj);
        }

        return hitObjects;
    }


    /* 스킬이 시전될 때 동작 */
    public abstract void ActivateSkill();


    /* 스킬이 끝날 때 동작 */
    public abstract void ReleaseSkill();
}
