using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkTest : MonoBehaviour
{
    int talkCnt = 10;       // 대화의 총 갯수를 설정해줍니다.
    int strCnt = 0;         // 이 변수가 하나씩 커져가면서 대화를 진행합니다.
    string[] talk;          // 대화 내용을 저장할 공간입니다.
    public Text txt;        // Text 오브젝트에 접근하기
    public Image showText;

    static public bool TalkFlag = false;
    // Use this for initialization
    void Start()
    {
        if(strCnt < 10)
        {
            talk = new string[talkCnt]; // 대화 저장 공간을 초기화해줍니다.
            txt = GameObject.Find("Talk").transform.Find("Canvas").transform.Find("Talk_Text").GetComponent<Text>();
            // 캔버스 오브젝트 아래 자식 오브젝트로 있는 Text를 호출합니다.
            showText = GameObject.Find("Talk").transform.Find("Canvas").transform.Find("Talk_Screen").GetComponent<Image>();
            // 캔버스 오브젝트 아래 자식 오브젝트로 있는 talkScreen을 호출합니다.

            initialized();      // 대화를 설정하는 함수입니다.
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") || Input.GetButtonDown("Jump"))
        {
            if (strCnt < 10 && strCnt > -1) strCnt++;
            // '엔터'나 '스페이스바'를 누르면 카운트가 올라갑니다.
        }
        showAll();  // 화면에 나오게 하는 함수로 연ㅋ결ㅋ
    }
    void showAll()
    {

        if (strCnt > -1 && strCnt < talkCnt)
        {
            showText.gameObject.SetActive(true);
            txt.enabled = true;
            txt.text = talk[strCnt];
        }
        else
        {
            showText.gameObject.SetActive(false);
            txt.enabled = false;
            TalkFlag = false;
            strCnt = -1;
        }


        // strCnt의 차례에 맞춰 대화를 출력합니다.
    }

    void initialized()
    {
        
        // 대화 내용을 하나하나 추가합니다.
        talk[0] = "안녕?";
        talk[1] = "난 졸리고 안되면 노트북을 때릴거야";
        talk[2] = "오늘 저녁은 뭘 먹을까";
        talk[3] = "점심은... 도서관 CU에서 먹으면 될거고";
        talk[4] = "나 점심 뭐 먹냐?";
        talk[5] = "라면?";
        talk[6] = "빵?";
        talk[7] = "삼각김밥?";
        talk[8] = "졸리네";
        talk[9] = "일단 자고 대화쪽은 자고 일어나서 생각해보자";
        ////////////////////////////////////////
        

    }
}
