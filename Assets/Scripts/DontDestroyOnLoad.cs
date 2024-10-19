using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
  private void Awake()
    {
        DontDestroyOnLoad(gameObject); // 确保对象在场景切换时不被销毁
    }

}
