using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    static bool introWatched = false;
    [SerializeField] Animator anim;
    int promptingID;
    private void Awake()
    {
        promptingID = Animator.StringToHash("Prompting");
        SceneSwitcher.Create();
        if (!introWatched)
        {
            anim.SetTrigger("Intro");
            introWatched = true;
        }
    }
    private void Update()
    {
        if (anim.GetBool(promptingID) && Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
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
