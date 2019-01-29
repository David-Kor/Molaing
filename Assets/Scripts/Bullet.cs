using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    protected AttackSkill skillInfo;
    protected Vector2 direction;
    protected bool isPullTrigger = false;
    protected float lifeTime;
    protected float timer;
    protected float speed;
    protected int hitCount;

    void Update()
    {
        if (isPullTrigger)
        {
            //발사된 순간부터 계속 움직임
            MoveBullet();
        }
    }


    /* 특정 방향으로 쏘는 경우 호출 */
    public void ShotToDirection(AttackSkill _skillInfo, Vector2 _direction, float _speed, float _lifeTime)
    {
        //초기화
        transform.SetParent(null);
        skillInfo = _skillInfo;
        timer = 0;
        speed = _speed;
        direction = _direction.normalized;
        hitCount = 0;
        lifeTime = _lifeTime;

        //목표 방향을 바라보도록 설정
        float angle = Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x) - 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        isPullTrigger = true;
    }


    /* 탄환 충돌 감지 */
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("HitPoint"))
        {
            if (col.transform.parent.tag == skillInfo.skillCaster.tag) { return; }
            OnHitBullet(col.GetComponent<HitObject>());
        }
    }


    /* 탄환이 HitPoint에 닿은 경우 호출 */
    protected abstract void OnHitBullet(HitObject hit);


    /* ShotToDirection()함수가 호출되면 Update에서 계속 호출됨 */
    protected void MoveBullet()
    {
        if (!isPullTrigger) { return; }

        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
            return;
        }

        //Bullet이 바라보는 방향을 향해 움직임
        transform.Translate(Vector2.up * Time.deltaTime * speed, Space.Self);
    }
}
