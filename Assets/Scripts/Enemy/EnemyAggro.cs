using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggro : MonoBehaviour
{
    public float aggroDelay;   //선공을 결정할 초 단위 시간(딜레이)

    private float aggroTimer;   //딜레이까지의 시간을 재는 타이머
    private int autoAggro;   //어그로 수치(공격성) -> 값이 높을 수록 선공할 확률이 오름
    private int maxRandRange;   //랜덤 값의 최대 파라매터 -> 거리가 가까울 수록 낮아짐

    //정확한 거리를 계산면 소수를 계산하며, sqrt연산까지 하면 보통 무한소수가 나옴
    //연산시간 단축을 위해 int형을 사용하여 정확한 거리값 대신 일정 수치로 표현
    private int distX;  //타겟과의 X값 거리
    private int distY;  //타겟과의 Y값 거리

    private EnemyStatus status;
    private EnemyMove enemyMove;
    private GameObject target;


    void Start()
    {
        status = GetComponentInParent<EnemyStatus>();
        enemyMove = GetComponentInParent<EnemyMove>();
        target = null;
        autoAggro = status.autoAggro;
        aggroTimer = 0f;
        maxRandRange = 1000;
    }


    void Update()
    {
        if (target != null)
        {
            aggroTimer += Time.deltaTime;
            //x의 거리 수치
            distX = Mathf.RoundToInt((target.transform.position.x - transform.position.x) * (target.transform.position.x - transform.position.x) * 10);
            //y의 거리 수치
            distY = Mathf.RoundToInt((target.transform.position.y - transform.position.y) * (target.transform.position.y - transform.position.y) * 10);

            //소수점이하 버림
            maxRandRange = Mathf.FloorToInt(1000 - (1000 / ((distX + distY) * (distX + distY) + 1)));

            if (aggroTimer >= aggroDelay)
            {
                aggroTimer = 0;
                
                if (Random.Range(1, maxRandRange) <= autoAggro)
                {
                    enemyMove.MoveToThisObject(target);
                }
            }
        }
        else
        {
            maxRandRange = 1000;
            enemyMove.MoveToThisObject(target);
        }
    }


    /* AggroRadius의 Circle Collider안에 플레이어 접 */
    void OnTriggerEnter2D(Collider2D col)
    {
        if (autoAggro <= 0) { return; }

        if (col.transform.parent.name == "Player")
        {
            aggroTimer = 0;
            target = col.transform.parent.gameObject;
        }
    }


    void OnTriggerExit2D(Collider2D col)
    {
        if (autoAggro <= 0) { return; }

        if (col.transform.parent.name == "Player")
        {
            aggroTimer = 0;
            target = null;
        }
    }
}
