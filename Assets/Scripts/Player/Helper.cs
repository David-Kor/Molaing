using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Helper : MonoBehaviour
{
    public float sqrt_maxDist;
    public float moveSpeed;

    public float txtPrintSpeed;      //문자 출력 속도
    public float defaultPrintTime;  //문자 출력 시간
    public GameObject canvas;
    public GameObject player;

    private Queue<string> msgQueue;
    private Text msgUI;       //텍스트 UI

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
        AddHelpMassage("TESTESTESTESTST");
        AddHelpMassage("TESTESTESTESTST");
        AddHelpMassage("TESTESTESTESTST");
        PrintHelpMassage();
        transform.parent = null;
    }

    void Update()
    {
        if (TargetDistance() >= sqrt_maxDist)
        {
            transform.Translate((player.transform.position - transform.position) * moveSpeed * Time.deltaTime);
        }
    }


    private float TargetDistance()
    {
        Vector2 _dist = (player.transform.position - transform.position);
        return (_dist.x) * (_dist.x) + (_dist.y) * (_dist.y);
    }


    /* 메시지를 한 글자씩 출력 */
    private IEnumerator PrintText()
    {
        Queue<char> _txtQueue = new Queue<char>();
        string _msg;
        if (!GetActiveMassage()) { SetActiveHelpMassage(true); }

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
        msgUI.text = "";
    }


    /* 저장된 메시지들을 출력 */
    public void PrintHelpMassage()
    {
        StartCoroutine("PrintText");
    }

}
