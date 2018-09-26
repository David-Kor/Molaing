﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOrder : MonoBehaviour
{
    private Vector2 prevPosition;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        prevPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y != prevPosition.y)
        {
            spriteRenderer.sortingOrder = -1 * Mathf.RoundToInt(transform.position.y * 50);
            prevPosition = transform.position;
        }
    }
}
