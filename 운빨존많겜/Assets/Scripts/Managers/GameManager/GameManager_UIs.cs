using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_UIs : MonoBehaviour
{
    [SerializeField] Transform selectedUI;
    [SerializeField] Button m_deleteButton, m_fuseButton;
    public Button deleteButton { get { return m_deleteButton; } }
    public Button fuseButton { get { return m_fuseButton; } }
    private void Awake()
    {
        selectedUI.gameObject.SetActive(false);
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
}
