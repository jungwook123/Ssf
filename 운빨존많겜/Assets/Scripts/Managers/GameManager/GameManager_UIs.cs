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
    [SerializeField] Text[] costTexts = new Text[GameManager.cardCount];
    [SerializeField] DamageText damageEffectPrefab;
    [SerializeField] RectTransform baseHPImage;
    [SerializeField] Text baseHPText;
    public Button deleteButton { get { return m_deleteButton; } }
    public Button fuseButton { get { return m_fuseButton; } }
    private void Awake()
    {
        selectedUI.gameObject.SetActive(false);
        GameManager.Instance.onMoneyChange += (int money) => { moneyText.text = string.Concat(money, "$"); };
        GameManager.Instance.onCardShuffle += CardShuffle;
        GameManager.Instance.onCardShuffle += PriceRefresh;
        GameManager.Instance.onCardSelect += CardSelect;
        GameManager.Instance.onCardSelect += PriceRefresh;
        GameManager.Instance.onBaseDamage += BaseHPUI;
        for(int i = 0; i < GameManager.cardCount; i++)
        {
            int k = i;
            cards[i].onClick.AddListener(() => { GameManager.Instance.SelectCard(k); });
        }
        BaseHPUI();
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
            cards[i].interactable = true;
        }
    }
    void CardSelect(int cardIndex)
    {
        cards[cardIndex].interactable = false;
    }
    void PriceRefresh()
    {
        for (int i = 0; i < GameManager.cardCount; i++)
        {
            costTexts[i].text = $"{GameManager.Instance.towerPrices[GameManager.Instance.cards[i]]}$";
        }
    }
    void PriceRefresh(int cardIndex)
    {
        PriceRefresh();
    }
    void BaseHPUI()
    {
        baseHPImage.localPosition = new Vector2(-150.0f + (-300.0f * (1.0f - GameManager.Instance.baseHP / GameManager.Instance.maxBaseHP)), baseHPImage.localPosition.y);
        baseHPText.text = $"기지 체력: {Mathf.CeilToInt(GameManager.Instance.baseHP)}/{GameManager.Instance.maxBaseHP}";
    }
    public Pooler<DamageText> dmgEffectPool { get; private set; }
    public void DamageUI(Enemy hit, float damage)
    {
        damage = Mathf.Ceil(damage * 4.0f) / 4.0f;
        if(damage >= 50.0f)
        {
            ShowDamageUI(hit.transform.position, $"{damage}!!", Color.red);
        }
        else if(damage >= 30.0f)
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
        damage = Mathf.Ceil(damage * 4.0f) / 4.0f;
        if (damage >= 50.0f)
        {
            ShowDamageUI(hit.transform.position, $"{damage}!!", color);
        }
        else if (damage >= 30.0f)
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
