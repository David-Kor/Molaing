using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameStatus : MonoBehaviour {
    Text level;
    Text HP_Text;
    Image Exp_bar;
    Image HP_Bar;
    UI_Controller uI_Controller;
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
        uI_Controller = gameObject.transform.parent.parent.GetComponentInChildren<UI_Controller>();
    }
	
	// Update is called once per frame
	void Update () {
        Level();
        HP();
        EXP();
    }

    void Level()
    {
        if(level.text != Player.level.ToString())
        {
            level.text = Player.level.ToString();
            if (int.Parse(level.text) > 1)
            {
                uI_Controller.point += 5;       //레벨이 오르면 UI_Controller.cs의 Point를 5 증가시킴.
                                                //레벨마다 오르는 Point는 여기서 설정.
                uI_Controller.Point.text = uI_Controller.point.ToString();
            }
        }
    }
    void HP()
    {
        HP_Text.text = Player.currentHP + "/" + Player.maxHP;
        HP_Bar.fillAmount = (float)Player.currentHP / (float)Player.maxHP;
    }

    void EXP()
    {
        Exp_bar.fillAmount = gameObject.transform.parent.parent.GetComponentInChildren<UI_Controller>().Exp(Player.currentEXP, Player.requireEXP);
    }
}
