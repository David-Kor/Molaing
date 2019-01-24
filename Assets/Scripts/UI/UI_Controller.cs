using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    GameObject Player;
    GameObject Inventory;
    GameObject InGameStatus;
    Inventory_Slot[] iSlot;
    QuickSlot[] qSlot;

    Button strengthButton;
    Button agilityButton;
    Button intelligenceButton;

    Text LV;
    Text currentExp;
    Text requireExp;
    Text HitPoint;
    Text Strength;
    Text Agility;
    Text Intelligence;
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
        iSlot = new Inventory_Slot[49];
        qSlot = new QuickSlot[4];

        LV = Inventory.transform.GetChild(0).GetChild(1).GetChild(2).GetComponent<Text>();  //Inventory/Left/Bottom/LV
        HitPoint = Inventory.transform.GetChild(0).GetChild(1).GetChild(4).GetComponent<Text>();  //Inventory/Left/Bottom/HP
        Strength = Inventory.transform.GetChild(0).GetChild(1).GetChild(6).GetComponent<Text>(); //Inventory/Left/Bottom/STR
        Agility = Inventory.transform.GetChild(0).GetChild(1).GetChild(9).GetComponent<Text>(); //Inventory/Left/Bottom/AGI
        Intelligence = Inventory.transform.GetChild(0).GetChild(1).GetChild(12).GetComponent<Text>(); //Inventory/Left/Bottom/INT
        Point = Inventory.transform.GetChild(0).GetChild(1).GetChild(15).GetComponent<Text>();  //Inventory/Left/Bottom/Point

        strengthButton = Inventory.transform.GetChild(0).GetChild(1).GetChild(7).GetComponent<Button>();
        agilityButton = Inventory.transform.GetChild(0).GetChild(1).GetChild(10).GetComponent<Button>();
        intelligenceButton = Inventory.transform.GetChild(0).GetChild(1).GetChild(13).GetComponent<Button>();

        for(int i = 0; i < 49; i++)
        {
            iSlot[i] = Inventory.transform.GetChild(1).GetChild(0).GetChild(i).GetComponent<Inventory_Slot>();
        }
        for(int i = 0; i< 4; i++)
        {
            qSlot[i] = Inventory.transform.parent.GetChild(2).GetChild(0).GetChild(i).GetComponent<QuickSlot>();
        }

        bInventory = false;
    }

    public void Exp(float currentExp, float requireExp, int level)
    {
        InGameStatus.GetComponent<InGameStatus>().ExpBar(currentExp, requireExp);
        InGameStatus.GetComponent<InGameStatus>().level.text = level.ToString();
    }

    public void HP(int currentHP, int maxHP)
    {
        InGameStatus.GetComponent<InGameStatus>().HP_Bar(currentHP, maxHP);
        InGameStatus.GetComponent<InGameStatus>().hpText.text = currentHP + "/" + maxHP;
    }

    public void Control_Inventory(bool i)
    {
        if (i == true)
        {
            bInventory = false;
            Inventory.SetActive(true);
        }
        else
        {
            bInventory = false;
            Inventory.SetActive(false);
        }
        LV.text = Player.GetComponentInChildren<PlayerStatus>().level.ToString();
        HitPoint.text = Player.GetComponentInChildren<PlayerStatus>().currentHP.ToString() + "/" + Player.GetComponentInChildren<PlayerStatus>().maxHP.ToString();
        Strength.text = Player.GetComponentInChildren<PlayerStatus>().GetSTR().ToString();
        Agility.text = Player.GetComponentInChildren<PlayerStatus>().GetAGI().ToString();
        Intelligence.text = Player.GetComponentInChildren<PlayerStatus>().GetINT().ToString();
        point = Player.GetComponentInChildren<PlayerStatus>().statusPoint;
        Point.text = point.ToString();
        if (point > 0)
        {
            ButtonActivation();
            Debug.Log("활성화");
        }
    }
    void ButtonDeactivation()
    {
        strengthButton.gameObject.SetActive(false);
        agilityButton.gameObject.SetActive(false);
        intelligenceButton.gameObject.SetActive(false);
    }

    void ButtonActivation()
    {
        strengthButton.gameObject.SetActive(true);
        agilityButton.gameObject.SetActive(true);
        intelligenceButton.gameObject.SetActive(true);
    }

    public void StrengthUP()
    {
        Player.GetComponentInChildren<PlayerStatus>().STR_Up(1);
        point--;
        Debug.Log(Player.GetComponent<PlayerStatus>().strength);
        if (point <= 0)
        {
            ButtonDeactivation();
        }
    }

    public void AgilityUP()
    {
        Player.GetComponentInChildren<PlayerStatus>().AGI_Up(1);
        point--;
        if (point <= 0)
        {
            ButtonDeactivation();
        }
    }

    public void IntelligenceUP()
    {
        Player.GetComponentInChildren<PlayerStatus>().INT_Up(1);
        point--;
        if (point <= 0)
        {
            ButtonDeactivation();
        }
    }
}