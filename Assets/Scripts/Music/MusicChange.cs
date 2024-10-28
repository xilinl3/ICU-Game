using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChange : MonoBehaviour
{
    public List<AudioClip> musicList;
    public List<GameObject> musicUIList; // 对应Canvas中的UI子对象
    private AudioSource audioSource;
    public int currentMusicIndex = 0;
    private bool isPlayerInRoom = false;
    private Animator animator;

    void Start()
    {
        audioSource = GameObject.Find("Camera Variant").GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        if (musicList.Count > 0)
        {
            audioSource.clip = musicList[currentMusicIndex];
            audioSource.Play();
        }
    }

    void Update()
    {
        if (isPlayerInRoom && Input.GetKeyDown(KeyCode.F))
        {
            SwitchMusic();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRoom = true;
            Debug.Log("Player in room");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRoom = false;
            Debug.Log("Player leave room");
        }
    }

    private void SwitchMusic()
    {
        currentMusicIndex = (currentMusicIndex + 1) % musicList.Count;
        audioSource.clip = musicList[currentMusicIndex];
        audioSource.Play();
        UpdateAnimation();
        UpdateUI();
    }

    public void ResetMusic()
    {
        currentMusicIndex = 0;
        if (musicList.Count > 0)
        {
            audioSource.clip = musicList[currentMusicIndex];
            audioSource.Play();
        }
        UpdateAnimation();
        UpdateUI();
    }

    private void UpdateAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("Play", currentMusicIndex != 0);
        }
    }

    private void UpdateUI()
    {
        // 先关闭所有UI子对象
        foreach (var uiElement in musicUIList)
        {
            uiElement.SetActive(false);
        }

        // 启用当前音乐对应的UI子对象
        if (currentMusicIndex < musicUIList.Count)
        {
            musicUIList[currentMusicIndex].SetActive(true);
        }
    }

    // 关闭所有MusicList中的UI子对象
    public void CloseAllUI()
    {
        foreach (var uiElement in musicUIList)
        {
            uiElement.SetActive(false);
        }
    }
}
