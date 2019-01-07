using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameStatus : MonoBehaviour {
    Text level;
    Text HP_Text;
    Image Exp_bar;
    Image HP_Bar;

    PlayerStatus Player;

    float MAX_HP;
    float Current_HP;
    
    // Use this for initialization
    void Start () {
        level = transform.GetChild(2).GetChild(0).GetComponent<Text>();
        HP_Text = transform.GetChild(0).GetChild(2).GetComponent<Text>();   //HP게이지 위에 CurrentHP/MaxHP 형식으로 표시

        HP_Bar = transform.GetChild(0).GetChild(1).GetComponent<Image>();
        Exp_bar = transform.GetChild(1).GetComponent<Image>();
        Player = GameObject.Find("Player").gameObject.GetComponentInChildren<PlayerStatus>();
	}
	
	// Update is called once per frame
	void Update () {
        Level();
        HP();
        EXP();
    }

    void Level()
    {
        if(level.text != Player.level.ToString()) { level.text = Player.level.ToString(); }
    }
    void HP()
    {
        HP_Text.text = Player.currentHP + "/" + Player.maxHP;
        HP_Bar.fillAmount = (float)(Player.currentHP/Player.maxHP);
        Debug.Log(HP_Bar.fillAmount);
    }

    void EXP()
    {
        Exp_bar.fillAmount = gameObject.transform.parent.parent.GetComponentInChildren<UI_Controller>().Exp(Player.currentEXP, Player.requireEXP);
    }
}
