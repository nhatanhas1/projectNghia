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
        aud.Stop();
        StartCoroutine(WaitAbit());
        
        
    }
    public void GameStart()
    {
        SceneManager.LoadScene("Stage_1");        
    }
    public void Quit()
    {
        Application.Quit();
    }

   IEnumerator WaitAbit()
    {
        yield return new WaitForSeconds(1);
        aud.Play();
    }
}
