using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartOfEarthquake : MonoBehaviour
{
    public float highestValue;
    public float speed;

    void Start()
    {
        transform.SetParent(null);
    }

    void Update()
    {
        transform.localScale += Vector3.down * speed * Time.deltaTime;
        transform.localPosition += Vector3.down * speed * 0.19f * Time.deltaTime;
        if (transform.localScale.y <= 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
