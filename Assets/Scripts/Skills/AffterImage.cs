using UnityEngine;

public class AffterImage : MonoBehaviour
{
    public Sprite[] imgs;
    public float frameTime;

    private float timer;
    private int index;
    private bool isInit;
    private SpriteRenderer myRenderer;
    private SpriteRenderer pivotRenderer;

    void Start()
    {
        timer = 0;
        index = 0;
        myRenderer = GetComponent<SpriteRenderer>();
        if (imgs != null)
        {
            myRenderer.sprite = imgs[index++];
        }
    }

    void Update()
    {
        if (isInit == false) { return; }
        if (imgs == null) { return; }

        timer += Time.deltaTime;
        if (timer >= frameTime)
        {
            //기준 sprite가 있으면 항상 기준 sprite보다 뒤쪽으로 정렬
            if (pivotRenderer != null &&
                pivotRenderer.sortingOrder <= myRenderer.sortingOrder)
            {
                myRenderer.sortingOrder = pivotRenderer.sortingOrder - 1;
            }

            if (index >= imgs.Length)
            {
                Destroy(gameObject);
                return;
            }
            myRenderer.sprite = imgs[index++];
        }
    }

    /* 초기화 함수 */
    public void Init(SpriteRenderer _pivotRenderer, Sprite[] _imgs, float _frameTime)
    {
        if (_pivotRenderer != null)
        {
            pivotRenderer = _pivotRenderer;
        }
        if (_imgs != null)
        {
            imgs = _imgs;
        }
        if (_frameTime > 0)
        {
            frameTime = _frameTime;
        }
        timer = 0;
        index = 0;

        isInit = true;
    }
}
