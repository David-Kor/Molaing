using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameStatus : MonoBehaviour {
    Text HP_Text;
   // Text EXP_Text;
    Image HP_Bar;

    GameObject Player;

    int MAX_HP;
    int Current_HP;
    
    // Use this for initialization
    void Start () {
        HP_Text = transform.GetChild(0).GetChild(2).GetComponent<Text>();   //HP게이지 위에 CurrentHP/MaxHP 형식으로 표시
       // EXP_Text = transform.GetChild(2).GetChild(0).GetComponent<Text>();

        HP_Bar = transform.GetChild(0).GetChild(1).GetComponent<Image>();

        Player = GameObject.Find("Player").gameObject;

        MAX_HP = Player.GetComponentInChildren<PlayerStatus>().maxHP;
        Current_HP = Player.GetComponentInChildren<PlayerStatus>().currentHP;
	}
	
	// Update is called once per frame
	void Update () {
        HP();
	}

    void HP()
    {
        HP_Text.text = Current_HP + "/" + MAX_HP;
        HP_Bar.fillAmount = (Current_HP / MAX_HP);
    }
}
