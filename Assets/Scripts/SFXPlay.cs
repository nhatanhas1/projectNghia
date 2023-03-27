using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlay : MonoBehaviour
{
    public AudioSource slap;
    public AudioSource eat;
    public AudioSource hit;       
    public AudioSource dead;
    //public AudioSource loop;

    private void Start()
    {
        
    }
    public void PlaySlap()
    {
        slap.Play();
    }
    public void PlayEat()
    {
        eat.Play();
    }
    public void PlayHit()
    {
        hit.Play();
    }
    public void PlayDead()
    {
        dead.Play();
    }

}
