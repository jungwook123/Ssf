using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    float damage, speed;
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
            OnHit(collision.GetComponent<Enemy>());
            Release();
        }
    }
    protected virtual void OnHit(Enemy hitEnemy)
    {
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
