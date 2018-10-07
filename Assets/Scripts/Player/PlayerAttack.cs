using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange;

    private PlayerAnimation playerAnimation;
    private PlayerStatus playerState;
    private BasicAttack basicAttack;    //플레이어의 기본 공격에 대한 정보가 담긴 객체

    private Vector2 attackDirect;
    private float attackTimer;
    private bool isAttackInput;

    void Start()
    {
        playerState = transform.parent.GetComponent<PlayerStatus>();
        playerAnimation = transform.parent.GetComponentInChildren<PlayerAnimation>();
        basicAttack = new BasicAttack();
        basicAttack.isKnockBack = true;
        basicAttack.SetDamage(playerState.attackDamage);
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
                if (playerState.attackSpeed == 0) { return; }

                attackTimer = 1 / playerState.attackSpeed;
                playerAnimation.SetAttackMotionSpeed(playerState.attackSpeed);
                playerAnimation.StartAttack();
                attackDirect = playerAnimation.GetPlayerSpriteDirect();
                OnHitAttack();
            }
        }
        else { playerAnimation.StopAttack(); }
    }


    public void Attack() { isAttackInput = true; }


    public void StopAttack() { isAttackInput = false; }


    /* 기본 공격 사거리 안에 HitPoint가 있는지 검사 */
    void OnHitAttack()
    {
        float distance = attackRange;
        if (attackDirect == Vector2.down) { distance += 0.07f; }

        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, attackDirect, distance);

        for (int i = 0; i < hit.Length; i++)
        {
            //HitPoint가 감지되면 기본 공격에 대한 정보를 넘김
            if (hit[i].collider != null && hit[i].collider.CompareTag("HitPoint"))
            {
                HitObject hitComponent = hit[i].collider.GetComponent<HitObject>();

                if (hitComponent.GetName() == "Player") { continue; } //플레이어의 HitPoint면 무시

                basicAttack.attackDirect = attackDirect * 0.1f;
                hitComponent.OnHitSkill(basicAttack);
                break;
            }
        }
    }
}
