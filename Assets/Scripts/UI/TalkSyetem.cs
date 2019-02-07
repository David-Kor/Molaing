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

        StartCoroutine(CountDialog());
    }
    IEnumerator CountDialog()
    {
        do
        {

            txt.text = "";
            StartCoroutine(ShowAll(talk[strCnt]));
            if (Input.GetKeyDown(KeyCode.Z))
            {
                txt.text = "";
                strCnt++;
                if (strCnt == talkCnt)
                {
                    IEnumerator Coroutine = CountDialog();
                    StopCoroutine(Coroutine);
                    gameObject.SetActive(false);
                }
                Debug.Log("대기중");
            }
            yield return new WaitForSeconds(0.01f);
        } while (true);
    }
    IEnumerator ShowAll(string a)
    {
        Debug.Log(a.Length);
        if (!txt.text.Equals(a))
        {
            for (int i = 0; i < a.Length; i++)
            {
                txt.text += a[i];
                yield return new WaitForSeconds(0.1f);
            }
        }
        yield return null;
        IEnumerator coroutine = ShowAll(a);
        StopCoroutine(coroutine);

    }
    void initialized()
    {
        talk[0] = "힝";
        talk[1] = "히이이이이이이이이잉";
    }
}
