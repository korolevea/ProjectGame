using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public List<GameObject> windows;

    // Start is called before the first frame update
    void Start()
    {
        OpenWindow("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenWindow(string _nameWindow)
    {
        foreach(GameObject window in windows)
        {
            if (window.name == _nameWindow)
            {
                window.SetActive(true);
            }
            else
            {
                window.SetActive(false);
            }
        }
    }

    public void LoadScene(string _nameScene)
    {
        SceneManager.LoadScene(_nameScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
