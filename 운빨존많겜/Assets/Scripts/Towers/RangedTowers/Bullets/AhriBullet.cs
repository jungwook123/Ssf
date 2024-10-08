using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class AhriBullet : MonoBehaviour
{
    float damage, speed;
    const float range = 50.0f;
    Pooler<AhriBullet> originPool;
    private void Awake()
    {
        GameManager.Instance.onGameOver += () => { Release(); };
    }
    public void Set(float damage, float speed, Pooler<AhriBullet> origin)
    {
        this.damage = damage;
        this.speed = speed;
        originPool = origin;
        counter = 0.0f;
        released = false;
        GameManager.Instance.onGameOver += Release;
    }
    float counter = 0.0f;
    bool released = false;
    private void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime, Space.World);
        counter += Time.deltaTime;
        if (counter * speed > range) Release();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().GetDamage(damage);
            collision.GetComponent<Enemy>().moveSpeed = 0.8f;
            Release();
        }
    }
    void Release()
    {
        if (released) return;
        released = true;
        originPool.ReleaseObject(this);
        GameManager.Instance.onGameOver -= Release;
    }
}*/
public class AhriBullet : Bullet
{
    protected override void OnHit(Enemy hitEnemy)
    {
        AudioManager.Instance.PlayAudio(hitSound);
        GameManager.Instance.UIs.DamageUI(hitEnemy, damage, new Color(1, 0, 1));
        if (inflictingDebuff != null) hitEnemy.AddDebuff(inflictingDebuff);
        hitEnemy.GetDamage(damage);
    }
}
