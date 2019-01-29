using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject[] skill_list;

    public float[] coolTimerList;
    private EnemyControl control;
    private Tackle tackle;
    private EnemyStatus status;

    void Start()
    {
        control = GetComponentInParent<EnemyControl>();
        status = GetComponentInParent<EnemyStatus>();
        tackle = gameObject.AddComponent<Tackle>();
        tackle.isKnockBack = true;
        tackle.knockBackPower = status.knockBackPower;
        tackle.skillCaster = transform.parent.gameObject;
        tackle.damage = status.attackDamage;
        coolTimerList = new float[skill_list.Length];
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (!col.CompareTag("HitPoint")) { return; }

        if (col.transform.parent.CompareTag("Player"))
        {
            tackle.skillDirection = control.GetLookDirection();
            col.GetComponent<HitObject>().OnHitSkill(tackle);
        }
    }


    private IEnumerator CoolDownTimer(int _index)
    {
        while (coolTimerList[_index] > 0)
        {
            yield return null;
            coolTimerList[_index] -= Time.deltaTime;
        }
        coolTimerList[_index] = 0;
    }


    private IEnumerator SkillDelay(float _delay)
    {
        control.SetIsDelay(true);
        yield return new WaitForSeconds(_delay);
        control.SetIsDelay(false);
    }


    public void UseSkill(int _index, Vector2 _direction)
    {
        //Out Of Index
        if (_index < 0 || skill_list.Length <= _index) { return; }
        //Null Reference
        if (skill_list[_index] == null) { return; }
        //쿨타임이 남있는 경우
        if (coolTimerList[_index] > 0) { return; }

        GameObject skillObj;
        if (_direction == Vector2.left)
        {
            skillObj = Instantiate(skill_list[_index], transform.GetChild(0));
            skillObj.transform.Rotate(0, 0, -90);
        }
        else
        {
            skillObj = Instantiate(skill_list[_index], transform.GetChild(1));
            skillObj.transform.Rotate(0, 0, 90);
        }

        Skill skill = skillObj.GetComponent<Skill>();
        skill.skillCaster = control.gameObject;
        skill.SetSkillIndex(_index);
        skill.skillDirection = _direction;
        if (skill.isOnHead) { skillObj.transform.position = transform.position; }   //스킬 범위가 자신의 위치가 중심인 경우
        StartCoroutine("SkillDelay", skill.delay);
        skill.ActivateSkill();
    }


    public Skill GetSkill(int _index)
    {
        //Out Of Index
        if (_index < 0 || skill_list.Length <= _index) { return null; }
        //Null Reference
        if (skill_list[_index] == null) { return null; }

        return skill_list[_index].GetComponent<Skill>();
    }


    /* 쿨타임 활성화 */
    public void CoolDownTimerActive(int _index, float _value)
    {
        //Out Of Index
        if (_index < 0 || coolTimerList.Length <= _index) { return; }

        float preTime = coolTimerList[_index];
        coolTimerList[_index] = _value;
        if (preTime <= 0)
        {
            StartCoroutine("CoolDownTimer", _index);
        }
    }
}
