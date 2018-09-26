using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 모든 오브젝트의 스탯 클래스
 * 각 NPC(미정), 플레이어, 몬스터로 나뉘어 상속받는다.
 */
public class ObjectStatus : MonoBehaviour
{
    public string objName;

    public int maxHP;       //최대 체력
    public int maxMP;      //최대 마력
    public int currentHP;   //현재 체력
    public int currentMP;  //현재 마력

    public int strength;      //힘
    public int agility;         //민첩
    public int intelligence;  //지력

    public int attackDamage;    //기본 공격력
    public float attackSpeed;    //초당 기본 공격횟수
    public float moveSpeed;     //이동 속도


    void Start()
    {
        currentHP = maxHP;
        currentMP = maxMP;
    }

    void Update()
    {
        if (currentHP <= 0)
        {
            /*
             * 체력을 전부 잃었을 경우의 모든 행동을 이곳에 넣어야 함
             */
            Debug.Log(gameObject.name + " is dead");
            Destroy(gameObject);
        }
    }


    /* 공격을 받으면 데미지 수치만큼 현재 hp 감소 */
    public void TakeDamage(int _dmg) { currentHP -= _dmg;Debug.Log(objName + "  Take Damage " + _dmg); }


    /* 스킬 사용 요구 mp만큼 감소 */
    public void ConsumMP(int _mp) { currentMP -= _mp; }

    
}
