using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwistedFateBullet : MonoBehaviour
{
    float damage, speed;
    int AttCnt = 0;
    const float range = 50.0f;
    Pooler<TwistedFateBullet> originPool;
    private void Awake()
    {
        GameManager.Instance.onGameOver += () => { Release(); };
    }
    public void Set(float damage, float speed, Pooler<TwistedFateBullet> origin)
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
            
            if (AttCnt == 4)
            {
                collision.GetComponent<Enemy>().GetDamage(damage+10f);
                AttCnt = 0;
            }
            else
            {
                collision.GetComponent<Enemy>().GetDamage(damage);
                AttCnt++;
            }
            
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
}
