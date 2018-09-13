using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Sprite frontSprites;
    public Sprite backSprites;
    public Sprite rightSprites;

    private PlayerMove playerMove;
    private SpriteRenderer playerSprite;
    private Vector2 spriteDirect;

    void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
        playerSprite = GetComponent<SpriteRenderer>();
        spriteDirect = Vector2.down;
    }

    void Update()
    {
        if (spriteDirect != playerMove.spriteDirect)
        {
            spriteDirect = playerMove.spriteDirect;
            transform.rotation = Quaternion.Euler(0, 0, 0);

            if (spriteDirect == Vector2.down) { playerSprite.sprite = frontSprites; }
            else if (spriteDirect == Vector2.up) { playerSprite.sprite = backSprites; }
            else if (spriteDirect == Vector2.right) { playerSprite.sprite = rightSprites; }
            else if (spriteDirect == Vector2.left)
            {
                playerSprite.sprite = rightSprites;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
}
