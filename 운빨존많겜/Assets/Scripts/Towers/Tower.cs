using System.Collections;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D), typeof(Animator))]
public abstract class Tower : MonoBehaviour
{
    TowerData m_data;
    public void Set(TowerData data)
    {
        m_data = data;
    }
    public TowerData data { get { return m_data; } }
    
    CircleCollider2D scanCollider;
    protected Animator anim { get; private set; }
    protected List<Enemy> enemies { get; } = new();

    [SerializeField] protected float m_range;
    public float range { get { return m_range; } set { m_range = value; scanCollider.radius = value; } }

    [Header("Statistics")]
    [SerializeField] protected float fireRate, damage;
    protected virtual void Awake()
    {
        scanCollider = GetComponent<CircleCollider2D>();
        scanCollider.radius = range;
        scanCollider.isTrigger = true;

        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemies.Add(collision.GetComponent<Enemy>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemies.Remove(collision.GetComponent<Enemy>());
        }
    }
    float counter = 0.0f;
    bool canAttack = true;
    protected virtual void Update()
    {
        if (!canAttack) return;
        if (counter < fireRate) counter += Time.deltaTime;
        else
        {
            if(enemies.Count > 0)
            {
                counter = 0.0f;
                Attack();
            }
        }
    }
    public virtual void Attack()
    {
        enemies.Sort((Enemy a, Enemy b) => TargettingCompare(a, b));
        anim.SetTrigger("Attack");
    }
    protected virtual int TargettingCompare(Enemy a, Enemy b)
    {
        return GameManager.Instance.GetIndex(a).CompareTo(GameManager.Instance.GetIndex(b));
    }
    public void Select()
    {
        
    }
    public void Unselect()
    {
        
    }
    public virtual void Disable()
    {
        canAttack = false;
    }
    public virtual void Enable()
    {
        canAttack = true;
    }
}
