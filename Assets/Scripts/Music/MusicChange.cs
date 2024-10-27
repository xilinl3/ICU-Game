using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChange : MonoBehaviour
{
    public List<AudioClip> musicList;
    private AudioSource audioSource;
    private int currentMusicIndex =0;
    private bool isPlayerInRoom = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("Camera Variant").GetComponent<AudioSource>();

        if(musicList.Count > 0)
        {
            audioSource.clip = musicList[currentMusicIndex];
            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerInRoom && Input.GetKeyDown(KeyCode.F))
        {
            SwitvhMusic();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other. CompareTag("Player"))
        {
            isPlayerInRoom = true;
            Debug.Log("Player in room");
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other. CompareTag("Player"))
        {
            isPlayerInRoom = false;
            Debug.Log("Player leave room");
        }
    }
    private void SwitvhMusic()
    {
        currentMusicIndex = (currentMusicIndex + 1) % musicList.Count;
        audioSource.clip = musicList[currentMusicIndex];
        audioSource.Play();
    }
}
