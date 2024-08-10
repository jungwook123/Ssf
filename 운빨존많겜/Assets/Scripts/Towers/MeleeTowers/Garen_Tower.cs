using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garen_Tower : Tower
{
    // ������ ���� ������ ��Ÿ���� ����
    [SerializeField] private float attackRange;
    // 5�� ���ݸ��� ���� �̵� �Ұ� ���·� ����� ���� ���� Ƚ���� �����ϴ� ����
    private int attackCount = 0;
    // ���� �̵� �Ұ� ���·� ����� ���� �ð�
    [SerializeField] private float immobilizeDuration = 2.0f;

    // Attack �޼��带 �������Ͽ� ���� ������ �����մϴ�.
    public override void Attack()
    {
        base.Attack();

        if (enemies.Count > 0)
        {
            float distance = Vector2.Distance(transform.position, enemies[0].transform.position);

            if (distance <= attackRange)
            {
                attackCount++;
                enemies[0].GetDamage(damage); // ������ ������ ������

                if (attackCount >= 5) // 5�� ������ ������
                {
                    attackCount = 0; // ���� Ƚ�� �ʱ�ȭ
                    StartCoroutine(ImmobilizeEnemy(enemies[0])); // ���� �̵� �Ұ� ���·� �����
                }
            }
        }
    }

    // ���� ���� �ð� ���� �̵� �Ұ� ���·� ����� �ڷ�ƾ
    private IEnumerator ImmobilizeEnemy(Enemy enemy)
    {
        Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
        if (enemyRb != null)
        {
            // ���� �̵��� ���߱� ���� �ӵ��� 0���� �����ϰ�, �������� �������� ��� ����ϴ�.
            Vector2 originalVelocity = enemyRb.velocity;
            enemyRb.velocity = Vector2.zero;
            enemyRb.constraints = RigidbodyConstraints2D.FreezeAll;

            yield return new WaitForSeconds(immobilizeDuration); // immobilizeDuration ���� ���

            // �ð��� ������ ���� �������� �ٽ� Ȱ��ȭ�մϴ�.
            enemyRb.constraints = RigidbodyConstraints2D.FreezeRotation;
            enemyRb.velocity = originalVelocity;
        }
    }
}
