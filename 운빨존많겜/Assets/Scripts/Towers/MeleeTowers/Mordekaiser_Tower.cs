using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mordekaiser_Tower : Tower
{
    // ������ ���� ������ ��Ÿ���� ����
    [SerializeField] private float attackRange;
    // 5�� ���ݸ��� ���� ����Ű�� ���� ���� Ƚ���� �����ϴ� ����
    private int attackCount = 0;

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

                if (attackCount >= 5) // 5�� ������ ������
                {
                    attackCount = 0; // ���� Ƚ�� �ʱ�ȭ
                    enemies[0].Die(); // ���� ����ŵ�ϴ�
                }
                else
                {
                    enemies[0].GetDamage(damage); // �⺻ ������ ����
                }
            }
        }
    }
}
