using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager_UIs : MonoBehaviour
{
    [SerializeField] Transform selectedUI;
    [SerializeField] Button m_deleteButton, m_fuseButton;
    [SerializeField] Text moneyText, shuffleButtonText;
    [SerializeField] Button[] cards = new Button[GameManager.cardCount];
    [SerializeField] DamageText damageEffectPrefab;
    public Button deleteButton { get { return m_deleteButton; } }
    public Button fuseButton { get { return m_fuseButton; } }
    private void Awake()
    {
        selectedUI.gameObject.SetActive(false);
        GameManager.Instance.onMoneyChange += (int money) => { moneyText.text = string.Concat(money, "$"); };
        GameManager.Instance.onCardShuffle += CardShuffle;
        GameManager.Instance.onCardSelect += CardSelect;
        dmgEffectPool = new Pooler<DamageText>(damageEffectPrefab, 100, 10);
    }
    public void SelectUI(Tower tower)
    {
        selectedUI.position = tower.transform.position;
        selectedUI.gameObject.SetActive(true);
    }
    public void CloseSelectUI()
    {
        selectedUI.gameObject.SetActive(false);
    }
    public bool _IsMouseOnUI()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    void CardShuffle()
    {
        for(int i = 0; i < GameManager.cardCount; i++)
        {
            cards[i].image.sprite = GameManager.Instance.cards[i].cardImage;
            TowerData tmp = GameManager.Instance.cards[i];
            cards[i].onClick.RemoveAllListeners();
            cards[i].onClick.AddListener(delegate { GameManager.Instance.SelectCard(tmp); });
            cards[i].interactable = true;
        }
        shuffleButtonText.text = $"Shuffle({GameManager.Instance.shuffleCost}$)";
    }
    void CardSelect()
    {
        for (int i = 0; i < GameManager.cardCount; i++)
        {
            cards[i].onClick.RemoveAllListeners();
            cards[i].interactable = false;
        }
    }
    public Pooler<DamageText> dmgEffectPool { get; private set; }
    public void DamageUI(Enemy hit, float damage)
    {
        if(damage/hit.maxHp >= 0.5f)
        {
            ShowDamageUI(hit.transform.position, $"{damage}!!", Color.red);
        }
        else if(damage/hit.maxHp >= 0.25f)
        {
            ShowDamageUI(hit.transform.position, $"{damage}!", new Color(1f, 0.5f, 0));
        }
        else
        {
            ShowDamageUI(hit.transform.position, damage.ToString(), new Color(1f, 1f, 0));
        }
    }
    public void DamageUI(Enemy hit, float damage, Color color)
    {
        if (damage / hit.maxHp >= 0.5f)
        {
            ShowDamageUI(hit.transform.position, $"{damage}!!", color);
        }
        else if (damage / hit.maxHp >= 0.25f)
        {
            ShowDamageUI(hit.transform.position, $"{damage}!", color);
        }
        else
        {
            ShowDamageUI(hit.transform.position, damage.ToString(), color);
        }
    }
    void ShowDamageUI(Vector2 pos, string content, Color color)
    {
        dmgEffectPool.GetObject(pos, Quaternion.identity).Set(content, color, new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)).normalized);
    }
    public void Release(DamageText obj) => dmgEffectPool.ReleaseObject(obj);
}
