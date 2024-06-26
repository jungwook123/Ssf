using System.Collections;
using System.Collections.Generic;
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
    protected Animator anim;
    protected List<Enemy> enemies = new();

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
        Debug.Log(collision.transform.name);
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
    public void Select()
    {
        anim.SetBool("Selected", true);
    }
    public void Unselect()
    {
        anim.SetBool("Selected", false);
    }
    float counter = 0.0f;
    protected virtual void Update()
    {
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
        
    }
}
