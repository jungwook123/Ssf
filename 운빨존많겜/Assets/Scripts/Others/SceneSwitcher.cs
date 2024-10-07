using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private static SceneSwitcher instance;
    public static SceneSwitcher Instance { get
        {
            if (instance == null)
            {
                Create();
            }
            return instance;
        } 
    }
    [SerializeField] Animator anim;
    string switchingTo;
    bool switching = false;
    public void SwitchScene(string sceneName)
    {
        if (switching) return;
        switching = true;
        switchingTo = sceneName;
        anim.Play("ExitScene");
    }
    public void Switch()
    {
        SceneManager.LoadScene(switchingTo);
        switching = false;
    }
    public static void Create()
    {
        if (instance != null) return;
        instance = Instantiate(Resources.Load<SceneSwitcher>("DontDestroyObject"));
        DontDestroyOnLoad(instance.gameObject);
    }
}
