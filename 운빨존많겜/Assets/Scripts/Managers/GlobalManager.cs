using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalManager : MonoBehaviour
{
    private static GlobalManager instance;
    public static GlobalManager Instance { get
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
    bool prompting = false;
    float counter = 0.0f;
    private void Update()
    {
        if (prompting)
        {
            counter += Time.deltaTime;
            if(counter >= 2.0f)
            {
                prompting = false;
                anim.SetBool("Prompting", false);
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                counter = 0.0f;
                prompting = true;
                anim.SetBool("Prompting", true);
            }
        }
    }
    public static void Create()
    {
        if (instance != null) return;
        instance = Instantiate(Resources.Load<GlobalManager>("DontDestroyObject"));
        DontDestroyOnLoad(instance.gameObject);
    }
}
