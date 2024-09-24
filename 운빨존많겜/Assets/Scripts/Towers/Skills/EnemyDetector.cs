using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyDetector : MonoBehaviour
{
    int damage = 0;
    Collider2D hitbox;
    Action<Enemy> onEnemyHit;
    private void Awake()
    {
        hitbox = GetComponent<Collider2D>();
    }
    public void Set(int damage, Action<Enemy> onEnemyHit)
    {
        this.damage = damage;
        this.onEnemyHit = onEnemyHit;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy tmp = collision.GetComponent<Enemy>();
            onEnemyHit?.Invoke(tmp);
        }
    }
    private void OnDisable()
    {
        hitbox.enabled = false;
    }
    private void OnEnable()
    {
        hitbox.enabled = true;
    }
}
