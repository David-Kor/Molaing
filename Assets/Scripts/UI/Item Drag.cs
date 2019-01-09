using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDrag : MonoBehaviour {

    public Transform Img;

    private Image EmptyImg;
    private Inventory_Slot slot;

	void Start () {
        slot = GetComponent<Inventory_Slot>();
        Img = GameObject.FindGameObjectWithTag("DragImg").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
