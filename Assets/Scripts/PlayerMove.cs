using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 30.0f;

    void Start()
    {
    }

    void Update()
    {
    }

    public void Move(Vector2 _moveDirect)
    {
        transform.Translate(_moveDirect * speed * Time.deltaTime, Space.World);
    }

}
