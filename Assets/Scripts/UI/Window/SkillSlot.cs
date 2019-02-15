using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public Image skillIcon;
    public string skillName;

    // Use this for initialization
    void Start()
    {
        skillIcon = gameObject.transform.GetChild(0).GetComponent<Image>();
    }
    public void RemoveSkillSlot()
    {
        skillIcon = null;
        skillName = null;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    /* 슬롯 이미지 변경함수 */
    public void SetSkillImage(Sprite img)
    {
        if (skillIcon == null)
        {
            skillIcon = gameObject.transform.GetChild(0).GetComponent<Image>();
        }
        skillIcon.sprite = img;
        if (img != null)
        {
            skillIcon.color = Color.white;
        }
        else
        {
            skillIcon.color = Color.clear;
        }
    }
}
