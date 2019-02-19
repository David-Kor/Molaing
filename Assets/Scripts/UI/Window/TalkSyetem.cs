using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkSyetem : MonoBehaviour
{
    List<string> talking = new List<string>();
    int strCnt = 0;         // 이 변수가 하나씩 커져가면서 대화를 진행합니다.
    Text talkingTxt;        // Text 오브젝트에 접근하기

    void Start()
    {
        talkingTxt = transform.GetChild(0).GetChild(1).GetComponent<Text>();
        talkingTxt.text = "";
        initialized();

        StartCoroutine(CountDialog());        
    }

    IEnumerator CountDialog()
    {
        yield return StartCoroutine(ShowAll(talking[strCnt]));
        strCnt++;

        while (strCnt < talking.Count)
        {
            yield return new WaitForSeconds(0.01f);
            if(Input.GetKeyDown(KeyCode.Z))
            {
                yield return StartCoroutine(ShowAll(talking[strCnt]));
                strCnt++;
            }
        }

        gameObject.SetActive(false);
        StopAllCoroutines();
    }
    IEnumerator ShowAll(string a)
    {
        if (!talkingTxt.text.Equals(a))
        {
            for (int i = 0; i < a.Length; i++)
            {
                talkingTxt.text += a[i];
                yield return new WaitForSeconds(0.02f);
            }
            StopCoroutine("ShowAll");
        }

    }
    void initialized()
    {
        talking.Add("힝");
        talking.Add("히이이이이이이이이잉");
        talking.Add("");
    }
}
