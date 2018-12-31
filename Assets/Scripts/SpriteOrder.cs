using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOrder : MonoBehaviour
{
    private Vector2 prevPosition;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        prevPosition = transform.parent.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = -1 * Mathf.RoundToInt(transform.parent.position.y * 50);
    }


    void Update()
    {
        if (transform.parent.position.y != prevPosition.y)
        {
            spriteRenderer.sortingOrder = -1 * Mathf.RoundToInt(transform.parent.position.y * 50);
            prevPosition = transform.parent.position;
        }
    }
}
