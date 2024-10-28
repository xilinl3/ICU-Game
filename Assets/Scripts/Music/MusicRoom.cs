using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicRoom : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MusicChange musicChange = GameObject.FindObjectOfType<MusicChange>();
            if (musicChange != null)
            {
                musicChange.ResetMusic();
                musicChange.CloseAllUI(); // 关闭所有MusicList中的UI子对象
            }
        }
    }
}
