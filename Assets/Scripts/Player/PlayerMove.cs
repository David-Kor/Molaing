using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private PlayerStatus playerState;

    void Start()
    {
        playerState = GetComponent<PlayerStatus>();
    }
    
    public void Move(Vector2 _moveDirect)
    {
        transform.Translate(_moveDirect * playerState.moveSpeed * Time.deltaTime, Space.World);
    }

}
