using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillWindowSlot : MonoBehaviour
{

    RaycastHit hit;
    GameObject target = null;
    LayerMask layerMask;
    GameObject mainCamera;
    Image image;
    bool enterSkillWindow;
    public Text skillName;
    public Text skillDes;
    public Image skill_Icon;
    public GameObject content;

    public void Start()
    {
        skillName = gameObject.transform.GetChild(2).GetComponent<Text>();
        skillDes = gameObject.transform.GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
        skill_Icon = gameObject.transform.GetChild(1).GetComponent<Image>();
        content = skillDes.transform.parent.gameObject;
        mainCamera = Camera.main.gameObject;
        image = gameObject.transform.GetChild(2).GetComponent<Image>();
        SetContentSize();
    }
    public void SetContentSize()
    {
        float width = 281.1f;
        float height = skillDes.transform.gameObject.GetComponent<RectTransform>().rect.height;

        RectTransform rt = (RectTransform)content.transform;

        rt.sizeDelta = new Vector2(width, height);
    }

    public void CastRay()
    {
        target = null;

        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        layerMask = 1 << 5;
        Ray2D ray = new Ray2D(pos, Vector2.zero);

        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, 1000, layerMask);
        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.CompareTag("SkillWindow")
                || hit.collider.gameObject.CompareTag("SkillSlot"))
            {
                target = hit.collider.gameObject;
            }

        }
    }
    private void OnGUI()
    {
        if (mainCamera.transform.GetChild(0).GetChild(5).gameObject.activeSelf == true && mainCamera.GetComponent<UI_Controller>().bMouse0Down == true)
            {
                GUI.DrawTexture(new Rect(Input.mousePosition.x, Event.current.mousePosition.y, 45, 45), skill_Icon.mainTexture);
            }
    }
    public void OnMouseDown()
    {
        CastRay();
        Debug.Log(target);
        if (enterSkillWindow == true)
        {
            if (target.GetComponent<SkillWindowSlot>().skillName.text == null)
            {
                return;
            }
            else
            {
                mainCamera.GetComponent<UI_Controller>().bMouse0Down = true;
            }
        }
    }
    public void OnMouseUp()
    {
        CastRay();
        if (target == null)
        {
            mainCamera.GetComponent<UI_Controller>().bMouse0Down = false;
            return;
        }
        if (target.CompareTag("SkillSlot"))
        {
            target.GetComponent<SkillSlot>().skillIcon = this.skill_Icon;
            target.GetComponent<SkillSlot>().skillName = this.skillName.text;
            target.transform.GetChild(0).gameObject.SetActive(true);
            mainCamera.GetComponent<UI_Controller>().bMouse0Down = false;
            return;
        }
        else
        {
            mainCamera.GetComponent<UI_Controller>().bMouse0Down = false;
            return;
        }
    }
    public void OnMouseEnter()
    {
        enterSkillWindow = true;
    }
    public void OnMouseExit()
    {
        enterSkillWindow = false;
    }
}
