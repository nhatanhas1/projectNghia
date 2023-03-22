using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    // Start is called before the first frame update
    private void Start()
    {
        Time.timeScale = 1.0f; 
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale= 0;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale= 1;
    }
     public void  Home()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
