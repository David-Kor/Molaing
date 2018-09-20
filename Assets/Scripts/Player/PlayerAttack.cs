using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackDistance;

    private PlayerAnimation playerAnimation;
    private PlayerState playerState;

    private Vector2 attackDirect;
    private float attackTimer;
    private bool isAttackInput;

    void Start()
    {
        playerState = transform.parent.GetComponent<PlayerState>();
        playerAnimation = transform.parent.GetComponentInChildren<PlayerAnimation>();
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


    void OnHitAttack()
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, attackDirect, attackDistance);

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider != null && hit[i].collider.CompareTag("HitPoint"))
            {
                if (hit[i].transform.parent.CompareTag("Player")) { continue; }

                Debug.Log("On Hit ( " + hit[i].transform.parent.name + " )");
                break;
            }
        }
    }
}
