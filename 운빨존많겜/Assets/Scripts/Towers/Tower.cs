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

    [Header("Tower")]
    [SerializeField] protected float m_range;
    public float range { get { return m_range; } set { m_range = value; scanCollider.radius = value; } }
    [SerializeField] protected float fireRate, damage;
    [SerializeField] Transform model;
    [SerializeField] SpriteRenderer m_sprite;
    public SpriteRenderer sprite { get { return m_sprite; } }
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
    protected bool canAttack { get; private set; } = true;
    protected virtual void Update()
    {
        if (!canAttack) return;
        if (counter < fireRate) counter += Time.deltaTime;
        else
        {
            enemies.RemoveAll((Enemy i) => i == null);
            if(enemies.Count > 0)
            {
                counter = 0.0f;
                GameManager.Instance.SortEnemies();
                enemies.Sort((Enemy a, Enemy b) => TargettingCompare(a, b));
                if (enemies[0].transform.position.x > transform.position.x)
                {
                    model.localScale = new Vector2(-1.0f, 1.0f);
                }
                else
                {
                    model.localScale = new Vector2(1.0f, 1.0f);
                }
                anim.SetTrigger("Attack");
                Attack();
            }
        }
    }
    protected abstract void Attack();
    protected virtual int TargettingCompare(Enemy a, Enemy b)
    {
        return GameManager.Instance.GetIndex(a).CompareTo(GameManager.Instance.GetIndex(b));
    }
    public virtual void Select()
    {
        
    }
    public virtual void Unselect()
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
