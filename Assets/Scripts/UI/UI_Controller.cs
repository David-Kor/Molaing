﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour {
    GameObject Player;
    GameObject Inventory;
    GameObject InGameStatus;

    Text LV;
    Text currentExp;
    Text requireExp;
    Text HitPoint;
    Text Strength;
    Text Agility;
    Text Intelligent;
    public Text Point;
    public int point = 0;

    public bool bInventory;
    public bool bMouse0Down;
	// Use this for initialization
	void Awake()
    {
        bInventory = false;
        bMouse0Down = false;

        Player = GameObject.Find("Player").gameObject;

        Inventory = transform.GetChild(0).GetChild(1).gameObject; //Main Camera/Canvas/Inventory
        InGameStatus = transform.GetChild(0).GetChild(0).gameObject;

        LV = Inventory.transform.GetChild(0).GetChild(1).GetChild(2).GetComponent<Text>();  //Inventory/Left/Bottom/LV
        HitPoint = Inventory.transform.GetChild(0).GetChild(1).GetChild(4).GetComponent<Text>();  //Inventory/Left/Bottom/HP
        Strength = Inventory.transform.GetChild(0).GetChild(1).GetChild(6).GetComponent<Text>(); //Inventory/Left/Bottom/STR
        Agility = Inventory.transform.GetChild(0).GetChild(1).GetChild(8).GetComponent<Text>(); //Inventory/Left/Bottom/AGI
        Intelligent = Inventory.transform.GetChild(0).GetChild(1).GetChild(10).GetComponent<Text>(); //Inventory/Left/Bottom/INT
        Point = Inventory.transform.GetChild(0).GetChild(1).GetChild(12).GetComponent<Text>();  //Inventory/Left/Bottom/Point

        bInventory = false;
	}
  
    public void Exp(float currentExp, float requireExp, int level)
    {
        InGameStatus.GetComponent<InGameStatus>().ExpBar(currentExp, requireExp);
        InGameStatus.GetComponent<InGameStatus>().level.text = level.ToString();
    }

    public void HP(int currentHP, int maxHP)
    {
        InGameStatus.GetComponent<InGameStatus>().HP_Bar(currentHP,maxHP);
        InGameStatus.GetComponent<InGameStatus>().hpText.text = currentHP + "/" + maxHP;
    }

    public void Control_Inventory(bool i)
    {
        if (i == true)
        {
            bInventory = false;
            Inventory.SetActive(true);
        }
        else{
            bInventory = false;
            Inventory.SetActive(false);
        }

        LV.text = Player.GetComponentInChildren<PlayerStatus>().level.ToString();
        HitPoint.text = Player.GetComponentInChildren<PlayerStatus>().currentHP.ToString() + "/" + Player.GetComponentInChildren<PlayerStatus>().maxHP.ToString();
        Strength.text = Player.GetComponentInChildren<PlayerStatus>().strength.ToString();
        Agility.text = Player.GetComponentInChildren<PlayerStatus>().agility.ToString();
        Intelligent.text = Player.GetComponentInChildren<PlayerStatus>().intelligence.ToString();
        Point.text = Player.GetComponentInChildren<PlayerStatus>().statusPoint.ToString();
    }
}
