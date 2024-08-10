using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garen_Tower : Tower
{
    // 공격할 적의 범위를 나타내는 변수
    [SerializeField] private float attackRange;
    // 5번 공격마다 적을 이동 불가 상태로 만들기 위해 공격 횟수를 추적하는 변수
    private int attackCount = 0;
    // 적을 이동 불가 상태로 만드는 지속 시간
    [SerializeField] private float immobilizeDuration = 2.0f;

    // Attack 메서드를 재정의하여 근접 공격을 수행합니다.
    public override void Attack()
    {
        base.Attack();

        if (enemies.Count > 0)
        {
            float distance = Vector2.Distance(transform.position, enemies[0].transform.position);

            if (distance <= attackRange)
            {
                attackCount++;
                enemies[0].GetDamage(damage); // 적에게 데미지 입히기

                if (attackCount >= 5) // 5번 공격할 때마다
                {
                    attackCount = 0; // 공격 횟수 초기화
                    StartCoroutine(ImmobilizeEnemy(enemies[0])); // 적을 이동 불가 상태로 만들기
                }
            }
        }
    }

    // 적을 일정 시간 동안 이동 불가 상태로 만드는 코루틴
    private IEnumerator ImmobilizeEnemy(Enemy enemy)
    {
        Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
        if (enemyRb != null)
        {
            // 적의 이동을 멈추기 위해 속도를 0으로 설정하고, 물리적인 움직임을 잠시 멈춥니다.
            Vector2 originalVelocity = enemyRb.velocity;
            enemyRb.velocity = Vector2.zero;
            enemyRb.constraints = RigidbodyConstraints2D.FreezeAll;

            yield return new WaitForSeconds(immobilizeDuration); // immobilizeDuration 동안 대기

            // 시간이 지나면 적의 움직임을 다시 활성화합니다.
            enemyRb.constraints = RigidbodyConstraints2D.FreezeRotation;
            enemyRb.velocity = originalVelocity;
        }
    }
}
