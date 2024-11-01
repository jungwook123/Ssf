using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#region 개발자 전용
[RequireComponent(typeof(GameManager_UIs))]
#endregion
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
    [SerializeField] int defaultTowerPrice = 10, towerPriceIncrease = 5;
    public Dictionary<TowerData, int> towerPrices { get; } = new();
    public const int cardCount = 3;
    public TowerData[] cards { get; private set; } = new TowerData[cardCount];
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
    #endregion
    #region baseHP
    [SerializeField] float m_maxBaseHP;
    public float maxBaseHP { get { return m_maxBaseHP; } }
    public float baseHP { get; private set; }
    #endregion
    #region soundClips
    [SerializeField] AudioVolumePair shuffleSound, upgradeSound;
    #endregion
    bool spawnEnded = false;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        UIs = GetComponent<GameManager_UIs>();

        topLayer = new GameManager_TopLayer(this);
        topLayer.OnStateEnter();
        onGameOver += () => { enabled = false; };

        for(int i = 0; i < gridSizeY; i++)
        {
            for(int k = 0; k < gridSizeX; k++)
            {
                grid[i, k] = gridParents[i].GetChild(k).GetComponent<Tile>();
                grid[i, k].order = i;
            }
        }
        foreach (var i in towers) towerPrices.Add(i, defaultTowerPrice);
        baseHP = maxBaseHP;
    }
    private void Start()
    {
        for (int i = 0; i < cardCount; i++)
        {
            cards[i] = towers.GetRandom();
        }
        onCardShuffle?.Invoke();
    }
    public int GetIndex(Enemy enemy) => enemies.IndexOf(enemy);
    private void Update()
    {
        if (gameOver) return;
        topLayer.OnStateUpdate();
    }
    bool enemySorted = false;
    private void LateUpdate()
    {
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
    public void AllSpawnEnd() => spawnEnded = true;
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
    public void UpgradeTower(TowerData towerToUpgrade, Tile tile)
    {
        if (towerToUpgrade.upgrade == null || SearchTower(towerToUpgrade) < 3) return;
        Destroy(tile.tower.gameObject);
        tile.tower = null;
        int removed = 1;
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
        tile.tower = Instantiate(towerToUpgrade.upgrade.tower, tile.transform.position, Quaternion.identity);
        tile.tower.Set(towerToUpgrade.upgrade);
        onTowerSpawn?.Invoke(towerToUpgrade.upgrade);
        AudioManager.Instance.PlayAudio(upgradeSound);
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
    public Action onCardShuffle;
    public Action<int> onCardSelect;
    public void ShuffleCards()
    {
        if (money < shuffleCost) return;
        MoneyChange(-shuffleCost);
        for(int i = 0; i < cardCount; i++)
        {
            cards[i] = towers.GetRandom();
        }
        AudioManager.Instance.PlayAudio(shuffleSound);
        onCardShuffle?.Invoke();
    }
    public void SelectCard(int cardIndex)
    {
        if (money < towerPrices[cards[cardIndex]]) return;
        MoneyChange(-towerPrices[cards[cardIndex]]);
        towerPrices[cards[cardIndex]] += towerPriceIncrease;
        if (SpawnTower(cards[cardIndex]))
        {
            onCardSelect?.Invoke(cardIndex);
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
        if (gameOver) return;
        gameOver = true;
        onGameOver?.Invoke();
        foreach(var i in enemies)
        {
            Destroy(i.gameObject);
        }
        if (victory)
        {
            GlobalManager.Instance.SwitchScene("Victory");
        }
        else 
        {
            GlobalManager.Instance.SwitchScene("Defeat");
        }
    }
}
