using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Helper : MonoBehaviour
{
    public float xDist;
    public float yDist;
    public float floatingSpeed;

    public float txtPrintSpeed;      //문자 출력 속도
    public float defaultPrintTime;  //문자 출력 시간
    public GameObject canvas;

    private Queue<string> msgQueue;
    private Text msgUI;       //텍스트 UI
    private Vector2 originPos;
    private Vector2 direction;

    void Awake()
    {
        if (msgQueue == null)
        {
            msgQueue = new Queue<string>();
        }
        SetActiveHelpMassage(true);
        msgUI = transform.parent.GetComponentInChildren<Text>();
        msgUI.text = "";
        SetActiveHelpMassage(false);
        AddHelpMassage("히이이이이이이이이이이이이이이이이이이이이이이이이이이이이이잉");
        AddHelpMassage("뀨이이이이이이이이이이이이이이이이이이이이이이이이이이이이이잉");
        PrintHelpMassage();
    }

    void Start()
    {
        originPos = transform.localPosition;
        direction = Vector2.zero;
    }

    void Update()
    {
        if (transform.localPosition.y <= originPos.y)
        {
            direction = Vector2.left + (Vector2.up * direction.y);
            if (transform.localPosition.x <= originPos.x - xDist)
            {
                direction = Vector2.right + (Vector2.up * direction.y);
            }
        }
        else
        {
            direction = Vector2.right + (Vector2.up * direction.y);
            if (transform.localPosition.x >= originPos.x)
            {
                direction = Vector2.left + (Vector2.up * direction.y);
            }
        }
        if (transform.localPosition.x <= originPos.x - (xDist * 0.5f))
        {
            direction = (Vector2.right * direction.x) + Vector2.up;
            if (transform.localPosition.y >= originPos.y + (yDist * 0.5f))
            {
                direction = (Vector2.right * direction.x) + Vector2.up;
            }
        }
        else
        {
            direction = (Vector2.right * direction.x) + Vector2.down;
            if (transform.localPosition.y <= originPos.y - (yDist * 0.5f))
            {
                direction = (Vector2.right * direction.x) + Vector2.down;
            }
        }
        transform.Translate(direction * floatingSpeed * Time.deltaTime, Space.Self);
    }


    /* 메시지를 한 글자씩 출력 */
    private IEnumerator PrintText()
    {
        Queue<char> _txtQueue = new Queue<char>();
        string _msg;

        while (msgQueue.Count > 0)
        {
            _msg = msgQueue.Dequeue();

            for (int i = 0; i < _msg.Length; i++)
            {
                _txtQueue.Enqueue(_msg[i]);
            }

            while (_txtQueue.Count > 0)
            {
                msgUI.text += _txtQueue.Dequeue();
                yield return new WaitForSeconds(txtPrintSpeed);
            }
            yield return new WaitForSeconds(defaultPrintTime);
            msgUI.text = "";
        }
        SetActiveHelpMassage(false);
    }


    /* Helper의 메시지 창이 활성화 상태인지 알려줌 */
    public bool GetActiveMassage() { return canvas.activeSelf; }


    /* Helper의 메시지 UI 활성화/비활성화 */
    public void SetActiveHelpMassage(bool value) { canvas.SetActive(value); }


    /* 출력할 메시지를 큐에 추가 */
    public void AddHelpMassage(string _msg)
    {
        if (msgQueue == null)
        {
            msgQueue = new Queue<string>();
        }
        //새 메시지를 큐에 추가
        msgQueue.Enqueue(_msg);
        
        //UI가 비활성화 상태라면 활성화시킴
        if (!GetActiveMassage()) { SetActiveHelpMassage(true); }
        msgUI.text = "";
    }


    /* 저장된 메시지들을 출력 */
    public void PrintHelpMassage()
    {
        StartCoroutine("PrintText");
    }

}
