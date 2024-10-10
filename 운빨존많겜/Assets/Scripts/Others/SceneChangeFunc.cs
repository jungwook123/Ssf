using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeFunc : MonoBehaviour
{
    public void SceneChange(string sceneName)
    {
        GlobalManager.Instance.SwitchScene(sceneName);
    }
}