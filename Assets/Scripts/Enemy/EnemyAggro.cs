using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggro : MonoBehaviour
{
    public float aggroDelay;   //선공을 결정할 초 단위 시간(딜레이)
    public float lostTime;      //어그로 범위를 벗어난 적을 놓치는데 걸리는 시간

    private int autoAggro;   //어그로 수치(공격성) -> 값이 높을 수록 선공할 확률이 오름
    private int maxRandRange;   //랜덤 값의 최대 파라매터 -> 거리가 가까울 수록 낮아짐

    //연산시간 단축을 위해 int형을 사용하여 정확한 거리값 대신 일정 수치로 표현
    private int distX;  //타겟과의 X값 거리
    private int distY;  //타겟과의 Y값 거리

    private EnemyStatus status;
    private EnemyControl control;
    private GameObject target;
    private bool isAggro;

    void Start()
    {
        status = GetComponentInParent<EnemyStatus>();
        control = GetComponentInParent<EnemyControl>();
        target = null;
        autoAggro = status.autoAggro;
        maxRandRange = 1000;
    }


    /* AggroRadius의 Circle Collider안에 플레이어 접 */
    void OnTriggerEnter2D(Collider2D col)
    {
        if (autoAggro <= 0) { return; }

        if (!col.CompareTag("HitPoint")) { return; }

        if (col.transform.parent.name == "Player")
        {
            target = col.transform.parent.gameObject;
            StartCoroutine("RandAggro");
        }
    }


    void OnTriggerExit2D(Collider2D col)
    {
        if (autoAggro <= 0) { return; }

        if (!col.CompareTag("HitPoint")) { return; }

        if (col.transform.parent.name == "Player")
        {
            target = null;
            maxRandRange = 1000;
            StartCoroutine("LostAggroTarget");
            StopCoroutine("RandAggro");
        }
    }


    private IEnumerator LostAggroTarget()
    {
        yield return new WaitForSeconds(lostTime);
        if (target == null && isAggro)
        {
            control.TargetLost();
            isAggro = false;
        }
    }


    private IEnumerator RandAggro()
    {
        while (!isAggro)
        {
            //선제공격 확률은 거리가 가까울수록 높아짐
            //x의 거리 수치
            distX = Mathf.RoundToInt((target.transform.position.x - transform.position.x) * (target.transform.position.x - transform.position.x) * 10);
            //y의 거리 수치
            distY = Mathf.RoundToInt((target.transform.position.y - transform.position.y) * (target.transform.position.y - transform.position.y) * 10);

            //소수점이하 버림
            maxRandRange = Mathf.FloorToInt(1000 - (1000 / ((distX + distY) * (distX + distY) + 1)));

            // 1 ~ maxRandRange 범위의 정수
            //거리가 가까울수록 위의 범위가 줄어듦으로써 확률 증가
            //확률에 당첨되면 컨트롤 클래스에 알려줌
            if (Random.Range(1, maxRandRange) <= autoAggro)
            {
                isAggro = true;
                control.DiscoverTarget(target);
            }
            yield return new WaitForSeconds(aggroDelay);
            //aggroDelay만큼의 시간이 지나면 다시 확률적으로 선제공격 결정
        }
    }
}
