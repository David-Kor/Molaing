using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggro : MonoBehaviour
{
    public bool isKOS;  //Killed On Sight -> 선공 여부 결정

    private EnemyMove enemyMove;
    private GameObject curTarget;
    private GameObject prevTarget;

    void Start()
    {
        enemyMove = GetComponentInParent<EnemyMove>();
        curTarget = null;
        prevTarget = null;
    }


    void Update()
    {
        if (prevTarget != curTarget)
        {
            prevTarget = curTarget;
            enemyMove.MoveToThisObject(curTarget);
        }
    }


    /* AggroRadius의 Circle Collider안에 플레이어 접 */
    void OnTriggerEnter2D(Collider2D col)
    {
        if (!isKOS) { return; }

        if (col.transform.parent.name == "Player")
        {
            curTarget = col.transform.parent.gameObject;
        }
    }


    void OnTriggerExit2D(Collider2D col)
    {
        if (!isKOS) { return; }

        if (col.transform.parent.name == "Player")
        {
            curTarget = null;
        }
    }
}
