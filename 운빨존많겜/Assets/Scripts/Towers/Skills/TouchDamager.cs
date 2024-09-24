using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TouchDamager : MonoBehaviour
{
    [SerializeField] int damage;
    Collider2D hitbox;
    private void Awake()
    {
        hitbox = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().GetDamage(damage);
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
