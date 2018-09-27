using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private EnemyStatus status;
    private GameObject targetObject;
    private Vector2 targetPosition;


    void Start()
    {
        status = GetComponent<EnemyStatus>();
        targetObject = null;
        targetPosition = transform.position;
    }


    void Update()
    {
        if (targetObject != null)
        {
            transform.Translate((targetObject.transform.position - transform.position).normalized * status.moveSpeed * Time.deltaTime);
        }
    }


    public void MoveToThisObject(GameObject _target) { targetObject = _target; }


    public void MoveToThisPosition(Vector2 _pos) { targetPosition = _pos; }

}
