using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameStatus : MonoBehaviour {
    public Text level;
    Text hpText;
    public Image expBar;
    Image hpBar;
    UI_Controller uI_Controller;
    PlayerStatus Player;

    float MAX_HP;
    float Current_HP;
    
    // Use this for initialization
    void Start () {
        level = transform.GetChild(2).GetChild(0).GetComponent<Text>();
        hpText = transform.GetChild(0).GetChild(2).GetComponent<Text>();   //HP게이지 위에 CurrentHP/MaxHP 형식으로 표시

        hpBar = transform.GetChild(0).GetChild(1).GetComponent<Image>();
        expBar = transform.GetChild(1).GetComponent<Image>();

        Player = GameObject.Find("Player").gameObject.GetComponentInChildren<PlayerStatus>();
        uI_Controller = gameObject.transform.parent.parent.GetComponentInChildren<UI_Controller>();
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
