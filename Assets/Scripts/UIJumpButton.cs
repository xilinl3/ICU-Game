using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIJumpButton : MonoBehaviour, IPointerDownHandler
{
    private player_behaviors player;

    public void OnPointerDown(PointerEventData eventData) // 修正类型名称
    {
        if (player != null)
        {
            // 在这里调用玩家的跳跃方法
            player.HandleUIJump();
        }
    }

    public void UpdatePlayersRef(player_behaviors newPlayer) => player = newPlayer;
}

