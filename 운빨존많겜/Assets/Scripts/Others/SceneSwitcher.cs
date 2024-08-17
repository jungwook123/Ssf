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
    [SerializeField] GameObject prompt;
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
    bool prompting = false;
    const float promptTime = 2.0f;
    float counter = 0.0f;
    private void Update()
    {
        if (prompting)
        {
            if (counter > 0.0f) counter -= Time.deltaTime;
            else
            {
                prompt.SetActive(false);
                prompting = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (prompting)
            {
                Application.Quit();
            }
            else
            {
                prompt.SetActive(true);
                prompting = true;
                counter = promptTime;
            }
        }
    }
    public static void Create()
    {
        if (instance != null) return;
        instance = Instantiate(Resources.Load<SceneSwitcher>("DontDestroyObject"));
        DontDestroyOnLoad(instance.gameObject);
    }
}
