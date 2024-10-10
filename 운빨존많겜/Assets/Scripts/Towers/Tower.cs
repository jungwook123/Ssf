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

    [Header("Tower")]
    [SerializeField] string m_towerName;
    public string towerName { get { return m_towerName; } }
    [SerializeField] protected float m_range;
    public float range { get { return m_range; } }
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
    float counter = 0.0f;
    protected bool canAttack { get; private set; } = true;
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
    protected void PreAttack()
    {
        if (enemies[0].transform.position.x > transform.position.x)
        {
            model.localScale = new Vector2(-1.0f, 1.0f);
        }
        else
        {
            model.localScale = new Vector2(1.0f, 1.0f);
        }
        anim.SetTrigger("Attack");
    }
    protected virtual void Update()
    {
        if (!canAttack) return;
        if (counter < fireRate) counter += Time.deltaTime;
        else
        {
            enemies.RemoveAll((Enemy i) => i == null);
            if (enemies.Count > 0)
            {
                counter = 0.0f;
                PreAttack();
                enemies.Sort((Enemy a, Enemy b) => TargettingCompare(a, b));
                Attack();
            }
        }
    }
    public virtual string Describe()
    {
        return $"- 대미지 {damage}\n- 공격주기 {fireRate}초\n- 범위 {range}";
    }
    public List<Enemy> enemies = new();
    //현재 감지한 적들을 보관하는 리스트
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //만약 태그가 'Enemy'인 오브젝트가 범위 안에 닿으면..
        if (collision.CompareTag("Enemy"))
        {
            enemies.Add(collision.GetComponent<Enemy>());
            //감지한 적들 리스트에 감지된 오브젝트의 'Enemy' 컴퍼넌트 추가하기
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //만약 범위 닿고 있던 태그가 'Enemy'인 오브젝트가 더 이상 닿지 않으면..
        if (collision.CompareTag("Enemy"))
        {
            enemies.Remove(collision.GetComponent<Enemy>());
            //감지한 적들 리스트에서 더 이상 닿지 않는 오브젝트의 'Enemy' 컴퍼넌트 제거하기
        }
    }
}
