using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] int startTime;
    [SerializeField] Wave[] waves;
    [SerializeField] Text waveText, waveTimeText;
    [SerializeField] Button skipButton;
    private void Awake()
    {
        skipButton.onClick.AddListener(Skip);
        StartCoroutine(Intermission());
    }
    IEnumerator Intermission()
    {
        waveText.text = "Intermission";
        for (int i = 0; i < startTime; i++)
        {
            waveTimeText.text = $"Starting in: {startTime - i}";
            yield return new WaitForSeconds(1);
        }
        skipButton.gameObject.SetActive(true);
        Wave();
    }
    IEnumerator waveWaiting = null;
    int currentWave = 0;
    void Wave()
    {
        if(currentWave < waves.Length - 1)
        {
            waveText.text = $"Wave {currentWave + 1}";
            waveWaiting = WaveWait();
            StartCoroutine(waveWaiting);
        }
        else
        {
            waveText.text = "Final Wave";
            waveTimeText.text = "";
            skipButton.gameObject.SetActive(false);
        }
        StartCoroutine(SpawnWave(currentWave));
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
        if (waveIndex == waves.Length - 1) GameManager.Instance.AllSpawnEnd();
    }
    IEnumerator WaveWait()
    {
        for(int i = 0; i < waves[currentWave].endTime; i++)
        {
            waveTimeText.text = $"Next Wave In: {waves[currentWave].endTime - i}";
            yield return new WaitForSeconds(1);
        }
        currentWave++;
        Wave();
    }
    void Skip()
    {
        StopCoroutine(waveWaiting);
        currentWave++;
        Wave();
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
