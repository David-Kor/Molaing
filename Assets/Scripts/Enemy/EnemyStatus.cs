﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : ObjectStatus
{
    public int tackleDamage;    //접촉 시 데미지(태클)
    public int autoAggro;   //어그로 수치(공격성) -> 값이 높을 수록 선공할 확률이 오름 (최댓값 : 1000)
    public float gainableEXP;

    public override void TakeDamage(int _dmg)
    {
        currentHP -= _dmg;
    }
}
