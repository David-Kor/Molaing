using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange;
    public GameObject basicAttackObject;

    private PlayerAnimation playerAnimation;
    private PlayerStatus playerStatus;
    private BasicAttack basicAttack;    //플레이어의 기본 공격에 대한 정보가 담긴 객체

    private Vector2 attackDirect;
    private float attackTimer;
    private bool isAttackInput;

    void Start()
    {
        playerStatus = transform.parent.GetComponent<PlayerStatus>();
        playerAnimation = transform.parent.GetComponentInChildren<PlayerAnimation>();
        basicAttack = new BasicAttack
        {
            knockBackPower = playerStatus.basicKnockBackPower,
            skillCaster = transform.parent.gameObject,
            isKnockBack = true
        };
        basicAttack.SetDamage(playerStatus.attackDamage);
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

                attackTimer = 1 / playerStatus.attackSpeed;
                playerAnimation.SetAttackMotionSpeed(playerStatus.attackSpeed);
                playerAnimation.StartAttack();
                attackDirect = playerAnimation.GetPlayerSpriteDirect();

                GameObject newAttack;
                if (attackDirect == Vector2.up)
                {
                    newAttack = Instantiate(basicAttackObject, transform.GetChild(0));
                }
                else if (attackDirect == Vector2.down)
                {
                    newAttack = Instantiate(basicAttackObject, transform.GetChild(1));
                }
                else if (attackDirect == Vector2.left)
                {
                    newAttack = Instantiate(basicAttackObject, transform.GetChild(2));
                    newAttack.transform.Rotate(0, 0, 90);
                }
                else
                {
                    newAttack = Instantiate(basicAttackObject, transform.GetChild(3));
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


    /* 기본 공격 사거리 안에 HitPoint가 있는지 검사 */
    private void OnHitBasicAttack(Collider2D col)
    {
        Collider2D[] hits = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D()
        {
            useTriggers = true,
            useLayerMask = true,
            layerMask = new LayerMask() { value = (1 << LayerMask.NameToLayer("HitPoint")) }
        };

        int count = Physics2D.OverlapCollider(col, filter, hits);

        HitObject hitObj;
        for (int i = 0; i < count; i++)
        {
            hitObj = hits[i].GetComponent<HitObject>();
            if (hitObj.GetName() == "Player") { continue; }
            basicAttack.attackDirect = attackDirect;
            hitObj.OnHitSkill(basicAttack);
        }
    }
}
