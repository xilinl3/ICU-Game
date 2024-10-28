using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerReload : MonoBehaviour, IPointerDownHandler
{
    private SavePlayer player;

    public void OnPointerDown(PointerEventData eventData) // 修正类型名称
    {
        if (player != null)
        {
            // 在这里调用玩家的跳跃方法
            player.UICheck();
        }
    }

    public void UpdatePlayersRef(SavePlayer newPlayer) => player = newPlayer;
}
