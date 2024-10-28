using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInteraction : MonoBehaviour, IPointerDownHandler
{
    private Fevent eve;

    public void OnPointerDown(PointerEventData eventData) // 修正类型名称
    {
        if (eve != null)
        {
        // 在这里调用玩家的跳跃方法
            eve.UIInteraction();
        }
    }

    public void UpdatePlayersRef(Fevent newEve) => eve = newEve;
}
