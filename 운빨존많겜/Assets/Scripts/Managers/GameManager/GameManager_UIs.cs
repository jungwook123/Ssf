using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager_UIs : MonoBehaviour
{
    [SerializeField] Transform selectedUI;
    [SerializeField] Button m_deleteButton, m_fuseButton;
    [SerializeField] Text moneyText, shuffleButtonText;
    [SerializeField] Button[] cards = new Button[GameManager.cardCount];
    public Button deleteButton { get { return m_deleteButton; } }
    public Button fuseButton { get { return m_fuseButton; } }
    private void Awake()
    {
        selectedUI.gameObject.SetActive(false);
        GameManager.Instance.onMoneyChange += (int money) => { moneyText.text = string.Concat(money, "$"); };
        GameManager.Instance.onCardShuffle += CardShuffle;
        GameManager.Instance.onCardSelect += CardSelect;
    }
    public void SelectUI(Tower tower)
    {
        selectedUI.position = tower.transform.position;
        if(tower.data.upgrade != null)
        {
            fuseButton.interactable = true;
        }
        else
        {
            fuseButton.interactable = false;
        }
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
}
