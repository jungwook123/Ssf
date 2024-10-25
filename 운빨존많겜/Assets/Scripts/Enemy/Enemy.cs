using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region 개발자 전용
[RequireComponent(typeof(Rigidbody2D))]
#endregion
public class Enemy : MonoBehaviour
{
    #region 개발자 전용
    Rigidbody2D rb;
    [SerializeField] float m_maxHp;
    public float maxHp { get { return m_maxHp; } }
    public float hp { get; private set; }
    [SerializeField] Transform model, hpScaler;
    [SerializeField] Animator anim;
    [SerializeField] protected int moneyReward = 1;
    public int pointIndex { get; private set; } = 1;

    public float moveSpeed = 1.0f;
    Transform targetPoint { get { return GameManager.Instance.enemyWaypoints[pointIndex]; } }

    internal List<Debuff> debuffs = new();
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        rb.mass = 0.0f;

        hp = maxHp;
        pointIndex = 1;

        if (targetPoint.position.x > transform.position.x) model.localScale = new Vector2(-1.0f, 1.0f);
        else model.localScale = new Vector2(1.0f, 1.0f);
    }
    public void AddDebuff(Debuff debuff)
    {
        debuff.AddDebuff(this);
    }
    public bool FindDebuff<T>() where T : Debuff
    {
        if (GetDebuff<T>() != null) return true;
        else return false;
    }
    public bool FindDebuff<T>(T reapplied) where T : Debuff
    {
        T tmp = GetDebuff<T>();
        if (tmp != null)
        {
            tmp.ReApply(reapplied);
            return true;
        }
        else return false;
    }
    public bool FindDebuff<T>(Action<T> onDebuffFound) where T : Debuff
    {
        T tmp = GetDebuff<T>();
        if (tmp != null)
        {
            onDebuffFound?.Invoke(tmp);
            return true;
        }
        else return false;
    }
    List<Debuff> removeQueue = new();
    public void RemoveDebuff(Debuff debuff)
    {
        removeQueue.Add(debuff);
    }
    public T GetDebuff<T>() where T : Debuff
    {
        foreach (var i in debuffs)
        {
            if (i is T && !removeQueue.Contains(i)) return i as T;
        }
        return null;
    }
    public Stackbool canMove;
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
                    OnEndReach();
                    return;
                }
                else
                {
                    if (targetPoint.position.x > transform.position.x) model.localScale = new Vector2(-1.0f, 1.0f);
                    else model.localScale = new Vector2(1.0f, 1.0f);
                }
            }
        }
        foreach (var i in debuffs) i.OnUpdate();
        debuffs.RemoveAll((Debuff i) => removeQueue.Contains(i));
        removeQueue.Clear();
    }
    public Action onDeath;
    bool isDead = false;
    #endregion

    public void GetDamage(float damage)
    {
        #region 개발자 전용
        if (isDead) return;
        hpScaler.localScale = new Vector2(hp / maxHp, 1.0f);
        if (anim != null) anim.SetTrigger("Damaged");
        #endregion
        hp -= damage;
        if (hp <= 0) Die();
    }
    private void Die()
    {
        #region 개발자 전용
        if (isDead) return;
        isDead = true;
        GameManager.Instance.MoneyChange(moneyReward);
        GameManager.Instance.RemoveEnemy(this);
        onDeath?.Invoke();
        #endregion
        Destroy(gameObject);
    }
    protected virtual void OnEndReach()
    {
        #region 개발자 전용
        if (isDead) return;
        isDead = true;
        GameManager.Instance.RemoveEnemy(this);
        GameManager.Instance.GetBaseDamage(hp);
        onDeath?.Invoke();
        #endregion
        Destroy(gameObject);
    }
}
