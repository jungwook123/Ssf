using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterYi_Tower : Tower
{
    // ������ ���� ������ ��Ÿ���� ����
    [SerializeField] private float attackRange;
    // 10�� ���ݸ��� �߰� �������� ���� ���� Ƚ���� �����ϴ� ����
    private int attackCount = 0;
    [SerializeField] private float bonusTime = 5.0f;
    [SerializeField] private float bonusDamage = 20.0f;
    private bool bonusActive = false;

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
                if (bonusActive)
                {
                    enemies[0].GetDamage(damage + bonusDamage); // �߰� �������� ������ ����
                }
                else
                {
                    enemies[0].GetDamage(damage); // �⺻ ������ ����
                }

                if (attackCount >= 10) // 10�� ������ ������
                {
                    attackCount = 0; // ���� Ƚ�� �ʱ�ȭ
                    StartCoroutine(ActivateBonusDamage());
                }
            }
        }
    }

    // �߰� �������� Ȱ��ȭ�ϴ� �ڷ�ƾ
    private IEnumerator ActivateBonusDamage()
    {
        bonusActive = true; // �߰� ������ Ȱ��ȭ
        yield return new WaitForSeconds(bonusTime); // 5�� ���� ���
        bonusActive = false; // �߰� ������ ��Ȱ��ȭ
    }
}
