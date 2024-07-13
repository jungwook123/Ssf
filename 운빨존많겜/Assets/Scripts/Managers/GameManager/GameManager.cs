using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(GameManager_UIs))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameManager_UIs UIs { get; private set; }
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
    Tile[,] grid = new Tile[gridSizeY, gridSizeX];
    [SerializeField]
    Transform[] gridParents = new Transform[gridSizeY];
    #endregion
    #region Chances
    [SerializeField]
    Chances[] chances;
    int chanceLevel = 0;
    #endregion
    #region FSMVals
    TopLayer<GameManager> topLayer;
    public Tower selected = null;
    public Tile selectedTile = null;
    [SerializeField] Transform m_moveArrow;
    public Transform moveArrow { get { return m_moveArrow; } }
    #endregion
    #region Money
    [SerializeField] int m_money;
    public int money { get { return m_money; } }
    public Action<int> onMoneyEarn;
    public void MoneyChange(int amount)
    {
        if(money + amount < 0)
        {
            onMoneyEarn?.Invoke(-money);
            m_money = 0;
        }
        else
        {
            onMoneyEarn?.Invoke(amount);
            m_money += amount;
        }
    }
    #region Costs
    public int spawnCost { get; private set; } = 20;
    const int spawnCostIncrease = 2;
    #endregion
    #endregion
    private void Awake()
    {
        UIs = GetComponent<GameManager_UIs>();

        topLayer = new GameManager_TopLayer(this);
        topLayer.OnStateEnter();
        onGameOver += () => { enabled = false; };

        for(int i = 0; i < gridSizeY; i++)
        {
            for(int k = 0; k < gridSizeX; k++)
            {
                grid[i, k] = gridParents[i].GetChild(k).GetComponent<Tile>();
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
        topLayer.OnStateUpdate();
    }
    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
        if(enemies.Count >= 100)
        {
            GameOver(false);
        }
    }
    public Action<TowerData> onTowerSpawn;
    public void SpawnTower()
    {
        if (money > spawnCost)
        {
            MoneyChange(-spawnCost);
            spawnCost += spawnCostIncrease;
        }
        else
        {
            return;
        }
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
                if (grid[i, k].tower == null) continue;
                if (grid[i, k].tower.data == towerToSpawn)
                {
                    if (grid[i, k].tower.AddTower())
                    {
                        onTowerSpawn?.Invoke(towerToSpawn);
                        return;
                    }
                }
            }
        }
        for (int i = 0; i < gridSizeX; i++)
        {
            for (int k = 0; k < gridSizeY; k++)
            {
                if (grid[k, i].tower == null)
                {
                    grid[k, i].tower = Instantiate(towerToSpawn.tower, grid[k, i].transform.position, Quaternion.identity).GetComponent<Tower>();
                    grid[k, i].tower.Set(towerToSpawn);
                    onTowerSpawn?.Invoke(towerToSpawn);
                    return;
                }
            }
        }
    }
    public Action onGameOver;
    public void GameOver(bool victory)
    {
        onGameOver?.Invoke();
        foreach(var i in grid)
        {
            if(i.tower!=null) Destroy(i.tower.gameObject);
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
