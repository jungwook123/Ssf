using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mordekaiser_Tower : Tower
{
    // 공격할 적의 범위를 나타내는 변수
    [SerializeField] private float attackRange;
    // 5번 공격마다 적을 즉사시키기 위해 공격 횟수를 추적하는 변수
    private int attackCount = 0;

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

                if (attackCount >= 5) // 5번 공격할 때마다
                {
                    attackCount = 0; // 공격 횟수 초기화
                    enemies[0].Die(); // 적을 즉사시킵니다
                }
                else
                {
                    enemies[0].GetDamage(damage); // 기본 데미지 공격
                }
            }
        }
    }
}
