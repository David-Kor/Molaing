﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject player;

	void Start () {
	}


    void Update()
    {
        if (player != null)
        {
            transform.position = player.transform.position + (Vector3.back * 10);
        }
    }
}
