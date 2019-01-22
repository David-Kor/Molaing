using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundCheck : MonoBehaviour
{
    private ObjectControl control;

    void Start()
    {
        control = GetComponentInParent<ObjectControl>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ground") || col.CompareTag("Earth"))
        {
            control.OnGround(col.tag);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Ground") || col.CompareTag("Earth"))
        {
            control.ExitGround();
        }
    }
}
