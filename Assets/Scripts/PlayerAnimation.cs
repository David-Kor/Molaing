using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Sprite frontSprites;
    public Sprite backSprites;
    public Sprite rightSprites;

    private PlayerControl playerControl;
    private SpriteRenderer playerSprite;    //플레이어의 스프라이트(이미지)
    private Vector2 spriteDirect;   //현재 바라보는 방향

    void Start()
    {
        playerControl = GetComponentInParent<PlayerControl>();
        playerSprite = GetComponent<SpriteRenderer>();
        spriteDirect = Vector2.down;
    }

    void Update()
    {
        //플레이어의 입력에 의해 이전과 다른 방향을 바라봐야 하는 경우
        if (spriteDirect != playerControl.spriteDirect)
        {
            spriteDirect = playerControl.spriteDirect;  //바뀐 방향으로 갱신
            transform.rotation = Quaternion.Euler(0, 0, 0);

            //각각 아래, 위, 오른쪽, 왼쪽에 해당하는 스프라이트를 적용
            if (spriteDirect == Vector2.down) { playerSprite.sprite = frontSprites; }
            else if (spriteDirect == Vector2.up) { playerSprite.sprite = backSprites; }
            else if (spriteDirect == Vector2.right) { playerSprite.sprite = rightSprites; }
            else if (spriteDirect == Vector2.left)
            {
                playerSprite.sprite = rightSprites;
                transform.rotation = Quaternion.Euler(0, 180, 0);   //스프라이트가 오른쪽 방향을 보는 이미지이므로 세로축으로 180도 회전하여 왼쪽을 보게 함
            }
        }
    }
}
