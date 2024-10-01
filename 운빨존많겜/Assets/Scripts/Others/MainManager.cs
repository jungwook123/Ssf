using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    static bool introWatched = false;
    [SerializeField] Animator anim;
    private void Awake()
    {
        SceneSwitcher.Create();
        if (!introWatched)
        {
            anim.SetTrigger("Intro");
            introWatched = true;
        }
    }
    public void SceneChange(string sceneName)
    {
        SceneSwitcher.Instance.SwitchScene(sceneName);
    }
    public void ToTitle()
    {
        SceneSwitcher.Instance.SwitchScene("Title");
    }
}
