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
        GlobalManager.Create();
        if (!introWatched)
        {
            anim.SetTrigger("Intro");
            introWatched = true;
        }
    }
    public void SceneChange(string sceneName)
    {
        GlobalManager.Instance.SwitchScene(sceneName);
    }
}
