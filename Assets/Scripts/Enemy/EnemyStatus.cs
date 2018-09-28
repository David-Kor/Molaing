using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : ObjectStatus
{
    public float attackRange;
    public int autoAggro;   //어그로 수치(공격성) -> 값이 높을 수록 선공할 확률이 오름 (최댓값 : 1000)
}
