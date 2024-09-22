using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    static Dictionary<Bullet, Pooler<Bullet>> bulletPools = new();

    protected bool isInstantiated = false;
    protected Pooler<Bullet> origin;

    protected float damage, speed;
    const float range = 50.0f;
    protected AudioVolumePair hitSound;
    protected Debuff inflictingDebuff;

    protected virtual bool pierce => false;
    private void Awake()
    {
        GameManager.Instance.onGameOver += () => { Release(); };
    }
    public Bullet SpawnBullet(Vector2 position, Quaternion rotation)
    {
        if (!bulletPools.ContainsKey(this))
        {
            bulletPools.Add(this, new Pooler<Bullet>(this));
        }
        Bullet tmp = bulletPools[this].GetObject(position, rotation);
        tmp.isInstantiated = true;
        tmp.origin = bulletPools[this];
        return tmp;
    }
    public virtual void Set(float damage, float speed, AudioVolumePair hitSound, Debuff inflictingDebuff)
    {
        if (!isInstantiated) return;
        this.damage = damage;
        this.speed = speed;
        this.hitSound = hitSound;
        this.inflictingDebuff = inflictingDebuff;
        counter = 0.0f;
        released = false;
        triggered = false;
        exception = null;
        GameManager.Instance.onGameOver += Release;
    }
    BulletException[] exception;
    public void SetException(params BulletException[] exceptions)
    {
        exception = exceptions;
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
        if (triggered && !pierce) return;
        if (collision.CompareTag("Enemy"))
        {
            Enemy tmp = collision.GetComponent<Enemy>();
            if(exception != null)
            {
                foreach (var i in exception) if (i.CanHit(tmp) == false) return;
            }
            triggered = true;
            OnHit(tmp);
            if(exception != null)
            {
                foreach (var i in exception) i.OnHit(tmp);
            }
            if(!pierce) Release();
        }
    }
    protected virtual void OnHit(Enemy hitEnemy)
    {
        AudioManager.Instance.PlayAudio(hitSound);
        GameManager.Instance.UIs.DamageUI(hitEnemy, damage);
        if (inflictingDebuff != null) hitEnemy.AddDebuff(inflictingDebuff);
        hitEnemy.GetDamage(damage);
    }
    void Release()
    {
        if (!isInstantiated) return;
        if (released) return;
        released = true;
        origin.ReleaseObject(this);
        GameManager.Instance.onGameOver -= Release;
    }
}
public abstract class BulletException
{
    public BulletException()
    {

    }
    public abstract bool CanHit(Enemy hitEnemy);
    public virtual void OnHit(Enemy hitEnemy) { }
}
public class HitCountException : BulletException
{
    readonly int maxHitCount;
    public HitCountException(int maxHitCount)
    {
        this.maxHitCount = maxHitCount;
    }
    Dictionary<Enemy, int> hitCount = new();
    public override bool CanHit(Enemy hitEnemy)
    {
        Debug.Log("EEEe");
        if (hitCount.ContainsKey(hitEnemy))
        {
            if (hitCount[hitEnemy] >= maxHitCount) return false;
            else return true;
        }
        else return true;
    }
    public override void OnHit(Enemy hitEnemy)
    {
        base.OnHit(hitEnemy);
        if (!hitCount.ContainsKey(hitEnemy))
        {
            hitCount.Add(hitEnemy, 1);
            Debug.Log("RRRRR");
        }
        else
        {
            hitCount[hitEnemy]++;
            Debug.Log("ADDDD");
        }
    }
}
public class ExcludeSpecificException : BulletException
{
    readonly Enemy ignore;
    public ExcludeSpecificException(Enemy ignore)
    {
        this.ignore = ignore;
    }
    public override bool CanHit(Enemy hitEnemy)
    {
        return hitEnemy != ignore;
    }
}