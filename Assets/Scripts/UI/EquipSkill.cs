using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSkill : MonoBehaviour
{
    private GameObject selected_skillObj;
    private SkillSlotControl s_slotControl;

    private void Start()
    {
        s_slotControl = FindObjectOfType<SkillSlotControl>();
    }

    public void SelectedSkill(GameObject skillObj)
    {
        selected_skillObj = skillObj;
    }

    /* Button Down Methods */
    public void Q_Button_Down()
    {
        s_slotControl.UpdateSlot(0, selected_skillObj);
        transform.parent.gameObject.SetActive(false);
    }
    public void W_Button_Down()
    {
        s_slotControl.UpdateSlot(1, selected_skillObj);
        transform.parent.gameObject.SetActive(false);
    }
    public void E_Button_Down()
    {
        s_slotControl.UpdateSlot(2, selected_skillObj);
        transform.parent.gameObject.SetActive(false);
    }
    public void R_Button_Down()
    {
        s_slotControl.UpdateSlot(3, selected_skillObj);
        transform.parent.gameObject.SetActive(false);
    }
    public void T_Button_Down()
    {
        s_slotControl.UpdateSlot(4, selected_skillObj);
        transform.parent.gameObject.SetActive(false);
    }
}
