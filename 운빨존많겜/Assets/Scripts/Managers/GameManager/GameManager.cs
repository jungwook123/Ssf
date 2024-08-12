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
    public List<Enemy> enemies = new();
    #endregion
    #region Towers&Cards
    [SerializeField] List<TowerData> towers = new();
    public const int cardCount = 3;
    public TowerData[] cards { get; private set; } = new TowerData[cardCount];
    public bool cardSelected { get; private set; } = false;
    #endregion
    #region Grid
    const int gridSizeX = 6, gridSizeY = 3;
    Tile[,] grid = new Tile[gridSizeY, gridSizeX];
    [SerializeField]
    Transform[] gridParents = new Transform[gridSizeY];
    #endregion
    #region FSMVals
    TopLayer<GameManager> topLayer;
    public Tower selected = null;
    public Tile selectedTile = null;
    [SerializeField] Transform m_rangeViewer;
    public Transform rangeViewer { get { return m_rangeViewer; } }
    #endregion
    #region Money
    [SerializeField] int m_money;
    [SerializeField] int m_shuffleCost;
    [SerializeField] int shuffleCostIncrease;
    public int money { get { return m_money; } private set { m_money = value; } }
    public int shuffleCost { get { return m_shuffleCost; } private set { m_shuffleCost = value; } }
    public Action<int> onMoneyChange;
    public void MoneyChange(int amount)
    {
        money += amount;
        if(money  < 0)
        {
            money = 0;
        }
        onMoneyChange.Invoke(money);
    }
    #region Costs
    public int spawnCost { get; private set; } = 20;
    const int spawnCostIncrease = 2;
    #endregion
    #endregion
    #region baseHP
    [SerializeField] float m_maxBaseHP;
    public float maxBaseHP { get { return m_maxBaseHP; } }
    public float baseHP { get; private set; }
    #endregion
    bool spawnEnded = false;
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
        baseHP = maxBaseHP;
    }
    private void Start()
    {
        for (int i = 0; i < cardCount; i++)
        {
            cards[i] = towers.GetRandom();
        }
        cardSelected = false;
        onCardShuffle?.Invoke();
    }
    public int GetIndex(Enemy enemy) => enemies.IndexOf(enemy);
    private void Update()
    {
        if (gameOver) return;
        topLayer.OnStateUpdate();
    }
    private void LateUpdate()
    {
        if (gameOver) return;
        enemies.Sort((Enemy a, Enemy b) =>
        {
            int tmp = b.pointIndex.CompareTo(a.pointIndex);
            if (tmp == 0)
            {
                return Vector2.Distance(a.transform.position, enemyWaypoints[a.pointIndex].position).CompareTo(Vector2.Distance(b.transform.position, enemyWaypoints[b.pointIndex].position));
            }
            else return tmp;
        });
    }
    public void AllWaveEnd() => spawnEnded = true;
    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }
    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
        if(enemies.Count == 0 && spawnEnded)
        {
            GameOver(true);
        }
    }
    public Action<TowerData> onTowerSpawn;
    public void UpgradeTower(TowerData towerToUpgrade)
    {
        if (towerToUpgrade.upgrade == null || SearchTower(towerToUpgrade) < 3) return;
        int removed = 0;
        for (int i = 0; i < gridSizeX; i++)
        {
            for (int k = 0; k < gridSizeY; k++)
            {
                if (grid[k, i].tower != null && grid[k, i].tower.data == towerToUpgrade)
                {
                    removed++;
                    Destroy(grid[k, i].tower.gameObject);
                    grid[k, i].tower = null;
                    if (removed >= 3) break;
                }
            }
            if (removed >= 3) break;
        }
        SpawnTower(towerToUpgrade.upgrade);
    }
    public bool SpawnTower(TowerData towerToSpawn)
    {
        for (int i = 0; i < gridSizeX; i++)
        {
            for (int k = 0; k < gridSizeY; k++)
            {
                if (grid[k, i].tower == null)
                {
                    grid[k, i].tower = Instantiate(towerToSpawn.tower, grid[k, i].transform.position, Quaternion.identity);
                    grid[k, i].tower.Set(towerToSpawn);
                    onTowerSpawn?.Invoke(towerToSpawn);
                    return true;
                }
            }
        }
        return false;
    }
    public int SearchTower(TowerData tower)
    {
        int tot = 0;
        for (int i = 0; i < gridSizeX; i++)
        {
            for (int k = 0; k < gridSizeY; k++)
            {
                if (grid[k, i].tower != null && grid[k, i].tower.data == tower)
                {
                    tot++;
                }
            }
        }
        return tot;
    }
    public Action onCardShuffle, onCardSelect;
    public void ShuffleCards()
    {
        if (money < shuffleCost) return;
        MoneyChange(-shuffleCost);
        shuffleCost += shuffleCostIncrease;
        for(int i = 0; i < cardCount; i++)
        {
            cards[i] = towers.GetRandom();
        }
        cardSelected = false;
        onCardShuffle?.Invoke();
    }
    public void SelectCard(TowerData tower)
    {
        if (SpawnTower(tower))
        {
            cardSelected = true;
            onCardSelect?.Invoke();
        }
    }
    public Action onBaseDamage;
    public void GetBaseDamage(float damage)
    {
        baseHP = Mathf.Max(0.0f, baseHP - damage);
        onBaseDamage.Invoke();
        if(baseHP <= 0)
        {
            GameOver(false);
        }
    }
    public Action onGameOver;
    bool gameOver = false;
    public void GameOver(bool victory)
    {
        gameOver = true;
        onGameOver?.Invoke();
        foreach(var i in enemies)
        {
            Destroy(i.gameObject);
        }
    }
}
