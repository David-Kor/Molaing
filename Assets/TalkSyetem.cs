using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkSyetem : MonoBehaviour
{
    public int talkCnt = 10;       // 대화의 총 갯수를 설정해줍니다.
    int strCnt = 0;         // 이 변수가 하나씩 커져가면서 대화를 진행합니다.
    string[] talk;          // 대화 내용을 저장할 공간입니다.
    Text txt;        // Text 오브젝트에 접근하기
    int[] showCnt;
    // Use this for initialization
    void Start()
    {
        talk = new string[talkCnt]; // 대화 저장 공간을 초기화해줍니다.
        txt = transform.GetChild(0).GetChild(1).GetComponent<Text>();
        txt.text = null;
        initialized();
        
        //StartCoroutine(ShowAll());
    }

    //IEnumerator ShowAll()
    //{
    //    do
    //    {
    //        if (Input.GetKeyDown(KeyCode.Z))
    //        {
    //            txt.text = "";
    //            strCnt++;
    //            if (strCnt == talkCnt) gameObject.SetActive(false);
    //        }
    //        if (!txt.text.Equals(talk[strCnt]))
    //        {
    //            for (int i = 0; i < talk[strCnt].Length; i++)
    //            {
    //                txt.text += talk[strCnt][i];
    //                yield return new WaitForSeconds(0.1f);
    //            }
    //        }
    //    } while (strCnt > talkCnt);
    //}

    void initialized()
    {
        talk[0] = "힝";
        talk[1] = "히이이이이이이이이잉";
    }
}
