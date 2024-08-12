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
    public float hp { get; private set; }
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
                return;
            }
        }
        T tmp = new T();
        debuffs.Add(tmp);
        tmp.Set(duration, this);
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
    bool canMove = true;
    public void DisableMovement()
    {
        canMove = false;
    }
    public void EnableMovement()
    {
        canMove = true;
    }
    protected virtual void Update()
    {
        if (canMove)
        {
            float dist = Vector2.Distance(transform.position, targetPoint.position);
            if (dist > 0.01f)
            {
                transform.Translate(Vector2.ClampMagnitude((targetPoint.position - transform.position).normalized * Mathf.Max(moveSpeed, 0.05f) * Time.deltaTime, dist));
            }
            else
            {
                if (++pointIndex >= GameManager.Instance.enemyWaypoints.Length)
                {
                    ReachEnd();
                    return;
                }
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
    public Action onDeath;
    protected virtual void ReachEnd()
    {
        GameManager.Instance.RemoveEnemy(this);
        GameManager.Instance.GetBaseDamage(hp);
        onDeath?.Invoke();
        Destroy(gameObject);
    }
    protected virtual void Die()
    {
        GameManager.Instance.MoneyChange(moneyReward);
        GameManager.Instance.RemoveEnemy(this);
        onDeath?.Invoke();
        Destroy(gameObject);
    }
}
