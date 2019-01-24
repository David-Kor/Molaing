using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameStatus : MonoBehaviour {
    public Text level;
    public Text hpText;
    public Image expBar;
    public Image hpBar;
    PlayerStatus Player;

    UI_Controller uI_Controller;
    float MAX_HP;
    float Current_HP;
    
    // Use this for initialization
    void Start () {
        level = transform.GetChild(2).GetChild(0).GetComponent<Text>();
        hpText = transform.GetChild(0).GetChild(2).GetComponent<Text>();   //HP게이지 위에 CurrentHP/MaxHP 형식으로 표시
        hpBar = transform.GetChild(0).GetChild(1).GetComponent<Image>();
        expBar = transform.GetChild(1).GetComponent<Image>();
        Player = GameObject.Find("Player").gameObject.GetComponentInChildren<PlayerStatus>();
        level.text = Player.level.ToString();
        hpText.text = Player.currentHP + "/" + Player.maxHP;
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void ExpBar(float currentExp, float requireExp)
    {
        expBar.fillAmount = currentExp / requireExp;
    }

    public void HP_Bar(int currentHP, int requireHP)
    {
        hpBar.fillAmount = ((float)currentHP / (float)requireHP);
    }
}
