using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffterImage : MonoBehaviour
{
    public Sprite[] imgs;
    public float frameTime;

    private float timer;
    private int index;

    void Start()
    {
        timer = 0;
        index = 0;
        GetComponent<SpriteRenderer>().sprite = imgs[index++];
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= frameTime)
        {
            if (index >= imgs.Length)
            {
                Destroy(gameObject);
                return;
            }
            GetComponent<SpriteRenderer>().sprite = imgs[index++];
        }
    }
}
