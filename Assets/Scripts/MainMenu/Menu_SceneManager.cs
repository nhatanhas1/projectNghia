using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu_SceneManager : MonoBehaviour
{
  
    public AudioSource aud;
   // AudioClip bg;
    private void Start()
    {
       // aud = GetComponent<AudioSource>();
        Time.timeScale = 1.0f;
        aud.Stop();
        StartCoroutine(WaitAbit());
                
    }
    public void GameStart()
    {
        //SceneManager.LoadScene("Stage_1");
        SceneManager.LoadScene(1);
        
    }
    public void Quit()
    {
        Application.Quit();
        PlayerPrefs.SetInt("highscore", 0);
    }

   IEnumerator WaitAbit()
    {
        yield return new WaitForSeconds(0.5f);
        aud.Play();
    }
}
