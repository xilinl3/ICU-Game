using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicRoom : MonoBehaviour
{
    private AudioSource audioSource;
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
           MusicChange musicChange = GameObject.FindObjectOfType<MusicChange>();
           if(musicChange != null)
           {
               musicChange.RestMusic();
           }
        }
    }
}
