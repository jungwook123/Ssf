using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Wave[] waves;
    [SerializeField] Text waveText, waveTimeText;
    private void Awake()
    {
        StartCoroutine(Wave());
    }
    IEnumerator SpawnWave(int waveIndex)
    {
        for(int i = 0; i < waves[waveIndex].elements.Count; i++)
        {
            for (int k = 0; k < waves[waveIndex].elements[i].count; k++)
            {
                Enemy tmp = Instantiate(waves[waveIndex].elements[i].enemy, GameManager.Instance.enemyWaypoints[0].position, Quaternion.identity).GetComponent<Enemy>();
                GameManager.Instance.AddEnemy(tmp);
                yield return new WaitForSeconds(waves[waveIndex].elements[i].spawnCooldown);
            }
        }
        if (waveIndex == waves.Length - 1) GameManager.Instance.AllWaveEnd();
    }
    IEnumerator Wave()
    {
        for(int i = 0; i < waves.Length; i++)
        {
            if (i < waves.Length - 1) waveText.text = $"Wave {i + 1}";
            else
            {
                waveText.text = "Final Wave";
                waveTimeText.text = "";
            }
            StartCoroutine(SpawnWave(i));
            for(int k = 0; k < waves[i].endTime; k++)
            {
                if(i < waves.Length - 1) waveTimeText.text = $"Next Wave In: {waves[i].endTime - k}";
                yield return new WaitForSeconds(1);
            }
        }
    }
}
[System.Serializable]
public class Wave
{
    public List<WaveElement> elements;
    [SerializeField] int m_endTime;
    public int endTime { get { return m_endTime; } }
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
