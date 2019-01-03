using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public GameObject[] skill_List;     //스킬 프리팹 목록

    private float skillDelay;              //딜레이 타이머
    private float[] coolTimerList;       //스킬별 쿨타임 타이머

    private PlayerControl playerControl;
    private int i;

    void Start()
    {
        playerControl = GetComponentInParent<PlayerControl>();
        coolTimerList = new float[skill_List.Length];
        skillDelay = 0f;

        for (i = 0; i < coolTimerList.Length; i++)
        {
            coolTimerList[i] = 0f;
        }
    }

    void Update()
    {
        if (skillDelay > 0)
        {
            skillDelay -= Time.deltaTime;
        }
        else if (playerControl.GetIsDelay()) { playerControl.SetIsDelay(false); }

        for (i = 0; i < coolTimerList.Length; i++)
        {
            if (coolTimerList[i] > 0)
            {
                coolTimerList[i] -= Time.deltaTime;
            }
        }
    }

    public void UseSkill(int index, Vector2 direct)
    {
        //딜레이가 남아있으면 스킬을 사용할 수 없음
        if (skillDelay > 0) { return; }
        //스킬이 없으면 사용 불가
        if (skill_List[index] == null) { return; }
        //해당 스킬의 쿨타임이 남아있으면 사용 불가
        if (coolTimerList[index] > 0) { return; }

        GameObject skillObj;
        if (direct == Vector2.up)
        {
            skillObj = Instantiate(skill_List[index], transform.GetChild(0));
            skillObj.transform.Rotate(0, 0, 180);
        }
        else if (direct == Vector2.down)
        {
            skillObj = Instantiate(skill_List[index], transform.GetChild(1));
            skillObj.transform.Rotate(0, 0, 0);
        }
        else if (direct == Vector2.left)
        {
            skillObj = Instantiate(skill_List[index], transform.GetChild(2));
            skillObj.transform.Rotate(0, 0, -90);
        }
        else
        {
            skillObj = Instantiate(skill_List[index], transform.GetChild(3));
            skillObj.transform.Rotate(0, 0, 90);
        }

        skillObj.transform.position = transform.position;
        Skill skill = skillObj.GetComponent<Skill>();
        skill.skillCaster = playerControl.gameObject;
        skill.skillDirect = direct;
        skill.ActivateSkill();
        coolTimerList[index] = skill.coolDown;
        skillDelay = skill.delay;
        playerControl.SetIsDelay(true);
    }
}
