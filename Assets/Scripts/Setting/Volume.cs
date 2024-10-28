using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public static Volume Instance { get; private set; }
    private float initialVolume;
    private bool volumeChanged = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            initialVolume = GetVolume();
            AudioListener.volume = initialVolume;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetVolume(float volume)
    {
        if (Mathf.Approximately(initialVolume, volume)) return;

        volumeChanged = true;
        AudioListener.volume = volume;
        initialVolume = volume;

        // 只有当音量变化时才存储
        PlayerPrefs.SetFloat("gameVolume", volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return PlayerPrefs.GetFloat("gameVolume", 1f);
    }
}