using System.Collections;
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
    public int towerCount { get; private set; } = 1;
    public readonly int maxTowerCount = 3;
    [SerializeField] GameObject[] models = new GameObject[3];
    public float range { get { return m_range; } set { m_range = value; scanCollider.radius = value; } }

    [Header("Statistics")]
    [SerializeField] protected float fireRate, damage;
    protected virtual void Awake()
    {
        scanCollider = GetComponent<CircleCollider2D>();
        scanCollider.radius = range;
        scanCollider.isTrigger = true;

        anim = GetComponent<Animator>();

        foreach (var i in models) i.SetActive(false);
        models[0].SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemies.Add(collision.GetComponent<Enemy>());
            enemies.Sort((Enemy a, Enemy b) => GameManager.Instance.GetIndex(a).CompareTo(GameManager.Instance.GetIndex(b)));
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemies.Remove(collision.GetComponent<Enemy>());
        }
    }
    public bool AddTower()
    {
        if (towerCount == maxTowerCount) return false;
        models[++towerCount - 1].SetActive(true);
        return true;
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
        anim.SetTrigger("Attack");
    }
    IEnumerator moving = null;
    public void Move(Transform moveTo)
    {
        if (moving != null) StopCoroutine(moving);
        moving = Moving(moveTo);
        StartCoroutine(moving);
    }
    const float towerLerpSpeed = 5.0f;
    IEnumerator Moving(Transform moveTo)
    {
        while(Vector2.Distance(transform.position, moveTo.position) > 0.1f)
        {
            transform.position = Vector2.Lerp(transform.position, moveTo.position, towerLerpSpeed * Time.deltaTime);
            yield return null;
        }
    }
    public void Select()
    {
        
    }
    public void Unselect()
    {
        
    }
    public void Disable()
    {
        canAttack = false;
    }
    public void Enable()
    {
        canAttack = true;
    }
}
