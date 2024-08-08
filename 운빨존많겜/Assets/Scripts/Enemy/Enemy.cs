using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float m_maxHp;
    public float maxHp { get { return m_maxHp; } }
    float hp;
    [SerializeField] Transform hpScaler;
    [SerializeField] Animator anim;
    [SerializeField] protected int moneyReward = 1;
    public int pointIndex { get; private set; } = 1;

    public float moveSpeed = 1.0f;
    Transform targetPoint { get { return GameManager.Instance.enemyWaypoints[pointIndex]; } }

    List<Debuff> debuffs = new();
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        rb.mass = 0.0f;

        hp = maxHp;
        pointIndex = 1;
    }
    public void AddDebuff<T>(float duration) where T : Debuff, new()
    {
        foreach (var i in debuffs)
        {
            if (i is T)
            {
                i.ResetDuration(duration);
                Debug.Log("Duration reset");
                return;
            }
        }
        T tmp = new T();
        tmp.Set(duration, this);
        debuffs.Add(tmp);
    }
    List<Debuff> removeQueue = new();
    public void RemoveDebuff(Debuff debuff)
    {
        removeQueue.Add(debuff);
    }
    public bool FindDebuff<T>() where T : Debuff
    {
        foreach(var i in debuffs)
        {
            if (i is T) return true;
        }
        return false;
    }
    public T GetDebuff<T>() where T : Debuff
    {
        foreach (var i in debuffs)
        {
            if (i is T) return i as T;
        }
        return null;
    }
    protected virtual void Update()
    {
        float dist = Vector2.Distance(transform.position, targetPoint.position);
        if (dist > 0.01f)
        {
            transform.Translate(Vector2.ClampMagnitude((targetPoint.position - transform.position).normalized * moveSpeed * Time.deltaTime, dist));
        }
        else
        {
            if (++pointIndex >= GameManager.Instance.enemyWaypoints.Length)
            {
                ReachEnd();
                return;
            }
        }
        foreach (var i in debuffs) i.OnUpdate();
    }
    private void LateUpdate()
    {
        foreach (var i in removeQueue) debuffs.Remove(i);
    }
    public void GetDamage(float damage)
    {
        hp -= damage;
        if (anim != null) anim.SetTrigger("Damaged");
        if (hp <= 0) Die();
        else hpScaler.localScale = new Vector2(hp / maxHp, 1.0f);
    }
    protected virtual void ReachEnd()
    {
        GameManager.Instance.enemies.Remove(this);
        Destroy(gameObject);
    }
    public Action onDeath;
    protected virtual void Die()
    {
        GameManager.Instance.MoneyChange(moneyReward);
        GameManager.Instance.enemies.Remove(this);
        onDeath?.Invoke();
        Destroy(gameObject);
    }
}
