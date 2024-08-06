using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterYi_Tower : Tower
{
    // 공격할 적의 범위를 나타내는 변수
    [SerializeField] private float attackRange;
    // 10번 공격마다 추가 데미지를 위해 공격 횟수를 추적하는 변수
    private int attackCount = 0;
    [SerializeField] private float bonusTime = 5.0f;
    [SerializeField] private float bonusDamage = 20.0f;
    private bool bonusActive = false;

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
                if (bonusActive)
                {
                    enemies[0].GetDamage(damage + bonusDamage); // 추가 데미지를 포함한 공격
                }
                else
                {
                    enemies[0].GetDamage(damage); // 기본 데미지 공격
                }

                if (attackCount >= 10) // 10번 공격할 때마다
                {
                    attackCount = 0; // 공격 횟수 초기화
                    StartCoroutine(ActivateBonusDamage());
                }
            }
        }
    }

    // 추가 데미지를 활성화하는 코루틴
    private IEnumerator ActivateBonusDamage()
    {
        bonusActive = true; // 추가 데미지 활성화
        yield return new WaitForSeconds(bonusTime); // 5초 동안 대기
        bonusActive = false; // 추가 데미지 비활성화
    }
}
