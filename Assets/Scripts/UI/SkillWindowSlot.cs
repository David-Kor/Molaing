using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillWindowSlot : MonoBehaviour {

    public Text skillName;
    public Text skillDes;
    public Image skill_Icon;
    public GameObject content;
    
	public void Start () {
        skillName = gameObject.transform.GetChild(2).GetComponent<Text>();
        skillDes = gameObject.transform.GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
        skill_Icon = gameObject.transform.GetChild(1).GetComponent<Image>();
        content = skillDes.transform.parent.gameObject;
        SetContentSize();
	}
    public void SetContentSize()
    {
        float width = 281.1f;
        float height = skillDes.transform.gameObject.GetComponent<RectTransform>().rect.height;

        RectTransform rt = (RectTransform)content.transform;

        rt.sizeDelta = new Vector2(width, height);
    }
}
