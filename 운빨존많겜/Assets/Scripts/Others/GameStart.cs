using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    private void Awake()
    {
        SceneSwitcher.Create();
    }
    public void SceneChange()
    {
        SceneSwitcher.Instance.SwitchScene("InGame");
    }
}
