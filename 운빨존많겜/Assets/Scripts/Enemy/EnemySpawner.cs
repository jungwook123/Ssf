using System.Collections;
using System.Collections.Generic;
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
        GameManager.Instance.onGameOver += () => { StopAllCoroutines(); };
    }
    IEnumerator Intermission()
    {
        waveText.text = "게임 시작 전";
        for (int i = 0; i < startTime; i++)
        {
            waveTimeText.text = $"{startTime - i}초 뒤에 시작합니다.";
            yield return new WaitForSeconds(1);
        }
        skipButton.gameObject.SetActive(true);
        Wave();
    }
    IEnumerator waveWaiting = null;
    int currentWave = 0;
    void Wave()
    {
        skipButton.gameObject.SetActive(false);
        if (currentWave < waves.Length - 1)
        {
            waveText.text = $"웨이브 {currentWave + 1}";
            waveWaiting = WaveWait();
            StartCoroutine(waveWaiting);
        }
        else
        {
            waveText.text = "최종 웨이브";
            waveTimeText.text = "";
        }
        StartCoroutine(SpawnWave(currentWave));
    }
    IEnumerator SpawnWave(int waveIndex)
    {
        for(int i = 0; i < waves[waveIndex].elements.Count; i++)
        {
            for (int k = 0; k < waves[waveIndex].elements[i].count; k++)
            {
                AddEnemy(Instantiate(waves[waveIndex].elements[i].enemy, enemySpawnPosition, Quaternion.identity).GetComponent<Enemy>());
                if (i == waves[waveIndex].elements.Count - 1 && k == waves[waveIndex].elements[i].count - 1) break;
                yield return new WaitForSeconds(waves[waveIndex].elements[i].spawnCooldown);
            }
        }
        if (waveIndex == waves.Length - 1) GameManager.Instance.AllSpawnEnd();
        else
        {
            skipButton.gameObject.SetActive(true);
        }
    }
    IEnumerator WaveWait()
    {
        for(int i = 0; i < waves[currentWave].endTime; i++)
        {
            waveTimeText.text = $"다음 웨이브까지 남은 시간: {waves[currentWave].endTime - i}초";
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
    void AddEnemy(Enemy enemy) => GameManager.Instance.AddEnemy(enemy);
    Vector2 enemySpawnPosition => GameManager.Instance.enemyWaypoints[0].position;
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