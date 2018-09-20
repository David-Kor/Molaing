using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public int maxHP;
    public int maxMP;
    public int currentHP;
    public int currentMP;

    public int strength;    //힘
    public int agility;     //민첩
    public int intelligence;    //지력

    public int attackPoint;
    public float attackSpeed;   //초당 기본 공격횟수
    public float moveSpeed;

}
