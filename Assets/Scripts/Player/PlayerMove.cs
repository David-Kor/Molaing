using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private PlayerState playerState;

    void Start()
    {
        playerState = GetComponent<PlayerState>();
    }
    
    public void Move(Vector2 _moveDirect)
    {
        transform.Translate(_moveDirect * playerState.moveSpeed * Time.deltaTime, Space.World);
    }

}
