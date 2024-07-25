using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Wave[] waves;
    int waveIndex = 0;
    private void Awake()
    {
        if (waves.Length > 0) StartCoroutine(SpawnWave());
    }
    IEnumerator SpawnWave()
    {
        while (waves[waveIndex].elements.Count > 0)
        {
            for (int i = 0; i < waves[waveIndex].elements[0].count; i++)
            {
                Enemy tmp = Instantiate(waves[waveIndex].elements[0].enemy, GameManager.Instance.enemyWaypoints[0].position, Quaternion.identity).GetComponent<Enemy>();
                GameManager.Instance.AddEnemy(tmp);
                yield return new WaitForSeconds(waves[waveIndex].elements[0].spawnCooldown);
            }
            waves[waveIndex].elements.RemoveAt(0);
        }
        yield return new WaitForSeconds(waves[waveIndex].endTime);
        if (++waveIndex < waves.Length) StartCoroutine(SpawnWave());
    }
}
[System.Serializable]
public class Wave
{
    public List<WaveElement> elements;
    [SerializeField] float m_endTime;
    public float endTime { get { return m_endTime; } }
}
[System.Serializable]
public class WaveElement
{
    [SerializeField] GameObject m_enemy;
    public GameObject enemy { get { return m_enemy; } }
    [SerializeField] int m_count;
    public int count { get { return m_count; } }
    [SerializeField] float m_spawnCooldown;
    public float spawnCooldown { get { return m_spawnCooldown; } }
}
