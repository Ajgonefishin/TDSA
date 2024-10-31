using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GretaSounds : MonoBehaviour
{

    public AudioSource src;
    public AudioClip jump, dash, death;


    public void jumpSFX()
    {
        src.clip = jump;
        src.Play();
    }
    public void dashSFX()
    {
        src.clip = dash;
        src.Play();
    }
    public void deathSFX()
    {
        src.clip = death;
        src.Play();
    }
}
