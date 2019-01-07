using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    protected AttackSkill skillInfo;
    protected Vector2 direction;
    protected bool isPullTrigger = false;
    protected float timer;
    protected float speed;
    protected int hitCount;

    void Update()
    {
        if (isPullTrigger)
        {
            timer += Time.deltaTime;
            if (timer >= skillInfo.lifeTime)
            {
                Destroy(gameObject);
                return;
            }

            //Bullet이 바라보는 방향을 향해 움직임
            transform.Translate(Vector2.up * Time.deltaTime * speed, Space.Self);
        }
    }


    /* 특정 방향으로 쏘는 경우 호출 */
    public void ShotToDirection(AttackSkill _skillInfo, Vector2 _direction, float _speed)
    {
        //초기화
        transform.SetParent(null);
        skillInfo = _skillInfo;
        timer = 0;
        speed = _speed;
        direction = _direction.normalized;
        hitCount = 0;

        //목표 방향을 바라보도록 설정
        float angle = Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x) - 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        isPullTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("HitPoint"))
        {
            if (col.transform.parent.gameObject == skillInfo.skillCaster) { return; }
            OnHitBullet(col.GetComponent<HitObject>());
        }
    }

    protected abstract void OnHitBullet(HitObject hit);
}
