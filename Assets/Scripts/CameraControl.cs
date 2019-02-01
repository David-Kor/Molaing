using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject player;
    public float cam_speed;

	void Start () {
	}


    void Update()
    {
        if (player != null)
        {
            transform.Translate((Vector2)(player.transform.position - transform.position) * cam_speed * Time.deltaTime, Space.World);
        }
    }
}
