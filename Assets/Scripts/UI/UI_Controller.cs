﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour {
    GameObject Player;
    GameObject Inventory;

 //   Text LV;
 //   Text HP;
    Text STR;
    Text AGI;
    Text INT;
 //   Text Point;

    bool bInventory;
	// Use this for initialization
	void Start ()
    {
        Player = GameObject.Find("Player").gameObject;

        Inventory = transform.GetChild(0).GetChild(1).gameObject; //Main Camera/Canvas/Inventory

//        LV = Inventory.transform.GetChild(0).GetChild(1).GetChild(2).GetComponent<Text>();  //Inventory/Left/Bottom/LV
//        HP = Inventory.transform.GetChild(0).GetChild(1).GetChild(4).GetComponent<Text>();  //Inventory/Left/Bottom/HP
        STR = Inventory.transform.GetChild(0).GetChild(1).GetChild(6).GetComponent<Text>(); //Inventory/Left/Bottom/STR
        AGI = Inventory.transform.GetChild(0).GetChild(1).GetChild(8).GetComponent<Text>(); //Inventory/Left/Bottom/AGI
        INT = Inventory.transform.GetChild(0).GetChild(1).GetChild(10).GetComponent<Text>(); //Inventory/Left/Bottom/INT
//        Point = Inventory.transform.GetChild(0).GetChild(1).GetChild(12).GetComponent<Text>();  //Inventory/Left/Bottom/Point

        bInventory = false;
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) == true)
        {
            Control_Inventory();
        }
        else
        {
            bInventory = true;  //키가 눌렸을 때 반복되는 것을 막기 위한 bool 변수
        }
    }

    void Control_Inventory()
    {
        if (Inventory.activeSelf == false && bInventory == true)
        {
            bInventory = false;
            Inventory.SetActive(true);
        }
        if (Inventory.activeSelf == true && bInventory == true)
        {
            bInventory = false;
            Inventory.SetActive(false);
        }

        STR.text = Player.GetComponentInChildren<PlayerStatus>().strength.ToString();
        AGI.text = Player.GetComponentInChildren<PlayerStatus>().agility.ToString();
        INT.text = Player.GetComponentInChildren<PlayerStatus>().intelligence.ToString();

    }
}
