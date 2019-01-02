using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject basicAttackPrefab;    //기본공격 프리팹

    private PlayerAnimation playerAnimation;    //플레이어의 애니메이션 담당 클래스
    private PlayerStatus playerStatus;              //스탯 클래스
    private BasicAttack basicAttack;                //기본공격 스킬 정보 클래스

    private Vector2 attackDirect;   //기본공격 방향
    private float attackTimer;       //기본공격 타이머
    private bool isAttackInput;     //기본공격 키 입력

    void Start()
    {
        playerStatus = transform.parent.GetComponent<PlayerStatus>();
        playerAnimation = transform.parent.GetComponentInChildren<PlayerAnimation>();
        //기본공격 정보 설정
        basicAttack = gameObject.AddComponent<BasicAttack>();
        basicAttack.knockBackPower = playerStatus.basicKnockBackPower;
        basicAttack.skillCaster = transform.parent.gameObject;
        basicAttack.isKnockBack = true;
        basicAttack.damage = playerStatus.attackDamage;
        attackTimer = 0f;
        isAttackInput = false;
        attackDirect = Vector2.down;
    }

    void Update()
    {
        //타이머가 초기화 되지 않았고, 공격 버튼을 누르지 않은 상태
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }

        //공격 버튼이 눌려있는 상태
        if (isAttackInput)
        {
            if (attackTimer <= 0)
            {
                //공격 속도가 0인 경우(공격 불가 상태) 실행 안함 / Divid zero 예외처리 겸용
                if (playerStatus.attackSpeed == 0) { return; }

                //공격 모션 적용
                attackTimer = 1 / playerStatus.attackSpeed;
                playerAnimation.SetAttackMotionSpeed(playerStatus.attackSpeed);
                playerAnimation.StartAttack();
                attackDirect = playerAnimation.GetPlayerSpriteDirect();

                //기본공격 프리팹을 인스턴스화
                //공격 방향에 따라 범위 회전 및 위치 변경
                //상하좌우 순서로 transform자식이 각각의 위치에 배치되어있음
                GameObject newAttack;
                if (attackDirect == Vector2.up)
                {
                    newAttack = Instantiate(basicAttackPrefab, transform.GetChild(0));
                    newAttack.transform.Rotate(0, 0, 180);
                }
                else if (attackDirect == Vector2.down)
                {
                    newAttack = Instantiate(basicAttackPrefab, transform.GetChild(1));
                    newAttack.transform.Rotate(0, 0, 0);
                }
                else if (attackDirect == Vector2.left)
                {
                    newAttack = Instantiate(basicAttackPrefab, transform.GetChild(2));
                    newAttack.transform.Rotate(0, 0, -90);
                }
                else
                {
                    newAttack = Instantiate(basicAttackPrefab, transform.GetChild(3));
                    newAttack.transform.Rotate(0, 0, 90);
                }

                OnHitBasicAttack(newAttack.GetComponent<Collider2D>());
                Destroy(newAttack);
            }
        }
        else { playerAnimation.StopAttack(); }
    }


    public void Attack() { isAttackInput = true; }


    public void StopAttack() { isAttackInput = false; }


    /* 기본공격 판정 범위 안에 HitPoint가 있는지 검사 */
    private void OnHitBasicAttack(Collider2D col)
    {
        Collider2D[] hits = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D()
        {
            useTriggers = true,     //isTrigger 충돌체도 검사함
            useLayerMask = true,  //레이어 마스크 사용함
            //HitPoint 레이어 외 전부 무시
            layerMask = new LayerMask() { value = (1 << LayerMask.NameToLayer("HitPoint")) }
        };

        int count = Physics2D.OverlapCollider(col, filter, hits);

        HitObject hitObj;
        for (int i = 0; i < count; i++)
        {
            hitObj = hits[i].GetComponent<HitObject>();
            //대상이 Player자신이면 무시
            if (hitObj.GetName() == "Player") { continue; }

            basicAttack.skillDirect = attackDirect;
            hitObj.OnHitSkill(basicAttack);
        }
    }
}
