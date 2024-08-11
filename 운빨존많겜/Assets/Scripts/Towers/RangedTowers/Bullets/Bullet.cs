using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    protected float damage, speed;
    const float range = 50.0f;
    Pooler<Bullet> originPool;
    private void Awake()
    {
        GameManager.Instance.onGameOver += () => { Release(); };
    }
    public void Set(float damage, float speed, Pooler<Bullet> origin)
    {
        this.damage = damage;
        this.speed = speed;
        originPool = origin;
        counter = 0.0f;
        released = false;
        triggered = false;
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
    bool triggered = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered) return;
        if (collision.CompareTag("Enemy"))
        {
            triggered = true;
            OnHit(collision.GetComponent<Enemy>());
            Release();
        }
    }
    protected virtual void OnHit(Enemy hitEnemy)
    {
        GameManager.Instance.UIs.DamageUI(hitEnemy, damage);
        hitEnemy.GetDamage(damage);
    }
    void Release()
    {
        if (released) return;
        released = true;
        originPool.ReleaseObject(this);
        GameManager.Instance.onGameOver -= Release;
    }
}
