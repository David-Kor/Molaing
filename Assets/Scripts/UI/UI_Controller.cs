using System.Collections;
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

        STR.text = Player.GetComponentInChildren<PlayerStatus>().strength.ToString();
        AGI.text = Player.GetComponentInChildren<PlayerStatus>().agility.ToString();
        INT.text = Player.GetComponentInChildren<PlayerStatus>().intelligence.ToString();

    }

    public void Exp(float a, float b, int level) { }
}
