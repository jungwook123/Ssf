using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameManager()
    {
        Instance = this;
    }
    #region Enemies
    [SerializeField] Transform[] m_enemyWaypoints;
    public Transform[] enemyWaypoints { get { return m_enemyWaypoints; } }
    public List<Enemy> enemies { get; } = new();
    #endregion
    #region Towers
    [SerializeField] List<TowerData> commonTowers, uncommonTowers, rareTowers, legedaryTowers;
    #endregion
    #region Grid
    const int gridSizeX = 6, gridSizeY = 3;
    const float gridCellSizeX = 1.6f, gridCellSizeY = 1.6f;
    Tower[,] towers = new Tower[gridSizeY, gridSizeX];
    [SerializeField]
    Transform[] gridParents = new Transform[gridSizeY];
    Transform[,] gridPos = new Transform[gridSizeY, gridSizeX];
    #endregion
    #region Chances
    [SerializeField]
    Chances[] chances;
    int chanceLevel = 0;
    #endregion
    Tower selected = null;
    private void Awake()
    {
        for(int i = 0; i < gridSizeY; i++)
        {
            for(int k = 0; k < gridSizeX; k++)
            {
                gridPos[i, k] = gridParents[i].GetChild(k);
            }
        }
    }
    public int GetIndex(Enemy enemy) => enemies.IndexOf(enemy);
    public void EnemyIndexReset(Enemy enemy)
    {
        enemies.Remove(enemy);
        enemies.Add(enemy);
    }
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.up, 0.0f, LayerMask.GetMask("Tile"));
        if(hit && Input.GetMouseButtonDown(0))
        {
            Debug.Log(hit.transform.name);
            if (selected != null) selected.Unselect();
            selected = towers[(hit.transform.name[0] - '0') - 1, (hit.transform.name[2] - '0') - 1];
            if (selected != null) selected.Select();
        }
    }
    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
        if(enemies.Count >= 100)
        {
            GameOver(false);
        }
    }
    public void SpawnTower()
    {
        int odd = UnityEngine.Random.Range(1, 1001);
        TowerData towerToSpawn;
        if (odd <= chances[chanceLevel].legendaryChance)
        {
            towerToSpawn = legedaryTowers.GetRandom();
        }
        else if (odd <= chances[chanceLevel].legendaryChance + chances[chanceLevel].rareChance)
        {
            towerToSpawn = rareTowers.GetRandom();
        }
        else if (odd <= chances[chanceLevel].legendaryChance + chances[chanceLevel].rareChance + chances[chanceLevel].uncommonChance)
        {
            towerToSpawn = uncommonTowers.GetRandom();
        }
        else
        {
            towerToSpawn = commonTowers.GetRandom();
        }
        for (int i = 0; i < gridSizeY; i++)
        {
            for (int k = 0; k < gridSizeX; k++)
            {
                if (towers[i, k] == null) continue;
                if (towers[i, k].data == towerToSpawn)
                {
                    if (towers[i, k].AddTower()) return;
                }
            }
        }
        for (int i = 0; i < gridSizeY; i++)
        {
            for (int k = 0; k < gridSizeX; k++)
            {
                if (towers[i, k] == null)
                {
                    towers[i, k] = Instantiate(towerToSpawn.tower, gridPos[i, k].position, Quaternion.identity).GetComponent<Tower>();
                    towers[i, k].Set(towerToSpawn);
                    return;
                }
            }
        }
    }
    public Action onGameOver;
    public void GameOver(bool victory)
    {
        onGameOver?.Invoke();
        foreach(var i in towers)
        {
            if(i!=null) Destroy(i.gameObject);
        }
        foreach(var i in enemies)
        {
            Destroy(i.gameObject);
        }
    }
}
[System.Serializable]
public struct Chances
{
    //N in 1000;
    public int uncommonChance, rareChance, legendaryChance;
}
