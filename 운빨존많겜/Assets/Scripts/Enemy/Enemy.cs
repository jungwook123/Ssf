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
    int pointIndex = 1;

    const float moveSpeed = 1.0f;
    Transform targetPoint { get { return GameManager.Instance.enemyWaypoints[pointIndex]; } }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        rb.mass = 0.0f;

        hp = maxHp;
        pointIndex = 1;
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
                pointIndex = 0;
                GameManager.Instance.EnemyIndexReset(this);
            }
        }
    }
    public void GetDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0) Destroy(gameObject);
        else hpScaler.localScale = new Vector2(hp / maxHp, 1.0f);
    }
}
